using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3RunnerUtilityContextBase : IRunnerUtilityContextV3
{
	readonly Lazy<INamedTypeSymbol?> lazyLongLivedMarshalByRefObjectType;

	protected V3RunnerUtilityContextBase(
		Compilation compilation,
		string platform,
		Version version)
	{
		Platform = platform;
		Version = version;

		lazyLongLivedMarshalByRefObjectType = new(() => TypeSymbolFactory.LongLivedMarshalByRefObject_RunnerUtility(compilation));
	}

	/// <inheritdoc/>
	public INamedTypeSymbol? LongLivedMarshalByRefObjectType =>
		lazyLongLivedMarshalByRefObjectType.Value;

	/// <inheritdoc/>
	public string Platform { get; }

	/// <inheritdoc/>
	public Version Version { get; }
}
