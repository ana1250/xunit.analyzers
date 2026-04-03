using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CommonContextV3Base : ICommonContextV3
{
	readonly Lazy<INamedTypeSymbol?> lazyIMessageSinkType;
	readonly Lazy<INamedTypeSymbol?> lazyISourceInformationProviderType;
	readonly Lazy<INamedTypeSymbol?> lazyITestAssemblyType;
	readonly Lazy<INamedTypeSymbol?> lazyITestCaseType;
	readonly Lazy<INamedTypeSymbol?> lazyITestClassType;
	readonly Lazy<INamedTypeSymbol?> lazyITestCollectionType;
	readonly Lazy<INamedTypeSymbol?> lazyITestFrameworkDiscovererType;
	readonly Lazy<INamedTypeSymbol?> lazyITestFrameworkExecutorType;
	readonly Lazy<INamedTypeSymbol?> lazyITestFrameworkType;
	readonly Lazy<INamedTypeSymbol?> lazyITestMethodType;
	readonly Lazy<INamedTypeSymbol?> lazyITestType;
	readonly Lazy<INamedTypeSymbol?> lazyIXunitSerializableType;
	readonly Lazy<INamedTypeSymbol?> lazyIXunitSerializerType;
	readonly Lazy<INamedTypeSymbol?> lazyRegisterXunitSerializerAttributeType;

	protected CommonContextV3Base(
		Compilation compilation,
		Version version)
	{
		Version = version;

		lazyIMessageSinkType = new(() => TypeSymbolFactory.IMessageSink_V3(compilation));
		lazyISourceInformationProviderType = new(() => TypeSymbolFactory.ISourceInformationProvider_V3(compilation));
		lazyITestAssemblyType = new(() => TypeSymbolFactory.ITestAssembly_V3(compilation));
		lazyITestCaseType = new(() => TypeSymbolFactory.ITestCase_V3(compilation));
		lazyITestClassType = new(() => TypeSymbolFactory.ITestClass_V3(compilation));
		lazyITestCollectionType = new(() => TypeSymbolFactory.ITestCollection_V3(compilation));
		lazyITestFrameworkDiscovererType = new(() => TypeSymbolFactory.ITestFrameworkDiscoverer_V3(compilation));
		lazyITestFrameworkExecutorType = new(() => TypeSymbolFactory.ITestFrameworkExecutor_V3(compilation));
		lazyITestFrameworkType = new(() => TypeSymbolFactory.ITestFramework_V3(compilation));
		lazyITestMethodType = new(() => TypeSymbolFactory.ITestMethod_V3(compilation));
		lazyITestType = new(() => TypeSymbolFactory.ITest_V3(compilation));
		lazyIXunitSerializableType = new(() => TypeSymbolFactory.IXunitSerializable_V3(compilation));
		lazyIXunitSerializerType = new(() => TypeSymbolFactory.IXunitSerializer_V3(compilation));
		lazyRegisterXunitSerializerAttributeType = new(() => TypeSymbolFactory.RegisterXunitSerializerAttribute_V3(compilation));
	}

	public INamedTypeSymbol? IMessageSinkType =>
		lazyIMessageSinkType.Value;

	public INamedTypeSymbol? ISourceInformationProviderType =>
		lazyISourceInformationProviderType.Value;

	public INamedTypeSymbol? ITestAssemblyType =>
		lazyITestAssemblyType.Value;

	public INamedTypeSymbol? ITestCaseType =>
		lazyITestCaseType.Value;

	public INamedTypeSymbol? ITestClassType =>
		lazyITestClassType.Value;

	public INamedTypeSymbol? ITestCollectionType =>
		lazyITestCollectionType.Value;

	public INamedTypeSymbol? ITestFrameworkDiscovererType =>
		lazyITestFrameworkDiscovererType.Value;

	public INamedTypeSymbol? ITestFrameworkExecutorType =>
		lazyITestFrameworkExecutorType.Value;

	public INamedTypeSymbol? ITestFrameworkType =>
		lazyITestFrameworkType.Value;

	public INamedTypeSymbol? ITestMethodType =>
		lazyITestMethodType.Value;

	public INamedTypeSymbol? ITestType =>
		lazyITestType.Value;

	public INamedTypeSymbol? IXunitSerializableType =>
		lazyIXunitSerializableType.Value;

	public INamedTypeSymbol? IXunitSerializerType =>
		lazyIXunitSerializerType.Value;

	public INamedTypeSymbol? RegisterXunitSerializerAttributeType =>
		lazyRegisterXunitSerializerAttributeType.Value;

	public Version Version { get; }
}
