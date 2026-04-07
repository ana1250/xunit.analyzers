using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TheoryParameterCannotUseParamsModifier() :
	XunitV3AotDiagnosticAnalyzer(Descriptors.X1064_TheoryParameterCannotBeParams)
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

			var paramsParam = methodSymbol.Parameters.FirstOrDefault(p => p.IsParams);
			if (paramsParam is not null)
				context.ReportDiagnostic(
					Diagnostic.Create(
						Descriptors.X1064_TheoryParameterCannotBeParams,
						paramsParam.Locations.FirstOrDefault()
					)
				);
		}, SymbolKind.Method);
	}
}
