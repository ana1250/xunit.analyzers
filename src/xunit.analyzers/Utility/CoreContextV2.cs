using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CoreContextV2 : CoreContextBase
{
	internal static readonly Version Version_2_2_0 = new("2.2.0");
	internal static readonly Version Version_2_4_0 = new("2.4.0");

	readonly Lazy<INamedTypeSymbol?> lazyDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyITestCaseOrdererType;
	readonly Lazy<INamedTypeSymbol?> lazyITestCollectionOrdererType;
	readonly Lazy<INamedTypeSymbol?> lazyITestFrameworkType;
	readonly Lazy<INamedTypeSymbol?> lazyITestOutputHelperType;
	readonly Lazy<INamedTypeSymbol?> lazyIXunitTestCollectionFactoryType;

	CoreContextV2(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{
		lazyDataAttributeType = new(() => TypeSymbolFactory.DataAttribute_V2(compilation));
		lazyITestCaseOrdererType = new(() => TypeSymbolFactory.ITestCaseOrderer_V2(compilation));
		lazyITestCollectionOrdererType = new(() => TypeSymbolFactory.ITestCollectionOrderer_V2(compilation));
		lazyITestFrameworkType = new(() => TypeSymbolFactory.ITestFramework_V2(compilation));
		lazyITestOutputHelperType = new(() => TypeSymbolFactory.ITestOutputHelper_V2(compilation));
		lazyIXunitTestCollectionFactoryType = new(() => TypeSymbolFactory.IXunitTestCollectionFactory_V2(compilation));
	}

	public override INamedTypeSymbol? CulturedFactAttributeType => null;

	public override INamedTypeSymbol? CulturedTheoryAttributeType => null;

	public override INamedTypeSymbol? DataAttributeType =>
		lazyDataAttributeType.Value;

	public override INamedTypeSymbol? ITestCaseOrdererType =>
		lazyITestCaseOrdererType.Value;

	public override INamedTypeSymbol? ITestCollectionFactoryType =>
		lazyIXunitTestCollectionFactoryType.Value;

	public override INamedTypeSymbol? ITestCollectionOrdererType =>
		lazyITestCollectionOrdererType.Value;

	public override INamedTypeSymbol? ITestFrameworkType =>
		lazyITestFrameworkType.Value;

	public override INamedTypeSymbol? ITestOutputHelperType =>
		lazyITestOutputHelperType.Value;

	// See: https://github.com/xunit/xunit/pull/1546
	public override bool TheorySupportsConversionFromStringToDateTimeOffsetAndGuid =>
		Version >= Version_2_4_0;

	public override bool TheorySupportsDefaultParameterValues =>
		Version >= Version_2_2_0;

	public override bool TheorySupportsParameterArrays =>
		Version >= Version_2_2_0;

	public static CoreContextV2? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.core", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new(compilation, version);
	}
}
