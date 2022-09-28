﻿namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

using System.Collections.Generic;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Models.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

[TestFixture]
public class ObjectInitializationArgumentTests
{
    [Test]
    public void GetArgumentSyntax_WhenInitializeObject_ShouldGetCorrectCode()
    {
        var argument = new ObjectInitializationArgument(
            CustomType.Create("CustomClass"),
            new Dictionary<string, IArgument>
            {
                ["Property"] = new ValueArgument("hello")
            });

        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("newCustomClass{Property=\"hello\"}", syntax.ToString());
    }

    [Test]
    public void
        GetArgumentSyntax_WhenInitializeObjectWithAnotherObjectInitialize_ShouldGetCorrectCode()
    {
        var argument = new ObjectInitializationArgument(
            CustomType.Create("CustomClass"),
            new Dictionary<string, IArgument>
            {
                ["CustomPropertyType"] = new ObjectInitializationArgument(
                    CustomType.Create("CustomPropertyType"),
                    new Dictionary<string, IArgument>
                    {
                        ["Property"] = new ValueArgument("hello")
                    })
            });

        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual(
            "newCustomClass{CustomPropertyType=newCustomPropertyType{Property=\"hello\"}}",
            syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenInitializeObjectWithDictionary_ShouldGetCorrectCode()
    {
        var argument = new ObjectInitializationArgument(
            CustomType.Create("CustomClass"),
            new Dictionary<string, IArgument>
            {
                ["PropertyWithDictionaryType"] = new DictionaryInitializationArgument<int, int>(
                    new Dictionary<int, IArgument>
                    {
                        [1] = new ValueArgument(2)
                    })
            });

        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual(
            "newCustomClass{PropertyWithDictionaryType=newDictionary<int,int>{[1]=2}}",
            syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenInitializeObjectWithVariable_ShouldGetCorrectCode()
    {
        var argument = new ObjectInitializationArgument(
            CustomType.Create("CustomClass"),
            new Dictionary<string, IArgument>
            {
                ["Property"] = new ValueArgument("hello"),
                ["AnotherProperty"] = new VariableArgument("variable")
            });

        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual(
            "newCustomClass{Property=\"hello\",AnotherProperty=variable}",
            syntax.ToString());
    }
}
