namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

using System.Collections.Generic;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

[TestFixture]
public class DictionaryArgumentTests
{
    [Test]
    public void GetArgumentSyntax_WhenUsingDictionary_ShouldGetCorrectCode()
    {
        var argument = new DictionaryInitializationArgument<int, int>(
            new Dictionary<int, IArgument>
            {
                [1] = new ValueArgument(2)
            });
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("newDictionary<int,int>{[1]=2}", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenUsingDictionaryAsNamedArgument_ShouldGetCorrectCode()
    {
        var argument = new DictionaryInitializationArgument<int, int>(
            new Dictionary<int, IArgument>
            {
                [1] = new ValueArgument(2)
            },
            "namedArgument");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("namedArgument:newDictionary<int,int>{[1]=2}", syntax.ToString());
    }
}
