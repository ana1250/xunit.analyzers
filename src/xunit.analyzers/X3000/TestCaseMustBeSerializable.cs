using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xunit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TestCaseMustBeSerializable : XunitDiagnosticAnalyzer
{
	public TestCaseMustBeSerializable() :
		base(
			Descriptors.X3006_TestCaseImplementationsMustBeSerializable,
			Descriptors.X3007_TestCaseImplementationsMightNotBeSerializable
		)
	{ }

	public override void AnalyzeCompilation(
		CompilationStartAnalysisContext context,
		XunitContext xunitContext)
	{
		Guard.ArgumentNotNull(context);
		Guard.ArgumentNotNull(xunitContext);

		var iTestCaseType = xunitContext.Common.ITestCaseType;
		if (iTestCaseType is null)
			return;

		context.RegisterSymbolAction(context =>
		{
			if (context.Symbol is not INamedTypeSymbol namedType)
				return;
			if (namedType.TypeKind != TypeKind.Class)
				return;
			if (namedType.IsAbstract)
				return;

			if (!iTestCaseType.IsAssignableFrom(namedType))
				return;

			// Types that implement IXunitSerializable
			if (xunitContext.Common.IXunitSerializableType?.IsAssignableFrom(namedType) == true)
				return;

			// Types that decorate with [JsonTypeID]
			if (xunitContext.V3Core?.JsonTypeIDAttributeType is INamedTypeSymbol jsonTypeIDAttributeType)
				if (namedType.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, jsonTypeIDAttributeType)))
					return;

			var iXunitSerializableDisplay =
				xunitContext.Common.IXunitSerializableType?.ToDisplayString()
				?? "IXunitSerializable";

			var isDefinitelyNotSerializable = namedType.IsSealed;

			context.ReportDiagnostic(
				Diagnostic.Create(
					isDefinitelyNotSerializable
						? Descriptors.X3006_TestCaseImplementationsMustBeSerializable
						: Descriptors.X3007_TestCaseImplementationsMightNotBeSerializable,
					namedType.Locations.First(),
					namedType.Name,
					iTestCaseType.ToDisplayString(),
					iXunitSerializableDisplay
				)
			);
		}, SymbolKind.NamedType);
	}
}
