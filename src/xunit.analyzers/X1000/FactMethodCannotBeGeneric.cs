using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class FactMethodCannotBeGeneric() :
	XunitDiagnosticAnalyzer(Descriptors.X1061_FactMethodCannotBeGeneric)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var factAttributeTypes = xunitContext.Core.FactAttributeTypes;
		if (factAttributeTypes.Count == 0)
			return;

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not IMethodSymbol methodSymbol)
				return;

			if (!methodSymbol.GetAttributes().Any(a => a.AttributeClass is not null && factAttributeTypes.Contains(a.AttributeClass)))
				return;

			if (methodSymbol.IsGenericMethod)
				context.ReportDiagnostic(
					Diagnostic.Create(
						Descriptors.X1061_FactMethodCannotBeGeneric,
						methodSymbol.Locations.FirstOrDefault()
					)
				);
		}, SymbolKind.Method);
	}
}
