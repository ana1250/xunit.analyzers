using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.CulturedTestMustHaveAtLeastOneCulture>;

#if ROSLYN_LATEST
using Microsoft.CodeAnalysis.CSharp;
#endif

public class X1060_CulturedTestMustHaveAtLeastOneCultureTests
{
	[Fact]
	public async ValueTask V3_only()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[CulturedFact(new[] { "en-US" })]
				[CulturedTheory(new[] { "en-US" })]
				public void Success() { }

				[[|CulturedFact(new string[] { })|]]
				[[|CulturedTheory(new string[] { })|]]
				public void Failure1() { }

				[[|CulturedFact(new string[0])|]]
				[[|CulturedTheory(new string[0])|]]
				public void Failure2() { }
			}
			""";

		await Verify.VerifyAnalyzerV3(source);
	}

#if ROSLYN_LATEST  // Need C# 12 for collection expression

	[Fact]
	public async ValueTask V3_only_CSharp12()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[CulturedFact(["en-US"])]
				[CulturedTheory(["en-US"])]
				public void Success() { }

				[[|CulturedFact([])|]]
				[[|CulturedTheory([])|]]
				public void Failure() { }
			}
			""";

		await Verify.VerifyAnalyzerV3(LanguageVersion.CSharp12, source);
	}

#endif  // ROSLYN_LATEST
}
