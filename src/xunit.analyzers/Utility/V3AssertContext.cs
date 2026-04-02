using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3AssertContext : V3AssertContextBase
{
	V3AssertContext(
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
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.assert", StringComparison.OrdinalIgnoreCase) || a.Name.Equals("xunit.v3.assert.source", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new V3AssertContext(compilation, version);
	}
}
