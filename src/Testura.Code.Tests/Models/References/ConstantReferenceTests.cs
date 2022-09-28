namespace Testura.Code.Tests.Models.References;

using System;
using System.Collections.Generic;
using Code.Models.References;
using NUnit.Framework;

[TestFixture]
public class ConstantReferenceTests
{
    [Test]
    public void Constructor_WhenGivingANonNumericOrNonBoolean_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new ConstantReference(new List<string>()));
    }
}
