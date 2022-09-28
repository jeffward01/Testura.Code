using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Factories;

using Generators.Common;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models.References;

internal static class EqualsValueClauseFactory
{
    /// <summary>
    ///     Get the correct equals value clause for a specific type
    /// </summary>
    /// <param name="value">Value we want to put the variable equal as</param>
    /// <returns>The correct equals value clause</returns>
    internal static EqualsValueClauseSyntax GetEqualsValueClause(object value)
    {
        return value switch
        {
            int i => EqualsValueClause(
                LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    Literal(TriviaList(), i.ToString(), i, TriviaList()))),
            string s => EqualsValueClause(
                LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal(TriviaList(), s, s, TriviaList()))),
            VariableReference reference => EqualsValueClause(ReferenceGenerator.Create(reference)),
            _ => throw new NotSupportedException("Not a supported value")
        };
    }
}
