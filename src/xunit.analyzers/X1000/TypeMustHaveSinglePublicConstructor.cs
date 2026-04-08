using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TypeMustHaveSinglePublicConstructor() :
	XunitDiagnosticAnalyzer(Descriptors.X1056_TypeMustHaveSinglePublicConstructor)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var fixtureTypes = new[] {
			xunitContext.Core.IClassFixtureType,
			xunitContext.Core.ICollectionFixtureType,
		}.WhereNotNull().ToImmutableHashSet(SymbolEqualityComparer.Default);

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not INamedTypeSymbol typeSymbol)
				return;

			if (typeSymbol.IsTestClass(xunitContext, strict: false))
				verifyType(typeSymbol, "Test class", allowStatic: true);

			foreach (var @interface in typeSymbol.AllInterfaces)
				if (@interface.IsGenericType && fixtureTypes.Contains(@interface.OriginalDefinition))
					if (@interface.TypeArguments.Length == 1 && @interface.TypeArguments[0] is INamedTypeSymbol fixtureType)
						verifyType(fixtureType, "Fixture", allowStatic: false);

			void verifyType(
				INamedTypeSymbol type,
				string typeDescription,
				bool allowStatic)
			{
				if (allowStatic && type.IsStatic)
					return;

				var publicCtors = type.Constructors.Where(c => c.DeclaredAccessibility == Accessibility.Public && !c.IsStatic).ToArray();
				if (publicCtors.Length != 1)
					context.ReportDiagnostic(
						Diagnostic.Create(
							Descriptors.X1056_TypeMustHaveSinglePublicConstructor,
							typeSymbol.Locations.FirstOrDefault(),
							typeDescription,
							type
						)
					);
			}
		}, SymbolKind.NamedType);
	}
}
