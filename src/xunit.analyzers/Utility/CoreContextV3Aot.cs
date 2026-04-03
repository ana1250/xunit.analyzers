using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class CoreContextV3Aot : CoreContextV3Base
{
	readonly Lazy<INamedTypeSymbol?> lazyICodeGenTestCollectionFactoryType;

	CoreContextV3Aot(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{
		lazyICodeGenTestCollectionFactoryType = new(() => TypeSymbolFactory.ICodeGenTestCollectionFactory_V3(compilation));
	}

	/// <remarks>
	/// This will always return <see langword="null"/> for Native AOT, since this interface is obsolete.
	/// </remarks>
	/// <inheritdoc/>
	public override INamedTypeSymbol? IDataAttributeType =>
		null;

	/// <remarks>
	/// This will always return <see langword="null"/> for Native AOT, since this interface is obsolete.
	/// </remarks>
	/// <inheritdoc/>
	public override INamedTypeSymbol? IFactAttributeType =>
		null;

	public override INamedTypeSymbol? ITestCollectionFactoryType =>
		lazyICodeGenTestCollectionFactoryType.Value;

	public static ICoreContextV3? Get(
		Compilation compilation,
		Version? versionOverride = null)
	{
		Guard.ArgumentNotNull(compilation);

		var version =
			versionOverride ??
			compilation
				.ReferencedAssemblyNames
				.FirstOrDefault(a => a.Name.Equals("xunit.v3.core.aot", StringComparison.OrdinalIgnoreCase))
				?.Version;

		return version is null ? null : new CoreContextV3Aot(compilation, version);
	}
}
