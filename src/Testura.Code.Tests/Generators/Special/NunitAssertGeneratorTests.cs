﻿namespace Testura.Code.Tests.Generators.Special;

using System;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Generators.Special;
using Code.Models.References;
using NUnit.Framework;

[TestFixture]
public class NunitAssertGeneratorTests
{
    [Test]
    public void AreEqual_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.AreEqual(true,false,\"Message\");",
            NunitAssertGenerator.AreEqual(
                    new ValueArgument(true),
                    new ValueArgument(false),
                    "Message")
                .ToString());
    }

    [Test]
    public void AreNotEqual_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.AreNotEqual(true,false,\"Message\");",
            NunitAssertGenerator.AreNotEqual(
                    new ValueArgument(true),
                    new ValueArgument(false),
                    "Message")
                .ToString());
    }

    [Test]
    public void AreNotSame_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.AreNotSame(true,false,\"Message\");",
            NunitAssertGenerator.AreNotSame(
                    new ValueArgument(true),
                    new ValueArgument(false),
                    "Message")
                .ToString());
    }

    [Test]
    public void AreSame_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.AreSame(true,false,\"Message\");",
            NunitAssertGenerator.AreSame(
                    new ValueArgument(true),
                    new ValueArgument(false),
                    "Message")
                .ToString());
    }

    [Test]
    public void Contains_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.IsTrue(\"test\".Contains(\"t\"),\"Message\");",
            NunitAssertGenerator.Contains(
                    new ValueArgument("t"),
                    new ValueArgument("test"),
                    "Message")
                .ToString());
    }

    [Test]
    public void IsFalse_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.IsFalse(true,\"Message\");",
            NunitAssertGenerator.IsFalse(new ValueArgument(true), "Message")
                .ToString());
    }

    [Test]
    public void IsTrue_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.IsTrue(true,\"Message\");",
            NunitAssertGenerator.IsTrue(new ValueArgument(true), "Message")
                .ToString());
    }

    [Test]
    public void Throws_WhenCallingWithAReferencethatIsNotMethod_ShouldShowError()
    {
        Assert.Throws<ArgumentException>(
            () => NunitAssertGenerator.Throws(
                    new VariableReference("test"),
                    typeof(ArgumentException),
                    "test")
                .ToString());
    }

    [Test]
    public void Throws_WhenCallingWithMessage_ShouldGenerateCode()
    {
        Assert.AreEqual(
            "Assert.Throws<ArgumentNullException>(()=>MyMethod(),\"Message\");",
            NunitAssertGenerator.Throws(
                    new MethodReference("MyMethod"),
                    typeof(ArgumentNullException),
                    "Message")
                .ToString());
    }
}
