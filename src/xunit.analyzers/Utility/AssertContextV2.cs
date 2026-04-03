using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class AssertContextV2 : AssertContextBase
{
	internal static readonly Version Version_2_5_0 = new("2.5.0");
	internal static readonly Version Version_2_9_3 = new("2.9.3");

	AssertContextV2(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{ }

	public override bool SupportsAssertFail =>
		Version >= Version_2_5_0;

	public override bool SupportsAssertNullWithPointers =>
		false;

	public override bool SupportsInexactTypeAssertions =>
		Version >= Version_2_9_3;

	public static AssertContextV2? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.assert", StringComparison.OrdinalIgnoreCase) || a.Name.Equals("xunit.assert.source", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new(compilation, version);
	}
}
