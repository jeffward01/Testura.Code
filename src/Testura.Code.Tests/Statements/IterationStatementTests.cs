namespace Testura.Code.Tests.Statements;

using Code.Generators.Common;
using Code.Generators.Common.BinaryExpressions;
using Code.Models.References;
using Code.Statements;
using NUnit.Framework;

[TestFixture]
public class IterationStatementTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _control = new IterationStatement();
    }

#pragma warning disable SA1201
    private IterationStatement _control;
#pragma warning restore SA1201

    [Test]
    public void For_WhenCreatingForeachLoopWithNamesAndNotVar_ShouldGenerateCodeWithType()
    {
        Assert.AreEqual(
            "foreach(intiinmyList){}",
            _control.ForEach("i", typeof(int), "myList", BodyGenerator.Create(), false)
                .ToString());
    }

    [Test]
    public void For_WhenCreatingForeachLoopWithNamesAndVar_ShouldGenerateCodeWithVar()
    {
        Assert.AreEqual(
            "foreach(variinmyList){}",
            _control.ForEach("i", typeof(int), "myList", BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void For_WhenCreatingForeachLoopWithReference_ShouldGenerateCodeWithType()
    {
        Assert.AreEqual(
            "foreach(intiina.MyMethod()){}",
            _control.ForEach(
                    "i",
                    typeof(int),
                    new VariableReference("a", new MethodReference("MyMethod")),
                    BodyGenerator.Create(),
                    false)
                .ToString());
    }

    [Test]
    public void For_WhenCreatingForLoopWithStartAndEnd_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "for(inti=1;i<2;i++){}",
            _control.For(1, 2, "i", BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void For_WhenCreatingForLoopWithVariableReferences_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "for(inti=0;i<myClass.MyProperty;i++){}",
            _control.For(
                    new ConstantReference(0),
                    new VariableReference("myClass", new MemberReference("MyProperty")),
                    "i",
                    BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void While_WhenCreatingWhileLoopWithBinaryExpression_ShouldGenerateCode()
    {
        var binaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.LessThan);

        Assert.AreEqual(
            "while(1<2){}",
            _control.While(binaryExpression, BodyGenerator.Create())
                .ToString());
    }

    [Test]
    public void While_WhenCreatingWhileLoopWithTrue_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "while(true){}",
            _control.WhileTrue(BodyGenerator.Create())
                .ToString());
    }
}
