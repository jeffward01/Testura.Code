namespace Testura.Code.Tests.Builders;

using Code.Builders;
using Code.Models;
using Code.Models.Properties;
using NUnit.Framework;

[TestFixture]
public class InterfaceBuilderTests
{
    [SetUp]
    public void SetUp()
    {
        _interfaceBuilder = new InterfaceBuilder("TestInterface", "MyNamespace");
    }

#pragma warning disable SA1201
    private InterfaceBuilder _interfaceBuilder;
#pragma warning restore SA1201

    [Test]
    public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
    {
        Assert.IsTrue(
            _interfaceBuilder.WithAttributes(new Attribute("MyAttribute"))
                .Build()
                .ToString()
                .Contains("[MyAttribute]"));
    }

    [Test]
    public void Build_WhenGivenClassName_CodeShouldContainClassName()
    {
        Assert.IsTrue(
            _interfaceBuilder.Build()
                .ToString()
                .Contains("TestInterface"));
    }

    [Test]
    public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
    {
        Assert.IsTrue(
            _interfaceBuilder.ThatInheritFrom(typeof(int))
                .Build()
                .ToString()
                .Contains("TestInterface:int"));
    }

    [Test]
    public void Build_WhenGivenInheritanceWithThreeTypes_CodeShouldContainInheritance()
    {
        Assert.IsTrue(
            _interfaceBuilder.ThatInheritFrom(typeof(int), typeof(string), typeof(double))
                .Build()
                .ToString()
                .Contains("TestInterface:int,string,double"));
    }

    [Test]
    public void Build_WhenGivenInheritanceWithTwoTypes_CodeShouldContainInheritance()
    {
        Assert.IsTrue(
            _interfaceBuilder.ThatInheritFrom(typeof(int), typeof(string))
                .Build()
                .ToString()
                .Contains("TestInterface:int,string"));
    }

    [Test]
    public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
    {
        Assert.IsTrue(
            _interfaceBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract)
                .Build()
                .ToString()
                .Contains("publicabstractinterfaceTestInterface"));
    }

    [Test]
    public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
    {
        Assert.IsTrue(
            _interfaceBuilder.Build()
                .ToString()
                .Contains("MyNamespace"));
    }

    [Test]
    public void Build_WhenGivenProperty_CodeShouldContainProperty()
    {
        Assert.IsTrue(
            _interfaceBuilder.WithProperties(
                    new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet))
                .Build()
                .ToString()
                .Contains("intMyProperty{get;set;}"));
    }

    [Test]
    public void Build_WhenGivenUsing_CodeShouldContainUsing()
    {
        Assert.IsTrue(
            _interfaceBuilder.WithUsings("some.namespace")
                .Build()
                .ToString()
                .Contains("some.namespace"));
    }
}
