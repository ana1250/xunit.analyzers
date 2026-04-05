using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TestClassShouldNotHaveCollectionFixture>;

public class X1059_TestClassShouldNotHaveCollectionFixtureTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class NonTestClass : ICollectionFixture<object> { }

			public class WithClassFixture : IClassFixture<object> {
				[Fact] public void TestMethod() { }
			}

			public class {|#0:WithCollectionFixture|} : ICollectionFixture<object> {
				[Fact] public void TestMethod() { }
			}
			""";

		await Verify.VerifyAnalyzer(source, Verify.Diagnostic().WithLocation(0).WithArguments("WithCollectionFixture"));
	}
}
