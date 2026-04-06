using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TheoryMethodCannotBeGeneric() :
	XunitV3AotDiagnosticAnalyzer(Descriptors.X1062_TheoryMethodCannotBeGeneric)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var theoryAttributeTypes = xunitContext.Core.TheoryAttributeTypes;
		if (theoryAttributeTypes.Count == 0)
			return;

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not IMethodSymbol methodSymbol)
				return;

			if (!methodSymbol.GetAttributes().Any(a => a.AttributeClass is not null && theoryAttributeTypes.Contains(a.AttributeClass)))
				return;

			if (methodSymbol.IsGenericMethod)
				context.ReportDiagnostic(
					Diagnostic.Create(
						Descriptors.X1062_TheoryMethodCannotBeGeneric,
						methodSymbol.Locations.FirstOrDefault()
					)
				);
		}, SymbolKind.Method);
	}
}
