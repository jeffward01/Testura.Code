namespace Testura.Code.Tests.Builders;

using System.Collections.Generic;
using Code.Builders;
using Code.Builders.BuildMembers;
using Code.Models;
using NUnit.Framework;

[TestFixture]
public class NamespaceBuildersTest
{
    [Test]
    public void Build_WhenBuildingNamespace_CodeShouldContainMembers()
    {
        var @namespace = new NamespaceBuilder("MyNamespace").WithUsings("System")
            .With(
                new EnumBuildMember(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("SomeEnum", 2)
                    }))
            .With(new ClassBuildMember(new ClassBuilder("MyClass", null)))
            .Build();

        Assert.AreEqual(
            "usingSystem;namespaceMyNamespace{enumMyEnum{SomeEnum=2}publicclassMyClass{}}",
            @namespace.ToString());
    }

    [Test]
    public void Build_WhenBuildingNamespaceWithInterfaceBuildMember_CodeShouldContainMembers()
    {
        var @namespace = new NamespaceBuilder("MyNamespace").WithUsings("System")
            .With(
                new EnumBuildMember(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("SomeEnum", 2)
                    }))
            .With(new InterfaceBuildMember(new InterfaceBuilder("MyInterface", null)))
            .Build();

        Assert.AreEqual(
            "usingSystem;namespaceMyNamespace{enumMyEnum{SomeEnum=2}publicinterfaceMyInterface{}}",
            @namespace.ToString());
    }

    [Test]
    public void Build_WhenBuildingNamespaceWithTypeFileScoped_CodeShouldContainMembers()
    {
        var @namespace = new NamespaceBuilder("MyNamespace", NamespaceType.FileScoped)
            .WithUsings("System")
            .With(
                new EnumBuildMember(
                    "MyEnum",
                    new List<EnumMember>
                    {
                        new("SomeEnum", 2)
                    }))
            .With(new ClassBuildMember(new ClassBuilder("MyClass", null).BuildWithoutNamespace()))
            .Build();

        Assert.AreEqual(
            "usingSystem;namespaceMyNamespace;enumMyEnum{SomeEnum=2}publicclassMyClass{}",
            @namespace.ToString());
    }
}
