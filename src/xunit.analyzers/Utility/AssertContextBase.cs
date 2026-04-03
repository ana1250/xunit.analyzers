using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public abstract class AssertContextBase(
	Compilation compilation,
	Version version) :
		IAssertContext
{
	readonly Lazy<INamedTypeSymbol?> lazyAssertType = new(() => TypeSymbolFactory.Assert(compilation));

	public INamedTypeSymbol? AssertType =>
		lazyAssertType.Value;

	public abstract bool SupportsAssertFail { get; }

	public abstract bool SupportsAssertNullWithPointers { get; }

	public abstract bool SupportsInexactTypeAssertions { get; }

	public Version Version { get; } = version;
}
