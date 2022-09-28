namespace Testura.Code.Tests.Generators.Common.BinaryExpressions;

using Code.Generators.Common.BinaryExpressions;
using Code.Models.References;
using NUnit.Framework;

[TestFixture]
public class ConditionalBinaryExpressionTests
{
    [Test]
    public void GetBinaryExpression_WhenHavingTwoReferencesAndEqual_ShouldGenerateCode()
    {
        var binaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.Equal);
        Assert.AreEqual(
            "1==2",
            binaryExpression.GetBinaryExpression()
                .ToString());
    }
}
