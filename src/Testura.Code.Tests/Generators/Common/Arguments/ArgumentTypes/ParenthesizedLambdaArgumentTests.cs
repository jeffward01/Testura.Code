namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Models;
using Code.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

[TestFixture]
public class ParenthesizedLambdaArgumentTests
{
    [Test]
    public void GetArgumentSyntax_WhenCreatingEmpty_ShouldGetCorrectCode()
    {
        var argument = new ParenthesizedLambdaArgument(
            Statement.Expression.Invoke("MyMethod")
                .AsExpression());
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("()=>MyMethod()", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenCreatingEmptyAsNamedArgument_ShouldGetCorrectCode()
    {
        var argument = new ParenthesizedLambdaArgument(
            Statement.Expression.Invoke("MyMethod")
                .AsExpression(),
            namedArgument: "namedArgument");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("namedArgument:()=>MyMethod()", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenCreatingWithParameter_ShouldGetCorrectCode()
    {
        var argument = new ParenthesizedLambdaArgument(
            Statement.Expression.Invoke("MyMethod")
                .AsExpression(),
            new[]
            {
                new Parameter("myPara", typeof(int))
            });
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("(myPara)=>MyMethod()", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenCreatingWithWithBlock_ShouldGetCorrectCode()
    {
        var block = BodyGenerator.Create(
            Statement.Expression.Invoke("MyMethod")
                .AsStatement());

        var argument = new ParenthesizedLambdaArgument(
            block,
            new[]
            {
                new Parameter("myPara", typeof(int))
            });
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("(myPara)=>{MyMethod();}", syntax.ToString());
    }
}
