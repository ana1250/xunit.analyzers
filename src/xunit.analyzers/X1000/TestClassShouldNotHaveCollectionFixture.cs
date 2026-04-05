using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TestClassShouldNotHaveCollectionFixture() :
	XunitDiagnosticAnalyzer(Descriptors.X1059_TestClassCannotImplementICollectionFixture)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var iCollectionFixtureType = xunitContext.Core.ICollectionFixtureType;
		if (iCollectionFixtureType is null)
			return;

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not INamedTypeSymbol typeSymbol || !typeSymbol.IsTestClass(xunitContext, strict: true))
				return;

			if (!typeSymbol.AllInterfaces.Any(i => i.IsGenericType && SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, iCollectionFixtureType)))
				return;

			context.ReportDiagnostic(
				Diagnostic.Create(
					Descriptors.X1059_TestClassCannotImplementICollectionFixture,
					typeSymbol.Locations.FirstOrDefault(),
					typeSymbol
				)
			);
		}, SymbolKind.NamedType);
	}
}
