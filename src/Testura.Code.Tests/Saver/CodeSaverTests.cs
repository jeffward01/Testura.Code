namespace Testura.Code.Tests.Saver;

using System.Collections.Generic;
using Code.Builders;
using Code.Models.Options;
using Code.Saver;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using NUnit.Framework;

[TestFixture]
public class CodeSaverTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _coderSaver = new CodeSaver();
    }

#pragma warning disable SA1201
    private CodeSaver _coderSaver;
#pragma warning restore SA1201

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsString_ShouldGetString()
    {
        var code = _coderSaver.SaveCodeAsString(new ClassBuilder("TestClass", "test").Build());
        Assert.IsNotNull(code);
        Assert.AreEqual(
            "namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n    }\r\n}",
            code);
    }

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsStringAndOptions_ShouldGetString()
    {
        var codeSaver = new CodeSaver(
            new List<OptionKeyValue>
            {
                new(CSharpFormattingOptions.NewLinesForBracesInMethods, false)
            });
        var code = codeSaver.SaveCodeAsString(
            new ClassBuilder("TestClass", "test").WithMethods(new MethodBuilder("MyMethod").Build())
                .Build());
        Assert.IsNotNull(code);
        Assert.AreEqual(
            "namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n        void MyMethod() {\r\n        }\r\n    }\r\n}",
            code);
    }
}
