namespace Testura.Code.Tests.Generators.Common.Arguments;

using Code.Generators.Common;
using Code.Generators.Common.Arguments.ArgumentTypes;
using NUnit.Framework;

[TestFixture]
public class ArgumentsTests
{
    [Test]
    public void Create_WhenNotProvidingAnyArguments_ShouldGetEmptyString()
    {
        Assert.AreEqual(
            "()",
            ArgumentGenerator.Create()
                .ToString());
    }

    [Test]
    public void Create_WhenNotProvidingMultipleArgument_ShouldContainArguments()
    {
        Assert.AreEqual(
            "(1,2)",
            ArgumentGenerator.Create(new ValueArgument(1), new ValueArgument(2))
                .ToString());
    }

    [Test]
    public void Create_WhenNotProvidingSingleArgument_ShouldContainArgument()
    {
        Assert.AreEqual(
            "(1)",
            ArgumentGenerator.Create(new ValueArgument(1))
                .ToString());
    }
}
