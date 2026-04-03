using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers;

public abstract class AssertContextV3Base(
	Compilation compilation,
	Version version) :
		AssertContextBase(compilation, version), IAssertContextV3
{
	internal static readonly Version Version_0_6_0 = new("0.6.0");
	internal static readonly Version Version_3_0_1 = new("3.0.1");

	public override bool SupportsAssertFail => true;

	public override bool SupportsAssertNullWithPointers =>
		Version >= Version_3_0_1;

	public override bool SupportsInexactTypeAssertions =>
		Version >= Version_0_6_0;
}
