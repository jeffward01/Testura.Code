namespace Testura.Code.Tests.Extensions.Naming;

using System.Collections.Generic;
using Code.Extensions.Naming;
using NUnit.Framework;

[TestFixture]
public class TypeNamingExtensionsTests
{
    [Test]
    public void FormattedClassName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("List", typeof(List<int>).FormattedClassName());
    }

    [Test]
    public void FormattedFieldName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("list", typeof(List<int>).FormattedFieldName());
    }

    [Test]
    public void FormattedTypeName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("List<int>", typeof(List<int>).FormattedTypeName());
    }

    [Test]
    public void FormattedTypeName_WhenHavingValueType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("int", typeof(int).FormattedTypeName());
    }
}
