using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class RunnerCommonContextV3Aot : RunnerCommonContextV3Base
{
	RunnerCommonContextV3Aot(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{ }

	public static IRunnerCommonContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var assembly =
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.runner.common.aot", StringComparison.OrdinalIgnoreCase));

		if (assembly is null)
			return null;

		var version = versionOverride ?? assembly.Version;

		return version is null ? null : new RunnerCommonContextV3Aot(compilation, version);
	}
}
