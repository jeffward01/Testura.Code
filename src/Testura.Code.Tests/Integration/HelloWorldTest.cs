﻿using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models;
using Testura.Code.Models.References;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Integration
{
    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void Test_HelloWorld()
        {
            var classBuilder = new ClassBuilder("Program", "HelloWorld");
            var @class = classBuilder
                .WithUsings("System") 
                .WithModifiers(Modifiers.Public, Modifiers.Static)
                .WithMethods(
                    new MethodBuilder("Main")
                    .WithParameters(new Parameter("args", typeof(string[])))
                    .WithBody(
                        BodyGenerator.Create(
                            Statement.Expression.Invoke(new VariableReference("Console", new MethodReference("WriteLine", new List<IArgument>() { new ValueArgument("Hello world") }))).AsStatement(),
                            Statement.Expression.Invoke("Console", "ReadLine").AsStatement()
                            ))
                        .Build())
                .Build();
            Assert.AreEqual(
                @"usingSystem;namespaceHelloWorld{publicclassProgram{publicstaticvoidMain(String[]args){Console.WriteLine(""Hello world"");Console.ReadLine();}}}",
                @class.ToString());
        }
    }
}
