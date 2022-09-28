namespace Testura.Code.Builders;

using Base;
using BuildMembers;
using Generators.Class;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

/// <summary>
///     Provides a builder to generate a class.
/// </summary>
public class ClassBuilder : TypeBuilderBase<ClassBuilder>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ClassBuilder" /> class.
    /// </summary>
    /// <param name="name">Name of the class.</param>
    /// <param name="namespace">Name of the class namespace.</param>
    /// <param name="namespaceType">Type of namespace</param>
    public ClassBuilder(
        string name, string @namespace, NamespaceType namespaceType = NamespaceType.Classic)
        : base(name, @namespace, namespaceType)
    {
    }

    /// <summary>
    ///     Add class constructor.
    /// </summary>
    /// <param name="constructor">An already generated constructor.</param>
    /// <returns>The current class builder</returns>
    public ClassBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
    {
        return With(new ConstructorBuildMember(constructor));
    }

    /// <summary>
    ///     Add class fields.
    /// </summary>
    /// <param name="fields">A set of wanted fields.</param>
    /// <returns>The current class builder</returns>
    public ClassBuilder WithFields(params Field[] fields)
    {
        return With(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
    }

    /// <summary>
    ///     Add class fields.
    /// </summary>
    /// <param name="fields">An array of already declared fields.</param>
    /// <returns>The current class builder</returns>
    public ClassBuilder WithFields(params FieldDeclarationSyntax[] fields)
    {
        return With(new FieldBuildMember(fields));
    }

    protected override TypeDeclarationSyntax BuildBase()
    {
        return SyntaxFactory.ClassDeclaration(Name)
            .WithBaseList(CreateBaseList())
            .WithModifiers(CreateModifiers());
    }
}
