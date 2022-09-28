#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

using Microsoft.CodeAnalysis.CSharp.Syntax;

public interface IArgument
{
    /// <summary>
    ///     Get the generated argument syntax.
    /// </summary>
    /// <returns>The generated argument syntax</returns>
    ArgumentSyntax GetArgumentSyntax();
}
