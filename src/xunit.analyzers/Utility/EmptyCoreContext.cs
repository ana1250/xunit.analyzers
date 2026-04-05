using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class EmptyCoreContext : ICoreContext
{
	EmptyCoreContext()
	{ }

	public INamedTypeSymbol? BeforeAfterTestAttributeType => null;

	public INamedTypeSymbol? ClassDataAttributeType => null;

	public INamedTypeSymbol? CollectionAttributeType => null;

	public INamedTypeSymbol? CollectionBehaviorAttributeType => null;

	public INamedTypeSymbol? CollectionDefinitionAttributeType => null;

	public INamedTypeSymbol? CulturedFactAttributeType => null;

	public INamedTypeSymbol? CulturedTheoryAttributeType => null;

	public INamedTypeSymbol? DataAttributeType => null;

	public INamedTypeSymbol? FactAttributeType => null;

	public INamedTypeSymbol? IClassFixtureType => null;

	public INamedTypeSymbol? ICollectionFixtureType => null;

	public INamedTypeSymbol? InlineDataAttributeType => null;

	public INamedTypeSymbol? ITestCaseOrdererType => null;

	public INamedTypeSymbol? ITestCollectionFactoryType => null;

	public INamedTypeSymbol? ITestCollectionOrdererType => null;

	public INamedTypeSymbol? ITestFrameworkType => null;

	public INamedTypeSymbol? ITestOutputHelperType => null;

	public static EmptyCoreContext Instance { get; } = new();

	public INamedTypeSymbol? MemberDataAttributeType => null;

	public INamedTypeSymbol? TestCaseOrdererAttributeType => null;

	public INamedTypeSymbol? TestCollectionOrdererAttributeType => null;

	public INamedTypeSymbol? TestFrameworkAttributeType => null;

	public INamedTypeSymbol? TheoryAttributeType => null;

	public bool TheorySupportsConversionFromStringToDateTimeOffsetAndGuid => false;

	public bool TheorySupportsDefaultParameterValues => false;

	public bool TheorySupportsParameterArrays => false;

	public Version Version { get; } = new();
}
