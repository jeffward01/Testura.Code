namespace Testura.Code.Tests.Compilation;

using System.IO;
using System.Threading.Tasks;
using Code.Builders;
using Compilations;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

[TestFixture]
public class CompilerTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _compiler = new Compiler();
    }

#pragma warning disable SA1201
    private Compiler _compiler;
#pragma warning restore SA1201

    [Test]
    public async Task CompileSourceAsync_WhenCompilingSource_ShouldGetADll()
    {
        var result = await _compiler.CompileSourceAsync(
            Path.Combine(TestContext.CurrentContext.TestDirectory, "test.dll"),
            new ClassBuilder("TestClass", "Test").Build()
                .NormalizeWhitespace()
                .ToString());
        Assert.IsNotNull(result.PathToDll);
        Assert.AreEqual(0, result.OutputRows.Count);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public async Task
        CompileSourceAsync_WhenCompilingSourceWithError_ShouldGetListContainingErrors()
    {
        var result = await _compiler.CompileSourceAsync(
            Path.Combine(TestContext.CurrentContext.TestDirectory, "test.dll"),
            "gfdgdfgfdg");
        Assert.AreEqual(1, result.OutputRows.Count);
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task CompileSourceInMemoryAsync_WhenCompilingSource_ShouldGetEmptyResultList()
    {
        var result = await _compiler.CompileSourceInMemoryAsync(
            new ClassBuilder("TestClass", "Test").Build()
                .NormalizeWhitespace()
                .ToString());
        Assert.AreEqual(0, result.OutputRows.Count);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public async Task
        CompileSourceInMemoryAsync_WhenCompilingSourceWithError_ShouldGetListContainingErrors()
    {
        var result = await _compiler.CompileSourceInMemoryAsync("gfdgdfgfdg");
        Assert.AreEqual(1, result.OutputRows.Count);
        Assert.IsFalse(result.Success);
    }
}
