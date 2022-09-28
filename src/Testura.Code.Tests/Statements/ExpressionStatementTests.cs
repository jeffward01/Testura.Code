﻿namespace Testura.Code.Tests.Statements;

using System;
using System.Collections.Generic;
using Code.Generators.Common.Arguments.ArgumentTypes;
using Code.Generators.Common.BinaryExpressions;
using Code.Models.References;
using Code.Statements;
using NUnit.Framework;

[TestFixture]
public class ExpressionStatementTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _expressionStatement = new ExpressionStatement();
    }

#pragma warning disable SA1201
    private ExpressionStatement _expressionStatement;
#pragma warning restore SA1201

    [Test]
    public void Invoke_WhenInvokeWithMethodNameAndArguments_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke(
            "MyMethod",
            new List<IArgument>
            {
                new ValueArgument(1)
            });
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "MyMethod(1);",
            invocation.AsStatement()
                .ToString());
    }

    [Test]
    public void
        Invoke_WhenInvokeWithMethodNameAndBinaryExpressionArgument_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke(
            "MyMethod",
            new List<IArgument>
            {
                new BinaryExpressionArgument(
                    new MathBinaryExpression(
                        new ConstantReference(1),
                        new ConstantReference(2),
                        MathOperators.Add))
            });
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "MyMethod(1+2);",
            invocation.AsStatement()
                .ToString());
    }

    [Test]
    public void Invoke_WhenUsingGenericMethodReference_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke(
            new VariableReference(
                "myClass",
                new MethodReference(
                    "Do",
                    new List<IArgument>(),
                    new List<Type>
                    {
                        typeof(int)
                    })));
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "myClass.Do<int>();",
            invocation.AsStatement()
                .ToString());
    }

    [Test]
    public void Invoke_WhenUsingMethodReference_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke(
            new VariableReference("myClass", new MethodReference("Do", new List<IArgument>())));
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "myClass.Do();",
            invocation.AsStatement()
                .ToString());
    }

    [Test]
    public void Invoke_WhenUsingReferenceThatisNotMethod_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(
            () => _expressionStatement.Invoke(new VariableReference("test")));
    }

    [Test]
    public void Invoke_WhenUsingSimpleNames_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke("myClass", "Do");
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "myClass.Do();",
            invocation.AsStatement()
                .ToString());
    }

    [Test]
    public void Invoke_WhenUsingSimpleNamesWithArguments_ShouldGenerateCorrectCode()
    {
        var invocation = _expressionStatement.Invoke(
            "myClass",
            "Do",
            new List<IArgument>
            {
                new ValueArgument(1)
            });
        Assert.IsNotNull(invocation);
        Assert.AreEqual(
            "myClass.Do(1);",
            invocation.AsStatement()
                .ToString());
    }
}
