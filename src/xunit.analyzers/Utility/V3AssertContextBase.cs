using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3AssertContextBase : IAssertContextV3
{
	internal static readonly Version Version_0_6_0 = new("0.6.0");
	internal static readonly Version Version_3_0_1 = new("3.0.1");

	readonly Lazy<INamedTypeSymbol?> lazyAssertType;

	protected V3AssertContextBase(
		Compilation compilation,
		Version version)
	{
		Version = version;

		lazyAssertType = new(() => TypeSymbolFactory.Assert(compilation));
	}

	/// <inheritdoc/>
	public INamedTypeSymbol? AssertType =>
		lazyAssertType.Value;

	/// <inheritdoc/>
	public bool SupportsAssertFail => true;

	/// <inheritdoc/>
	public bool SupportsAssertNullWithPointers =>
		Version >= Version_3_0_1;

	/// <inheritdoc/>
	public bool SupportsInexactTypeAssertions =>
		Version >= Version_0_6_0;

	/// <inheritdoc/>
	public Version Version { get; }
}
