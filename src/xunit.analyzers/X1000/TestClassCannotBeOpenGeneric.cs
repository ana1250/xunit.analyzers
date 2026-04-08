using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TestClassCannotBeOpenGeneric() :
	XunitDiagnosticAnalyzer(Descriptors.X1063_TestClassCannotBeOpenGeneric)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not INamedTypeSymbol typeSymbol || typeSymbol.IsAbstract)
				return;

			if (!typeSymbol.IsTestClass(xunitContext, strict: true))
				return;

			if (typeSymbol.IsGenericType && typeSymbol.TypeArguments.Any(a => a.Kind == SymbolKind.TypeParameter))
				context.ReportDiagnostic(
					Diagnostic.Create(
						Descriptors.X1063_TestClassCannotBeOpenGeneric,
						typeSymbol.Locations.FirstOrDefault()
					)
				);
		}, SymbolKind.NamedType);
	}
}
