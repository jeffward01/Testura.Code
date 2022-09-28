namespace Testura.Code.Tests.Builders;

using System.Collections.Generic;
using Code.Builders;
using Code.Models;
using Code.Models.Properties;
using NUnit.Framework;

[TestFixture]
public class ClassBuilderTests
{
    [SetUp]
    public void SetUp()
    {
        _classBuilder = new ClassBuilder("TestClass", "MyNamespace");
    }

#pragma warning disable SA1201
    private ClassBuilder _classBuilder;
#pragma warning restore SA1201

    [Test]
    public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
    {
        Assert.IsTrue(
            _classBuilder.WithAttributes(new Attribute("MyAttribute"))
                .Build()
                .ToString()
                .Contains("[MyAttribute]"));
    }

    [Test]
    public void Build_WhenGivenClassName_CodeShouldContainClassName()
    {
        Assert.IsTrue(
            _classBuilder.Build()
                .ToString()
                .Contains("TestClass"));
    }

    [Test]
    public void Build_WhenGivenField_CodeShouldContainField()
    {
        Assert.IsTrue(
            _classBuilder.WithFields(
                    new Field(
                        "myField",
                        typeof(int),
                        new List<Modifiers>
                        {
                            Modifiers.Public
                        }))
                .Build()
                .ToString()
                .Contains("publicintmyField;"));
    }

    [Test]
    public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
    {
        Assert.IsTrue(
            _classBuilder.ThatInheritFrom(typeof(int))
                .Build()
                .ToString()
                .Contains("TestClass:int"));
    }

    [Test]
    public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
    {
        Assert.IsTrue(
            _classBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract)
                .Build()
                .ToString()
                .Contains("publicabstractclassTestClass"));
    }

    [Test]
    public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
    {
        Assert.IsTrue(
            _classBuilder.Build()
                .ToString()
                .Contains("MyNamespace"));
    }

    [Test]
    public void Build_WhenGivenNamespaceWithFileScopedNamespace_CodeShouldContainNamespace()
    {
        var classBuilder = new ClassBuilder("TestClass", "MyNamespace", NamespaceType.FileScoped);
        Assert.IsTrue(
            classBuilder.Build()
                .ToString()
                .Contains("MyNamespace;"));
    }

    [Test]
    public void Build_WhenGivenProperty_CodeShouldContainProperty()
    {
        Assert.IsTrue(
            _classBuilder.WithProperties(
                    new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet))
                .Build()
                .ToString()
                .Contains("intMyProperty{get;set;}"));
    }

    [Test]
    public void Build_WhenGivenUsing_CodeShouldContainUsing()
    {
        Assert.IsTrue(
            _classBuilder.WithUsings("some.namespace")
                .Build()
                .ToString()
                .Contains("some.namespace"));
    }
}
