using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3RunnerCommonContextBase : IRunnerCommonContextV3
{
	readonly Lazy<INamedTypeSymbol?> lazyIRunnerReporterType;

	protected V3RunnerCommonContextBase(
		Compilation compilation,
		Version version)
	{
		Version = version;

		lazyIRunnerReporterType = new(() => TypeSymbolFactory.IRunnerReporter_V3(compilation));
	}

	/// <inheritdoc/>
	public INamedTypeSymbol? IRunnerReporterType =>
		lazyIRunnerReporterType.Value;

	/// <inheritdoc/>
	public Version Version { get; }
}
