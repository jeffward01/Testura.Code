namespace Testura.Code.Builders.BuildMembers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public interface IBuildMember
{
    SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members);
}
