using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public abstract class RunnerUtilityContextBase(
	Compilation compilation,
	string platform,
	Version version) :
		IRunnerUtilityContext
{
	readonly Lazy<INamedTypeSymbol?> lazyLongLivedMarshalByRefObjectType = new(() => TypeSymbolFactory.LongLivedMarshalByRefObject_RunnerUtility(compilation));

	public INamedTypeSymbol? LongLivedMarshalByRefObjectType =>
		lazyLongLivedMarshalByRefObjectType.Value;

	public string Platform { get; } = platform;

	public Version Version { get; } = version;
}
