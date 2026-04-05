using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TypeMustBePublicOrInternal>;

#if ROSLYN_LATEST
using Microsoft.CodeAnalysis.CSharp;
#endif

public class X1057_TypeMustBePublicOrInternalTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			using Xunit;
			using Xunit.v3;

			public class WithBeforeAfter
			{
				[Fact, {|#0:MyBeforeAfter|}]
				public void TestMethod() { }

				class MyBeforeAfter : BeforeAfterTestAttribute { }
			}

			public class {|#1:WithCollectionFixture|} : ICollectionFixture<WithCollectionFixture.PrivateClass>
			{
				class PrivateClass { }
			}

			public class {|#2:WithClassFixture|} : IClassFixture<WithClassFixture.PrivateClass>
			{
				class PrivateClass { }
			}
			""";

		await Verify.VerifyAnalyzerV2(source.Replace("Xunit.v3", "Xunit.Sdk"));
		await Verify.VerifyAnalyzerV3NonAot(source);

#if NETCOREAPP && ROSLYN_LATEST
		var expectedAot = new[] {
			Verify.Diagnostic().WithLocation(0).WithArguments("Attribute", "WithBeforeAfter.MyBeforeAfter"),
			Verify.Diagnostic().WithLocation(1).WithArguments("Fixture", "WithCollectionFixture.PrivateClass"),
			Verify.Diagnostic().WithLocation(2).WithArguments("Fixture", "WithClassFixture.PrivateClass"),
		};

		await Verify.VerifyAnalyzerV3Aot(source, expectedAot);
#endif
	}

	[Fact]
	public async ValueTask V3_only()
	{
		var source = /* lang=c#-test */ """
			using Xunit;
			using Xunit.v3;

			public class WithSkipExceptions
			{
				[Fact(SkipExceptions = new[] { {|#0:typeof(PrivateException)|} })]
				public void WithNewArraySyntax() { }

				class PrivateException { }
			}
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);

#if NETCOREAPP && ROSLYN_LATEST
		await Verify.VerifyAnalyzerV3Aot(source, Verify.Diagnostic().WithLocation(0).WithArguments("Exception", "WithSkipExceptions.PrivateException"));
#endif
	}

#if ROSLYN_LATEST  // Need C# 12 for collection expression

	[Fact]
	public async ValueTask V3_only_CSharp12()
	{
		var source = /* lang=c#-test */ """
			using Xunit;
			using Xunit.v3;

			public class WithSkipExceptions
			{
				[Fact(SkipExceptions = [{|#0:typeof(PrivateException)|}])]
				public void WithExpressionSyntax() { }

				class PrivateException { }
			}
			""";

		await Verify.VerifyAnalyzerV3NonAot(LanguageVersion.CSharp12, source);

#if NETCOREAPP
		await Verify.VerifyAnalyzerV3Aot(source, Verify.Diagnostic().WithLocation(0).WithArguments("Exception", "WithSkipExceptions.PrivateException"));
#endif
	}

#endif  // ROSLYN_LATEST
}
