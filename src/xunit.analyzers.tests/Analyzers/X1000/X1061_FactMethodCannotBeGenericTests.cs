using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.FactMethodCannotBeGeneric>;

public class X1061_FactMethodCannotBeGenericTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[Fact]
				public void NonGenericTestMethod() { }

				[Fact]
				public void [|GenericTestMethod|]<T>() { }
			}
			""";

		await Verify.VerifyAnalyzer(source);
	}

	[Fact]
	public async ValueTask V3_only()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[CulturedFact(new[] { "en-US" })]
				public void NonGenericTestMethod() { }

				[CulturedFact(new[] { "en-US" })]
				public void [|GenericTestMethod|]<T>() { }
			}
			""";

		await Verify.VerifyAnalyzerV3(source);
	}
}
