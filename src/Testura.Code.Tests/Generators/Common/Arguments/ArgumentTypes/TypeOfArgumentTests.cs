namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

using Code.Generators.Common.Arguments.ArgumentTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

[TestFixture]
public class TypeOfArgumentTests
{
    [Test]
    public void GetArgumentSyntax_WhenUsingType_ShouldGetCode()
    {
        var argument = new TypeOfArgument(typeof(int));
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("typeof(int)", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenUsingTypeAsNamedArgument_ShouldGetCode()
    {
        var argument = new TypeOfArgument(typeof(int), "namedArgument");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("namedArgument:typeof(int)", syntax.ToString());
    }
}
