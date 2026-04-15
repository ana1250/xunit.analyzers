using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.InlineDataMustMatchTheoryParameters>;

public class X1009_InlineDataMustMatchTheoryParametersTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[Fact]
				[InlineData]
				public void Fact_DoesNotTrigger(string a) { }

				[Theory]
				[InlineData(1, "Hello")]
				[InlineData([2, "There"])]
				[InlineData(new object[] { 3, "World" })]
				public void Theory_CorrectArgumentCount_DoesNotTrigger(int a, string b) { }

				[Theory]
				[{|xUnit1009:InlineData|}]
				public void Theory_NoArguments_Triggers(string a) { }

				[Theory]
				[{|xUnit1009:InlineData(1)|}]
				public void Theory_TooFewArguments_Triggers(int a, int b, string c) { }

				[Theory]
				[{|xUnit1009:InlineData(1)|}]
				public void Theory_TooFewArgumentsWithParams_Triggers(int a, int b, params string[] value) { }
			}
			""";

		await Verify.VerifyAnalyzer(LanguageVersion.CSharp12, source);
	}

	[Fact]
	public async ValueTask V3_only()
	{
		var source = /* lang=c#-test */ """
			using Xunit;

			public class TestClass {
				[CulturedTheory(new[] { "en-US" })]
				[InlineData(1, "Hello")]
				[InlineData([2, "There"])]
				[InlineData(new object[] { 3, "World" })]
				public void Theory_CorrectArgumentCount_DoesNotTrigger(int a, string b) { }

				[CulturedTheory(new[] { "en-US" })]
				[{|xUnit1009:InlineData|}]
				public void Theory_NoArguments_Triggers(string a) { }

				[CulturedTheory(new[] { "en-US" })]
				[{|xUnit1009:InlineData(1)|}]
				public void Theory_TooFewArguments_Triggers(int a, int b, string c) { }

				[CulturedTheory(new[] { "en-US" })]
				[{|xUnit1009:InlineData(1)|}]
				public void Theory_TooFewArgumentsWithParams_Triggers(int a, int b, params string[] value) { }
			}
			""";

		await Verify.VerifyAnalyzerV3(LanguageVersion.CSharp12, source);
	}
}
