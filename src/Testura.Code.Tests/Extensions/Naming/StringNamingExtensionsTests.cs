namespace Testura.Code.Tests.Extensions.Naming;

using Code.Extensions.Naming;
using NUnit.Framework;

[TestFixture]
public class StringNamingExtensionsTests
{
    [Test]
    public void FirstLetterToLowerCase_WhenHavingAEmptyString_ShouldReturnSameString()
    {
        Assert.AreEqual(string.Empty, string.Empty.FirstLetterToLowerCase());
    }

    [Test]
    public void FirstLetterToLowerCase_WhenHavingAString_ShouldSetLFirstLetterToLowerCase()
    {
        Assert.AreEqual("test", "Test".FirstLetterToLowerCase());
    }

    [Test]
    public void FirstLetterToUpperCase_WhenHavingAEmptyString_ShouldReturnSameString()
    {
        Assert.AreEqual(string.Empty, string.Empty.FirstLetterToUpperCase());
    }

    [Test]
    public void FirstLetterToUpperCase_WhenHavingAString_ShouldSetLFirstLetterToUpperCase()
    {
        Assert.AreEqual("Test", "test".FirstLetterToUpperCase());
    }
}
