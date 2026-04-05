using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class CulturedTestMustHaveAtLeastOneCulture() :
	XunitV3DiagnosticAnalyzer(Descriptors.X1060_CulturedTestMustHaveAtLeastOneCulture)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var culturedAttributeTypes = xunitContext.V3Core.CulturedTestAttributeTypes;
		if (culturedAttributeTypes.Count == 0)
			return;

		context.RegisterSyntaxNodeAction(context =>
		{
			if (context.Node is not AttributeSyntax attributeSyntax)
				return;

			if (context.SemanticModel.GetTypeInfo(attributeSyntax, context.CancellationToken).Type is not INamedTypeSymbol attributeType
					|| !culturedAttributeTypes.Contains(attributeType)
					|| attributeSyntax.ArgumentList is null
					|| attributeSyntax.ArgumentList.Arguments.Count < 1)
				return;

			var cultures = attributeSyntax.ArgumentList.Arguments[0];

			if (cultures.Expression is ArrayCreationExpressionSyntax arraySyntax)
			{
				if (arraySyntax.Initializer is null || arraySyntax.Initializer.Expressions.Count == 0)
					reportX1060();
				return;
			}

			if (cultures.Expression is ImplicitArrayCreationExpressionSyntax implicitArraySyntax)
			{
				if (implicitArraySyntax.Initializer.Expressions.Count == 0)
					reportX1060();
				return;
			}

#if ROSLYN_LATEST
			if (cultures.Expression is CollectionExpressionSyntax collectionSyntax)
			{
				if (collectionSyntax.Elements.Count == 0)
					reportX1060();
				return;
			}
#endif

			void reportX1060() =>
				context.ReportDiagnostic(
					Diagnostic.Create(
						Descriptors.X1060_CulturedTestMustHaveAtLeastOneCulture,
						attributeSyntax.GetLocation()
					)
				);
		}, SyntaxKind.Attribute);
	}
}
