﻿namespace Testura.Code.Builders.BuildMembers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class ClassBuildMember : IBuildMember
{
    private readonly TypeDeclarationSyntax _class;

    public ClassBuildMember(ClassBuilder classBuilder)
    {
        _class = classBuilder.BuildWithoutNamespace();
    }

    public ClassBuildMember(TypeDeclarationSyntax @class)
    {
        _class = @class;
    }

    public SyntaxList<MemberDeclarationSyntax> AddMember(
        SyntaxList<MemberDeclarationSyntax> members)
    {
        return members.Add(_class);
    }
}
