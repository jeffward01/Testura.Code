namespace Testura.Code.Tests.Generators.Common;

using System.Collections.Generic;
using Code.Generators.Common;
using Code.Models;
using NUnit.Framework;

[TestFixture]
public class EnumGeneratorTests
{
    [Test]
    public void Enum_WhenCreatingEnum_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "enumMyEnum{MyValue,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("MyValue"),
                        new("MyOtherValue")
                    })
                .ToString());
    }

    [Test]
    public void Enum_WhenCreatingEnumWithAttribute_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test]enumMyEnum{MyValue,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("MyValue"),
                        new("MyOtherValue")
                    },
                    attributes: new List<Attribute>
                    {
                        new("Test")
                    })
                .ToString());
    }

    [Test]
    public void Enum_WhenCreatingEnumWithAttributeOnMember_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test]publicenumMyEnum{[MyAttribute]MyValue,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new(
                            "MyValue",
                            attributes: new[]
                            {
                                new Attribute("MyAttribute")
                            }),
                        new("MyOtherValue")
                    },
                    new List<Modifiers>
                    {
                        Modifiers.Public
                    },
                    new List<Attribute>
                    {
                        new("Test")
                    })
                .ToString());
    }

    [Test]
    public void Enum_WhenCreatingEnumWithModifiers_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "publicenumMyEnum{MyValue,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("MyValue"),
                        new("MyOtherValue")
                    },
                    new List<Modifiers>
                    {
                        Modifiers.Public
                    })
                .ToString());
    }

    [Test]
    public void Enum_WhenCreatingEnumWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test]publicenumMyEnum{MyValue,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("MyValue"),
                        new("MyOtherValue")
                    },
                    new List<Modifiers>
                    {
                        Modifiers.Public
                    },
                    new List<Attribute>
                    {
                        new("Test")
                    })
                .ToString());
    }

    [Test]
    public void Enum_WhenCreatingEnumWithValueOnMember_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual(
            "[Test]publicenumMyEnum{MyValue=2,MyOtherValue}",
            EnumGenerator.Create(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("MyValue", 2),
                        new("MyOtherValue")
                    },
                    new List<Modifiers>
                    {
                        Modifiers.Public
                    },
                    new List<Attribute>
                    {
                        new("Test")
                    })
                .ToString());
    }
}
