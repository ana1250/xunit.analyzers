using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CommonContextV3 : CommonContextV3Base
{
	CommonContextV3(
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
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.common", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new CommonContextV3(compilation, version);
	}
}
