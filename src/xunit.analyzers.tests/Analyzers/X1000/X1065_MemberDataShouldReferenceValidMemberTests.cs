using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.MemberDataShouldReferenceValidMember>;

public class X1065_MemberDataShouldReferenceValidMemberTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			#pragma warning disable xUnit1042

			using System.Collections.Generic;
			using Xunit;

			public class TestClass {
				public static IEnumerable<object[]> DataMethod() => DataMethod("Hello world");
				public static IEnumerable<object[]> DataMethod(string s) => new object[][] { new object[] { s } };

				[Theory]
				[{|#0:MemberData(nameof(DataMethod))|}]
				public void WithoutArgs(string _) { }

				[Theory]
				[{|#1:MemberData(nameof(DataMethod), "Heyo")|}]
				public void WithArgs(string _) { }
			}
			""";

		await Verify.VerifyAnalyzerNonAot(source);
#if NETCOREAPP && ROSLYN_LATEST
		var expectedAot = new[] {
			Verify.Diagnostic("xUnit1065").WithLocation(0).WithArguments("TestClass", "DataMethod"),
			Verify.Diagnostic("xUnit1065").WithLocation(1).WithArguments("TestClass", "DataMethod"),
		};
		await Verify.VerifyAnalyzerV3Aot(source, expectedAot);
#endif
	}
}
