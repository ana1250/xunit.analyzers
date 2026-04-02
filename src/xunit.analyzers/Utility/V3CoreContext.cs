using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public class V3CoreContext : V3CoreContextBase
{
	readonly Lazy<INamedTypeSymbol?> lazyIDataAttributeType;
	readonly Lazy<INamedTypeSymbol?> lazyIFactAttributeType;

	V3CoreContext(
		Compilation compilation,
		Version version) :
			base(compilation, version)
	{
		lazyIDataAttributeType = new(() => TypeSymbolFactory.IDataAttribute_V3(compilation));
		lazyIFactAttributeType = new(() => TypeSymbolFactory.IFactAttribute_V3(compilation));
	}

	/// <inheritdoc/>
	public override INamedTypeSymbol? IDataAttributeType =>
		lazyIDataAttributeType.Value;

	/// <inheritdoc/>
	public override INamedTypeSymbol? IFactAttributeType =>
		lazyIFactAttributeType.Value;

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

		return version is null ? null : new V3CoreContext(compilation, version);
	}
}
