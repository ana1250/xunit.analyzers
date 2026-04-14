using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TestCaseMustBeSerializable>;

public class X3006_TestCaseMustBeSerializableTests
{
	static string CS0535(string interfaceName, int memberCount)
	{
		var result = interfaceName;

		while (memberCount-- > 0)
			result = $"{{|CS0535:{result}|}}";

		return result;
	}

	[Fact]
	public async ValueTask ClassNotImplementingITestCase_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ """
			public class NotATestCase { }
			""";

		await Verify.VerifyAnalyzerV2(source);
		await Verify.VerifyAnalyzerV3(source);
	}

	[Fact]
	public async ValueTask V2_TestCaseAlwaysSerializable_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Abstractions;

			public abstract class MyAbstractTestCase : {{CS0535("ITestCase", 9)}} { }

			public class MyTestCase : {{CS0535("ITestCase", 9)}} { }

			public sealed class MySealedTestCase : {{CS0535("ITestCase", 9)}} { }
			""";

		await Verify.VerifyAnalyzerV2(source);
	}

	[Fact]
	public async ValueTask V3_AbstractClassImplementingITestCase_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			public abstract class MyAbstractTestCase : {{CS0535("ITestCase", 19)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}

	[Fact]
	public async ValueTask V3_TestCaseWithIXunitSerializable_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			public sealed class MyTestCase : {{CS0535("ITestCase", 19)}}, {{CS0535("IXunitSerializable", 2)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}

	[Fact]
	public async ValueTask V3_TestCaseWithJsonTypeID_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			[JsonTypeID("my-test-case")]
			public sealed class MyTestCase : {{CS0535("ITestCase", 19)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}

	[Fact]
	public async ValueTask V3_SealedTestCaseNotSerializable_Triggers()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			public sealed class {|#0:MyTestCase|} : {{CS0535("ITestCase", 19)}} { }
			""";
		var expected = Verify.Diagnostic("xUnit3006")
			.WithLocation(0)
			.WithArguments("MyTestCase", "Xunit.Sdk.ITestCase", "Xunit.Sdk.IXunitSerializable");

		await Verify.VerifyAnalyzerV3NonAot(source, expected);
	}
}
