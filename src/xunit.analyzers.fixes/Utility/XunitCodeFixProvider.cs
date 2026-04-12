using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Xunit.Analyzers.Fixes;

public abstract class XunitCodeFixProvider(params string[] diagnostics) :
	CodeFixProvider
{
	public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } = [.. diagnostics];

	public override FixAllProvider? GetFixAllProvider() =>
		null;
}
