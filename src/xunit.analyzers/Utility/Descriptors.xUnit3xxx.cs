using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.DiagnosticSeverity;
using static Xunit.Analyzers.Category;

namespace Xunit.Analyzers;

public static partial class Descriptors
{
	public static DiagnosticDescriptor X3000_CrossAppDomainClassesMustBeLongLivedMarshalByRefObject { get; } =
		Diagnostic(
			"xUnit3000",
			"Classes which cross AppDomain boundaries must derive directly or indirectly from LongLivedMarshalByRefObject",
			Extensibility,
			Error,
			"Class {0} must derive directly or indirectly from LongLivedMarshalByRefObject."
		);

	public static DiagnosticDescriptor X3001_SerializableClassMustHaveParameterlessConstructor { get; } =
		Diagnostic(
			"xUnit3001",
			"Classes that are marked as serializable (or created by the test framework at runtime) must have a public parameterless constructor",
			Extensibility,
			Error,
			"Class {0} must have a public parameterless constructor to support {1}."
		);

	public static DiagnosticDescriptor X3002_DoNotTestForConcreteTypeOfJsonSerializableTypes { get; } =
		Diagnostic(
			"xUnit3002",
			"Classes which are JSON serializable should not be tested for their concrete type",
			Extensibility,
			Warning,
			"Class {0} is JSON serializable and should not be tested for its concrete type. Test for its primary interface instead."
		);

	public static DiagnosticDescriptor X3003_ProvideConstructorForFactAttributeOverride { get; } =
		Diagnostic(
			"xUnit3003",
			"Classes which extend FactAttribute (directly or indirectly) should provide a public constructor for source information",
			Extensibility,
			Warning,
			"Class {0} extends FactAttribute. It should include a public constructor for source information."
		);

	public static DiagnosticDescriptor X3004_TypeDoesNotImplementInterface { get; } =
		Diagnostic(
			"xUnit3004",
			"The given type is missing a required interface implementation",
			Extensibility,
			Error,
			"Class {0} must implement interface {1}"
		);

	public static DiagnosticDescriptor X3005_TypeMustHaveCorrectPublicConstructor { get; } =
		Diagnostic(
			"xUnit3005",
			"Type must have an appropriate non-obsolete public constructor",
			Extensibility,
			Error,
			"Type '{0}' must have a non-obsolete public constructor: public {0}({1})"
		);

	public static DiagnosticDescriptor X3006_TestCaseImplementationsMustBeSerializable { get; } =
		Diagnostic(
			"xUnit3006",
			"Test case implementations must be serializable",
			Extensibility,
			Warning,
			"Class '{0}' implements '{1}' but is not serializable. Test cases must be serializable to support test discovery and execution. Implement '{2}', decorate with '[JsonTypeID]', or register an external IXunitSerializer."
		);

	public static DiagnosticDescriptor X3007_TestCaseImplementationsMightNotBeSerializable { get; } =
		Diagnostic(
			"xUnit3007",
			"Test case implementations might not be serializable",
			Extensibility,
			Warning,
			"Class '{0}' implements '{1}' but might not be serializable. Test cases must be serializable to support test discovery and execution. Consider implementing '{2}', decorating with '[JsonTypeID]', or registering an external IXunitSerializer."
		);

	// Placeholder for rule X3008

	// Placeholder for rule X3009
}
