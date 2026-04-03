using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CoreContextV3 : CoreContextV3Base
{
	readonly Lazy<INamedTypeSymbol?> lazyIDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyIFactAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyIXunitTestCollectionFactoryType;

	CoreContextV3(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{
		lazyIDataAttributeType = new(() => TypeSymbolFactory.IDataAttribute_V3(compilation));
		lazyIFactAttributeType = new(() => TypeSymbolFactory.IFactAttribute_V3(compilation));
		lazyIXunitTestCollectionFactoryType = new(() => TypeSymbolFactory.IXunitTestCollectionFactory_V3(compilation));
	}

	public override INamedTypeSymbol? IDataAttributeType =>
		lazyIDataAttributeType.Value;

	public override INamedTypeSymbol? IFactAttributeType =>
		lazyIFactAttributeType.Value;

	public override INamedTypeSymbol? ITestCollectionFactoryType =>
		lazyIXunitTestCollectionFactoryType.Value;

	public static ICoreContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.core", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new CoreContextV3(compilation, version);
	}
}
