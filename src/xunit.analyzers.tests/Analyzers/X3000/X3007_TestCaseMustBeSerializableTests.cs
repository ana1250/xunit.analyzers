using System.Threading.Tasks;
using Xunit;
using Verify = CSharpVerifier<Xunit.Analyzers.TestCaseMustBeSerializable>;

public class X3007_TestCaseMustBeSerializableTests
{
	static string CS0535(
		string interfaceName,
		int memberCount)
	{
		var result = interfaceName;

		while (memberCount-- > 0)
			result = $"{{|CS0535:{result}|}}";

		return result;
	}

	[Fact]
	public async ValueTask V3_only_NonAOT()
	{
		var source = $$"""
			using Xunit.Sdk;

			[assembly: RegisterXunitSerializer(typeof(MySerializer), typeof(ExternalSerializedTestCase))]

			public class NonTestCase { }

			public abstract class AbstractTestCase : {{CS0535("ITestCase", 19)}} { }

			public class SelfSerializedTestCase : {{CS0535("ITestCase", 19)}}, {{CS0535("IXunitSerializable", 2)}} { }

			public class ExternalSerializedTestCase : {{CS0535("ITestCase", 19)}} { }

			public class DerivedTestCase : ExternalSerializedTestCase { }

			public class {|xUnit3007:UnserializedTestCase|} : {{CS0535("ITestCase", 19)}} { }

			public class MySerializer : {{CS0535("IXunitSerializer", 3)}} { }
			""";

		await Verify.VerifyAnalyzerV3NonAot(source);
	}
}
