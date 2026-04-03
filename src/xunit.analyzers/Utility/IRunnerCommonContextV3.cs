using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

/// <summary>
/// Context for types that live in one of <c>xunit.v3.runner.common</c> or <c>xunit.v3.runner.common.aot</c>.
/// </summary>
public interface IRunnerCommonContextV3
{
	/// <summary>
	/// Gets a reference to type <c>IConsoleResultWriter</c>, if available.
	/// </summary>
	INamedTypeSymbol? IConsoleResultWriterType { get; }

	/// <summary>
	/// Gets a reference to type <c>IMicrosoftTestingPlatformResultWriter</c>, if available.
	/// </summary>
	INamedTypeSymbol? IMicrosoftTestingPlatformResultWriterType { get; }

	/// <summary>
	/// Gets a reference to type <c>IRunnerReporter</c>, if available.
	/// </summary>
	INamedTypeSymbol? IRunnerReporterType { get; }

	/// <summary>
	/// Gets a reference to type <c>RegisterConsoleResultWriterAttribute</c>, if available.
	/// </summary>
	INamedTypeSymbol? RegisterConsoleResultWriterAttributeType { get; }

	/// <summary>
	/// Gets a reference to type <c>RegisterMicrosoftTestingPlatformResultWriterAttribute</c>, if available.
	/// </summary>
	INamedTypeSymbol? RegisterMicrosoftTestingPlatformResultWriterAttributeType { get; }

	/// <summary>
	/// Gets a reference to type <c>RegisterResultWriterAttribute</c>, if available.
	/// </summary>
	INamedTypeSymbol? RegisterResultWriterAttributeType { get; }

	/// <summary>
	/// Gets a reference to type <c>RegisterRunnerReporterAttribute</c>, if available.
	/// </summary>
	INamedTypeSymbol? RegisterRunnerReporterAttributeType { get; }

	/// <summary>
	/// Gets the version number of the runner common assembly.
	/// </summary>
	Version Version { get; }
}
