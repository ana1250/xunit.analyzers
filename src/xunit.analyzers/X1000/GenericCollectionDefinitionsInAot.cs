using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class GenericCollectionDefinitionsInAot() :
	XunitV3AotDiagnosticAnalyzer(Descriptors.X1058_GenericCollectionDefinitionNotSupported)
{
	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var collectionDefinitionAttributeType = xunitContext.V3Core?.CollectionDefinitionAttributeType;
		if (collectionDefinitionAttributeType is null)
			return;

		context.RegisterSyntaxNodeAction(context =>
		{
			if (context.Node is not AttributeSyntax attributeSyntax)
				return;

			if (context.SemanticModel.GetTypeInfo(attributeSyntax, context.CancellationToken).Type is not INamedTypeSymbol attributeType
					|| !SymbolEqualityComparer.Default.Equals(attributeType, collectionDefinitionAttributeType)
					|| context.ContainingSymbol is not INamedTypeSymbol collectionType
					|| !collectionType.IsGenericType)
				return;

			context.ReportDiagnostic(
				Diagnostic.Create(
					Descriptors.X1058_GenericCollectionDefinitionNotSupported,
					collectionType.Locations.FirstOrDefault()
				)
			);
		}, SyntaxKind.Attribute);
	}
}
