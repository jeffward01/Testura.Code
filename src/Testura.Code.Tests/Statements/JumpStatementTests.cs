namespace Testura.Code.Tests.Statements;

using System.Linq;
using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Models.References;
using Code.Models.Types;
using Code.Statements;
using NUnit.Framework;

[TestFixture]
public class JumpStatementTests
{
    [SetUp]
    public void SetUp()
    {
        _return = new JumpStatement();
    }

#pragma warning disable SA1201
    private JumpStatement _return;
#pragma warning restore SA1201

    [Test]
    public void Return_WhenReturnExpression_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returntest();",
            _return.Return(
                    Statement.Expression.Invoke("test")
                        .AsExpression())
                .ToString());
    }

    [Test]
    public void Return_WhenReturnNewObject_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returnnewHello(1,\"test\");",
            _return.Return(
                    ObjectCreationGenerator.Create(
                        CustomType.Create("Hello"),
                        new[]
                        {
                            new ValueArgument(1), new ValueArgument("test")
                        }))
                .ToString());
    }

    [Test]
    public void Return_WhenReturnNewObjectWithInitializer_ShouldGenerateCorrectCode()
    {
        var initializers = new[]
        {
            Statement.Declaration.Assign("hej", new VariableReference("test"))
        };

        var objectCreation = ObjectCreationGenerator.Create(
            CustomType.Create("Hello"),
            initialization: initializers.Select(i => i.Expression));

        Assert.AreEqual(
            "returnnewHello{hej=test};",
            _return.Return(objectCreation)
                .ToString());
    }

    [Test]
    public void Return_WhenReturnNewObjectWithoutParameters_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returnnewHello();",
            _return.Return(ObjectCreationGenerator.Create(CustomType.Create("Hello")))
                .ToString());
    }

    [Test]
    public void Return_WhenReturnReference_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returni;",
            _return.Return(new VariableReference("i"))
                .ToString());
    }

    [Test]
    public void Return_WhenReturnThis_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returnthis;",
            _return.ReturnThis()
                .ToString());
    }

    [Test]
    public void ReturnFalse_WhenReturnFalse_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returnfalse;",
            _return.ReturnFalse()
                .ToString());
    }

    [Test]
    public void ReturnTrue_WhenReturnTrue_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "returntrue;",
            _return.ReturnTrue()
                .ToString());
    }
}
