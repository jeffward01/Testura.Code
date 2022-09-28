namespace Testura.Code.Tests.Generators.Common;

using System.Collections.Generic;
using System.Linq;
using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Models.References;
using Code.Models.Types;
using Code.Statements;
using NUnit.Framework;

[TestFixture]
public class ObjectCreationTests
{
    [Test]
    public void ObjectCreation_WhenCreateObjectWithInitializer_ShouldGenerateCorrectCode()
    {
        var initializers = new[]
        {
            Statement.Declaration.Assign("hej", new VariableReference("test"))
        };

        var objectCreation = ObjectCreationGenerator.Create(
            CustomType.Create("Hello"),
            initialization: initializers.Select(i => i.Expression));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello{hej=test}", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithJustType_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(CustomType.Create("Hello"));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello()", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithMultipleInitializers_ShouldGenerateCorrectCode()
    {
        var initializers = new[]
        {
            Statement.Declaration.Assign("hej", new VariableReference("test")),
            Statement.Declaration.Assign("hej2", new ConstantReference(1))
        };

        var objectCreation = ObjectCreationGenerator.Create(
            CustomType.Create("Hello"),
            initialization: initializers.Select(i => i.Expression));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello{hej=test,hej2=1}", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithTypeAndArguments_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(
            CustomType.Create("Hello"),
            new List<IArgument>
            {
                new ValueArgument(1)
            });
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello(1)", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithTypeThatHaveGenerics_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(typeof(List<string>));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newList<string>()", objectCreation.ToString());
    }
}
