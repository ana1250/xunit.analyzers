using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3RunnerUtilityContext : V3RunnerUtilityContextBase
{
	const string assemblyPrefix = "xunit.v3.runner.utility.";

	V3RunnerUtilityContext(
		Compilation compilation,
		string platform,
		Version version) :
			base(compilation, platform, version)
	{ }

	public static IRunnerUtilityContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var assembly =
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.StartsWith(assemblyPrefix, StringComparison.OrdinalIgnoreCase));

		if (assembly is null)
			return null;

		var version = versionOverride ?? assembly.Version;
		var platform = assembly.Name.Substring(assemblyPrefix.Length);

		if (version is null || platform.Equals("aot", StringComparison.OrdinalIgnoreCase))
			return null;

		return new V3RunnerUtilityContext(compilation, platform, version);
	}
}
