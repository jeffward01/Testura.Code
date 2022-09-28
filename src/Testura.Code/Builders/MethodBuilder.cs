using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Builders;

using Extensions;
using Factories;
using Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

/// <summary>
///     Provides a builder to generate a method
/// </summary>
public class MethodBuilder
{
    private readonly List<Modifiers> _modifiers;
    private readonly string _name;
    private readonly List<ParameterSyntax> _parameters;
    private readonly List<Parameter> _parameterXmlDocumentation;

    private SyntaxList<AttributeListSyntax> _attributes;
    private BlockSyntax _body;
    private SyntaxKind? _overrideOperator;

    private Type _returnType;
    private string _summary;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MethodBuilder" /> class.
    /// </summary>
    /// <param name="name">Name of the method</param>
    public MethodBuilder(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        _name = name.Replace(" ", "_");
        _parameters = new List<ParameterSyntax>();
        _modifiers = new List<Modifiers>();
        _parameterXmlDocumentation = new List<Parameter>();
        _body = BodyGenerator.Create();
    }

    /// <summary>
    ///     Build method and return the generated code.
    /// </summary>
    /// <returns>The generated method.</returns>
    public BaseMethodDeclarationSyntax Build()
    {
        var method = BuildMethodBase();
        method = BuildModifiers(method);
        method = BuildAttributes(method);
        method = method.WithSummary(_summary, _parameterXmlDocumentation);
        method = BuildParameters(method);
        method = BuildBody(method);

        return method;
    }

    /// <summary>
    ///     Set method attributs.
    /// </summary>
    /// <param name="attributes">A set of wanted attributes</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithAttributes(params Attribute[] attributes)
    {
        _attributes = AttributeGenerator.Create(attributes);

        return this;
    }

    /// <summary>
    ///     Set method attributes.
    /// </summary>
    /// <param name="attributes">A set of already generated attributes </param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
    {
        _attributes = attributes;

        return this;
    }

    /// <summary>
    ///     Set method body.
    /// </summary>
    /// <param name="body">The method body.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithBody(BlockSyntax? body)
    {
        _body = body;

        return this;
    }

    /// <summary>
    ///     Set the method body
    /// </summary>
    /// <param name="bodyOptions">
    ///     Can be configured to not generate a method body. (Common use-case:
    ///     generating interface methods)
    /// </param>
    /// <param name="body">The method body.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithBody(MethodBodyOptions bodyOptions, BlockSyntax? body = null)
    {
#pragma warning disable CS0472
        if (bodyOptions == null)
#pragma warning restore CS0472
        {
            throw new InvalidDataException("The body options cannot be null");
        }

        if (bodyOptions == MethodBodyOptions.WithoutBody)
        {
            _body = null;

            return this;
        }

        _body = body;

        return this;
    }

    /// <summary>
    ///     Set method modifiers.
    /// </summary>
    /// <param name="modifiers">A set of wanted modifiers.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithModifiers(params Modifiers[] modifiers)
    {
        _modifiers.Clear();
        _modifiers.AddRange(modifiers);

        return this;
    }

    /// <summary>
    ///     Set operator overloading.
    /// </summary>
    /// <param name="operator">Operator to overload</param>
    /// <returns>The current method builder.</returns>
    public MethodBuilder WithOperatorOverloading(Operators @operator)
    {
        _overrideOperator = OperatorFactory.GetSyntaxKind(@operator);

        return this;
    }

    /// <summary>
    ///     Set operator overloading.
    /// </summary>
    /// <param name="operator">Operator to overload</param>
    /// <returns>The current method builder.</returns>
    public MethodBuilder WithOperatorOverloading(SyntaxKind @operator)
    {
        _overrideOperator = @operator;

        return this;
    }

    /// <summary>
    ///     Set method parameters.
    /// </summary>
    /// <param name="parameters">A set of wanted parameters.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithParameters(params Parameter[] parameters)
    {
        _parameters.Clear();
        _parameterXmlDocumentation.Clear();

        foreach (var parameter in parameters)
        {
            _parameters.Add(ParameterGenerator.Create(parameter));

            if (parameter.XmlDocumentation != null)
            {
                _parameterXmlDocumentation.Add(parameter);
            }
        }

        return this;
    }

    /// <summary>
    ///     Set method parameters.
    /// </summary>
    /// <param name="parameters">A set of already generated parameters.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithParameters(params ParameterSyntax[] parameters)
    {
        _parameters.Clear();
        _parameters.AddRange(parameters);

        return this;
    }

    /// <summary>
    ///     Set method return type
    /// </summary>
    /// <param name="type">The wanted return type</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithReturnType(Type type)
    {
        _returnType = type;

        return this;
    }

    /// <summary>
    ///     Set method xml summary.
    /// </summary>
    /// <param name="summary">The method summary.</param>
    /// <returns>The current method builder</returns>
    public MethodBuilder WithSummary(string summary)
    {
        _summary = summary;

        return this;
    }

    private BaseMethodDeclarationSyntax BuildAttributes(BaseMethodDeclarationSyntax method)
    {
        return !_attributes.Any() ? method : method.WithAttributeLists(_attributes);
    }

    private BaseMethodDeclarationSyntax BuildBody(BaseMethodDeclarationSyntax method)
    {
        if (_body == null)
        {
            return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        return method.WithBody(_body);
    }

    private BaseMethodDeclarationSyntax BuildMethodBase()
    {
        if (_overrideOperator != null)
        {
            return OperatorDeclaration(IdentifierName(_name), Token(_overrideOperator.Value));
        }

        if (_returnType != null)
        {
            return MethodDeclaration(TypeGenerator.Create(_returnType), Identifier(_name));
        }

        return MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(_name));
    }

    private BaseMethodDeclarationSyntax BuildModifiers(BaseMethodDeclarationSyntax method)
    {
        if (_modifiers == null || !_modifiers.Any())
        {
            return method;
        }

        return method.WithModifiers(ModifierGenerator.Create(_modifiers.ToArray()));
    }

    private BaseMethodDeclarationSyntax BuildParameters(BaseMethodDeclarationSyntax method)
    {
        return !_parameters.Any()
            ? method
            : method.WithParameterList(
                ParameterGenerator.ConvertParameterSyntaxToList(_parameters.ToArray()));
    }
}
