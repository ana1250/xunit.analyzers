using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public abstract class V3CoreContextBase : ICoreContextV3
{
	readonly Lazy<INamedTypeSymbol?> lazyAssemblyFixtureAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyClassDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyClassDataAttributeOfTType;
	readonly Lazy<INamedTypeSymbol?> lazyCollectionAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyCollectionAttributeOfTType;
	readonly Lazy<INamedTypeSymbol?> lazyCollectionDefinitionAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyCulturedFactAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyCulturedTheoryAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyFactAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyIClassFixtureType;
	readonly Lazy<INamedTypeSymbol?> lazyICollectionFixtureType;
	readonly Lazy<INamedTypeSymbol?> lazyInlineDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyITestContextAccessorType;
	readonly Lazy<INamedTypeSymbol?> lazyITestOutputHelperType;
	readonly Lazy<INamedTypeSymbol?> lazyJsonTypeIDAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyMemberDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyTheoryAttributeType;

	protected V3CoreContextBase(
		Compilation compilation,
		Version version)
	{
		Version = version;

		lazyAssemblyFixtureAttributeType = new(() => TypeSymbolFactory.AssemblyFixtureAttribute_V3(compilation));
		lazyClassDataAttributeType = new(() => TypeSymbolFactory.ClassDataAttribute(compilation));
		lazyClassDataAttributeOfTType = new(() => TypeSymbolFactory.ClassDataAttributeOfT_V3(compilation));
		lazyCollectionAttributeType = new(() => TypeSymbolFactory.CollectionAttribute(compilation));
		lazyCollectionAttributeOfTType = new(() => TypeSymbolFactory.CollectionAttributeOfT_V3(compilation));
		lazyCollectionDefinitionAttributeType = new(() => TypeSymbolFactory.CollectionDefinitionAttribute(compilation));
		lazyCulturedFactAttributeType = new(() => TypeSymbolFactory.CulturedFactAttribute_V3(compilation));
		lazyCulturedTheoryAttributeType = new(() => TypeSymbolFactory.CulturedTheoryAttribute_V3(compilation));
		lazyDataAttributeType = new(() => TypeSymbolFactory.DataAttribute_V3(compilation));
		lazyFactAttributeType = new(() => TypeSymbolFactory.FactAttribute(compilation));
		lazyIClassFixtureType = new(() => TypeSymbolFactory.IClassFixureOfT(compilation));
		lazyICollectionFixtureType = new(() => TypeSymbolFactory.ICollectionFixtureOfT(compilation));
		lazyInlineDataAttributeType = new(() => TypeSymbolFactory.InlineDataAttribute(compilation));
		lazyITestContextAccessorType = new(() => TypeSymbolFactory.ITestContextAccessor_V3(compilation));
		lazyITestOutputHelperType = new(() => TypeSymbolFactory.ITestOutputHelper_V3(compilation));
		lazyJsonTypeIDAttributeType = new(() => TypeSymbolFactory.JsonTypeIDAttribute_V3(compilation));
		lazyMemberDataAttributeType = new(() => TypeSymbolFactory.MemberDataAttribute(compilation));
		lazyTheoryAttributeType = new(() => TypeSymbolFactory.TheoryAttribute(compilation));
	}

	/// <inheritdoc/>
	public INamedTypeSymbol? AssemblyFixtureAttributeType =>
		lazyAssemblyFixtureAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? ClassDataAttributeType =>
		lazyClassDataAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? ClassDataAttributeOfTType =>
		lazyClassDataAttributeOfTType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? CollectionAttributeType =>
		lazyCollectionAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? CollectionAttributeOfTType =>
		lazyCollectionAttributeOfTType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? CollectionDefinitionAttributeType =>
		lazyCollectionDefinitionAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? CulturedFactAttributeType =>
		lazyCulturedFactAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? CulturedTheoryAttributeType =>
		lazyCulturedTheoryAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? DataAttributeType =>
		lazyDataAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? FactAttributeType =>
		lazyFactAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? IClassFixtureType =>
		lazyIClassFixtureType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? ICollectionFixtureType =>
		lazyICollectionFixtureType.Value;

	/// <inheritdoc/>
	public abstract INamedTypeSymbol? IDataAttributeType { get; }

	/// <inheritdoc/>
	public abstract INamedTypeSymbol? IFactAttributeType { get; }

	/// <inheritdoc/>
	public INamedTypeSymbol? InlineDataAttributeType =>
		lazyInlineDataAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? ITestContextAccessorType =>
		lazyITestContextAccessorType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? ITestOutputHelperType =>
		lazyITestOutputHelperType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? JsonTypeIDAttributeType =>
		lazyJsonTypeIDAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? MemberDataAttributeType =>
		lazyMemberDataAttributeType.Value;

	/// <inheritdoc/>
	public INamedTypeSymbol? TheoryAttributeType =>
		lazyTheoryAttributeType.Value;

	/// <inheritdoc/>
	public bool TheorySupportsConversionFromStringToDateTimeOffsetAndGuid => true;

	/// <inheritdoc/>
	public bool TheorySupportsDefaultParameterValues => true;

	/// <inheritdoc/>
	public bool TheorySupportsParameterArrays => true;

	/// <inheritdoc/>
	public Version Version { get; }
}
