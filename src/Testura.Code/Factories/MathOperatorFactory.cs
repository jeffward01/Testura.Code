﻿namespace Testura.Code.Factories;

using Microsoft.CodeAnalysis.CSharp;

internal static class MathOperatorFactory
{
    public static SyntaxKind GetSyntaxKind(MathOperators mathOperator)
    {
        switch (mathOperator)
        {
            case MathOperators.Add:
                return SyntaxKind.AddExpression;
            case MathOperators.Subtract:
                return SyntaxKind.SubtractExpression;
            case MathOperators.Divide:
                return SyntaxKind.DivideExpression;
            case MathOperators.Multiply:
                return SyntaxKind.MultiplyExpression;
            default:
                throw new ArgumentOutOfRangeException(nameof(mathOperator), mathOperator, null);
        }
    }
}
