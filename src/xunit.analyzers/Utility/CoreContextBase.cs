using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public abstract class CoreContextBase(
	Compilation compilation,
	Version version) :
		ICoreContext
{
	readonly Lazy<INamedTypeSymbol?> lazyClassDataAttributeType = new(() => TypeSymbolFactory.ClassDataAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyCollectionAttributeType = new(() => TypeSymbolFactory.CollectionAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyCollectionBehaviorAttributeType = new(() => TypeSymbolFactory.CollectionBehaviorAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyCollectionDefinitionAttributeType = new(() => TypeSymbolFactory.CollectionDefinitionAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyFactAttributeType = new(() => TypeSymbolFactory.FactAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyIClassFixtureType = new(() => TypeSymbolFactory.IClassFixureOfT(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyICollectionFixtureType = new(() => TypeSymbolFactory.ICollectionFixtureOfT(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyInlineDataAttributeType = new(() => TypeSymbolFactory.InlineDataAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyMemberDataAttributeType = new(() => TypeSymbolFactory.MemberDataAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyTestCaseOrdererAttributeType = new(() => TypeSymbolFactory.TestCaseOrdererAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyTestCollectionOrdererAttributeType = new(() => TypeSymbolFactory.TestCollectionOrdererAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyTestFrameworkAttributeType = new(() => TypeSymbolFactory.TestFrameworkAttribute(compilation));
	readonly Lazy<INamedTypeSymbol?> lazyTheoryAttributeType = new(() => TypeSymbolFactory.TheoryAttribute(compilation));

	public INamedTypeSymbol? ClassDataAttributeType =>
		lazyClassDataAttributeType.Value;

	public INamedTypeSymbol? CollectionAttributeType =>
		lazyCollectionAttributeType.Value;

	public INamedTypeSymbol? CollectionBehaviorAttributeType =>
		lazyCollectionBehaviorAttributeType.Value;

	public INamedTypeSymbol? CollectionDefinitionAttributeType =>
		lazyCollectionDefinitionAttributeType.Value;

	public abstract INamedTypeSymbol? CulturedFactAttributeType { get; }

	public abstract INamedTypeSymbol? CulturedTheoryAttributeType { get; }

	public abstract INamedTypeSymbol? DataAttributeType { get; }

	public INamedTypeSymbol? FactAttributeType =>
		lazyFactAttributeType.Value;

	public INamedTypeSymbol? IClassFixtureType =>
		lazyIClassFixtureType.Value;

	public INamedTypeSymbol? ICollectionFixtureType =>
		lazyICollectionFixtureType.Value;

	public INamedTypeSymbol? InlineDataAttributeType =>
		lazyInlineDataAttributeType.Value;

	public abstract INamedTypeSymbol? ITestCaseOrdererType { get; }

	public abstract INamedTypeSymbol? ITestCollectionFactoryType { get; }

	public abstract INamedTypeSymbol? ITestCollectionOrdererType { get; }

	public abstract INamedTypeSymbol? ITestFrameworkType { get; }

	public abstract INamedTypeSymbol? ITestOutputHelperType { get; }

	public INamedTypeSymbol? MemberDataAttributeType =>
		lazyMemberDataAttributeType.Value;

	public INamedTypeSymbol? TestCaseOrdererAttributeType =>
		lazyTestCaseOrdererAttributeType.Value;

	public INamedTypeSymbol? TestCollectionOrdererAttributeType =>
		lazyTestCollectionOrdererAttributeType.Value;

	public INamedTypeSymbol? TestFrameworkAttributeType =>
		lazyTestFrameworkAttributeType.Value;

	public INamedTypeSymbol? TheoryAttributeType =>
		lazyTheoryAttributeType.Value;

	public abstract bool TheorySupportsConversionFromStringToDateTimeOffsetAndGuid { get; }

	public abstract bool TheorySupportsDefaultParameterValues { get; }

	public abstract bool TheorySupportsParameterArrays { get; }

	public Version Version { get; } = version;
}
