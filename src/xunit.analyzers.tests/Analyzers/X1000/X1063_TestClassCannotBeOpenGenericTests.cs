using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TestClassCannotBeOpenGeneric>;

public class X1063_TestClassCannotBeOpenGenericTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class NonGenericTestClass {
				[Fact]
				public void TestMethod() { }
			}

			public class [|OpenGenericBase|]<T> {
				[Fact]
				public void TestMethod() { }
			}

			public abstract class AbstractOpenGenericBase<T> {
				[Fact]
				public void TestMethod() { }
			}

			public class ClosedGenericTestClass : OpenGenericBase<int> {
				[Fact]
				public void DerivedTestMethod() { }
			}
			""";

		await Verify.VerifyAnalyzer(source);
	}
}
