using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.MemberDataShouldReferenceValidMember>;

public class X1067_MemberDataShouldReferenceValidMemberTests
{
	[Fact]
	public async ValueTask V2_and_V3()
	{
		var source = /* lang=c#-test */ """
			#pragma warning disable xUnit1042

			using System.Collections.Generic;
			using Xunit;

			public class TestClass
			{
				public static IEnumerable<object[]> DataSourceOneArg(int multiplier) => new object[][] { new object[] { 42 * multiplier } };

				[Theory]
				[{|#0:MemberData(nameof(DataSourceOneArg))|}]
				public void OneArg_WithoutSingleArg(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOneArg), 1)]
				public void OneArg_WithSingleArg_AsParams(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOneArg), new object[] { 1 })]
				public void OneArg_WithSingleArg_AsArray(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOneArg), [1])]
				public void OneArg_WithSingleArg_AsCollection(int _)
				{ }

				public static IEnumerable<object[]> DataSourceOptionalArg(int multiplier = 1) => new object[][] { new object[] { 42 * multiplier } };

				[Theory]
				[MemberData(nameof(DataSourceOptionalArg))]
				public void OptionalArg_WithoutSingleArg(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOptionalArg), 1)]
				public void OptionalArg_WithSingleArg_AsParams(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOptionalArg), new object[] { 1 })]
				public void OptionalArg_WithSingleArg_AsArray(int _)
				{ }

				[Theory]
				[MemberData(nameof(DataSourceOptionalArg), [1])]
				public void OptionalArg_WithSingleArg_AsCollection(int _)
				{ }
			}
			""";
		var expected = Verify.Diagnostic("xUnit1067").WithLocation(0).WithArguments("TestClass", "DataSourceOneArg", "int", "multiplier");

		await Verify.VerifyAnalyzer(LanguageVersion.CSharp12, source, expected);
	}
}
