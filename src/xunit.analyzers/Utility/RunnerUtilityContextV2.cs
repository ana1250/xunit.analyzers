using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class RunnerUtilityContextV2 : RunnerUtilityContextBase
{
	const string assemblyPrefix = "xunit.runner.utility.";

	RunnerUtilityContextV2(
		Compilation compilation,
		string platform,
		Version version) :
			base(compilation, platform, version)
	{ }

	public static RunnerUtilityContextV2? Get(
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

		return version is null ? null : new(compilation, platform, version);
	}
}
