using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class RunnerUtilityContextV3Aot : RunnerUtilityContextBase, IRunnerUtilityContextV3
{
	RunnerUtilityContextV3Aot(
		Compilation compilation,
		Version version) :
			base(compilation, "aot", version)
	{ }

	public static IRunnerUtilityContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var assembly =
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.runner.utility.aot", StringComparison.OrdinalIgnoreCase));

		if (assembly is null)
			return null;

		var version = versionOverride ?? assembly.Version;

		return version is null ? null : new RunnerUtilityContextV3Aot(compilation, version);
	}
}
