namespace Testura.Code.Tests.Statements;

using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Generators.Common.BinaryExpressions;
using Code.Models.References;
using Code.Statements;
using NUnit.Framework;

[TestFixture]
public class SelectionStatementTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _conditional = new SelectionStatement();
    }

#pragma warning disable SA1201
    private SelectionStatement _conditional;
#pragma warning restore SA1201

    [Test]
    public void If_WhenCreatingAnBinaryExpression_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<=3){}",
            _conditional.If(
                    new ConditionalBinaryExpression(
                        new ConstantReference(2),
                        new ConstantReference(3),
                        ConditionalStatements.LessThanOrEqual),
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnComplexBinaryExpression_ShouldGenerateCorrectIfStatement()
    {
        var leftBinaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.Equal);

        var rightBinaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.LessThan);

        var orBinaryExpression = new OrBinaryExpression(
            leftBinaryExpression,
            rightBinaryExpression);

        var binaryExpression = new OrBinaryExpression(leftBinaryExpression, orBinaryExpression);

        Assert.AreEqual(
            "if(1==2||1==2||1<2){}",
            _conditional.If(binaryExpression, BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void
        If_WhenCreatingAnIfWithBinaryExpressionAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
    {
        Assert.AreEqual(
            "if(2==3)MyMethod();",
            _conditional.If(
                    new ConditionalBinaryExpression(
                        new ConstantReference(2),
                        new ConstantReference(3),
                        ConditionalStatements.Equal),
                    Statement.Expression.Invoke("MyMethod")
                        .AsStatement())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2==3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.Equal,
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void
        If_WhenCreatingAnIfWithEqualAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
    {
        Assert.AreEqual(
            "if(2==3)MyMethod();",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.Equal,
                    Statement.Expression.Invoke("MyMethod")
                        .AsStatement())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithGreaterThan_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2>3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.GreaterThan,
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithGreaterThanOrEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2>=3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.GreaterThanOrEqual,
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithLessThan_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.LessThan,
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithLessThanOrEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<=3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.LessThanOrEqual,
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithNotEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2!=3){}",
            _conditional.If(
                    new ValueArgument(2),
                    new ValueArgument(3),
                    ConditionalStatements.NotEqual,
                    BodyGenerator.Create())
                .ToString());
    }
}
