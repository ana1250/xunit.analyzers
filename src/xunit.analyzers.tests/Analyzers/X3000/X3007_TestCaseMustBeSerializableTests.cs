using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TestCaseMustBeSerializable>;

public class X3007_TestCaseMustBeSerializableTests
{
	static string CS0535(string interfaceName, int memberCount)
	{
		var result = interfaceName;

		while (memberCount-- > 0)
			result = $"{{|CS0535:{result}|}}";

		return result;
	}

	[Fact]
	public async ValueTask V3_UnsealedTestCaseNotSerializable_Triggers()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			public class {|#0:MyTestCase|} : {{CS0535("ITestCase", 19)}} { }
			""";
		var expected = Verify.Diagnostic("xUnit3007")
			.WithLocation(0)
			.WithArguments("MyTestCase", "Xunit.Sdk.ITestCase", "Xunit.Sdk.IXunitSerializable");

		await Verify.VerifyAnalyzerV3NonAot(source, expected);
	}

	[Fact]
	public async ValueTask V3_UnsealedTestCaseWithIXunitSerializable_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			public class MyTestCase : {{CS0535("ITestCase", 19)}}, {{CS0535("IXunitSerializable", 2)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}

	[Fact]
	public async ValueTask V3_UnsealedTestCaseWithJsonTypeID_DoesNotTrigger()
	{
		var source = /* lang=c#-test */ $$"""
			using Xunit.Sdk;

			[JsonTypeID("my-test-case")]
			public class MyTestCase : {{CS0535("ITestCase", 19)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}
}
