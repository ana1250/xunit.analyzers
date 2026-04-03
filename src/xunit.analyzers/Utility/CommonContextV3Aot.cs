using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CommonContextV3Aot : CommonContextV3Base
{
	CommonContextV3Aot(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{ }

	public static ICommonContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.common.aot", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new CommonContextV3Aot(compilation, version);
	}
}
