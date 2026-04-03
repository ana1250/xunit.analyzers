using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class AssertContextV3Aot : AssertContextV3Base
{
	AssertContextV3Aot(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{ }

	public static IAssertContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.assert.aot", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new AssertContextV3Aot(compilation, version);
	}
}
