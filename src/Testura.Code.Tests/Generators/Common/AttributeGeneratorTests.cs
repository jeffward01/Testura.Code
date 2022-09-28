namespace Testura.Code.Tests.Generators.Common;

using System.Collections.Generic;
using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Models;
using NUnit.Framework;

[TestFixture]
public class AttributeGeneratorTests
{
    [Test]
    public void Create_WhenCreatingAttributeWithArguments_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test(1,2)]",
            AttributeGenerator.Create(
                    new Attribute(
                        "Test",
                        new List<IArgument>
                        {
                            new ValueArgument(1),
                            new ValueArgument(2)
                        }))
                .ToString());
    }

    [Test]
    public void Create_WhenCreatingAttributeWithAssignedArgument_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test(with=1,value=true)]",
            AttributeGenerator.Create(
                    new Attribute(
                        "Test",
                        new List<IArgument>
                        {
                            new AssignArgument("with", 1),
                            new AssignArgument("value", true)
                        }))
                .ToString());
    }

    [Test]
    public void Create_WhenCreatingAttributeWithNamedArgument_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test(with:1,value:2)]",
            AttributeGenerator.Create(
                    new Attribute(
                        "Test",
                        new List<IArgument>
                        {
                            new ValueArgument(1, "with"),
                            new ValueArgument(2, "value")
                        }))
                .ToString());
    }

    [Test]
    public void Create_WhenCreatingWithMultipleAttributes_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test][TestCase]",
            AttributeGenerator.Create(new Attribute("Test"), new Attribute("TestCase"))
                .ToString());
    }

    [Test]
    public void Create_WhenCreatingWithSingleAttrbute_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test]",
            AttributeGenerator.Create(new Attribute("Test", new List<IArgument>()))
                .ToString());
    }

    [Test]
    public void Create_WhenNotProvidingAnyAttributes_ShouldGetEmptyString()
    {
        Assert.AreEqual(
            string.Empty,
            AttributeGenerator.Create()
                .ToString());
    }
}
