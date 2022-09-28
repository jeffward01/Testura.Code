namespace Testura.Code.Builders.BuildMembers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class MethodBuildMember : IBuildMember
{
    private readonly BaseMethodDeclarationSyntax[] _methodDeclarationSyntaxs;

    public MethodBuildMember(BaseMethodDeclarationSyntax[] methodDeclarationSyntaxs)
    {
        _methodDeclarationSyntaxs = methodDeclarationSyntaxs;
    }

    public SyntaxList<MemberDeclarationSyntax> AddMember(
        SyntaxList<MemberDeclarationSyntax> members)
    {
        foreach (var methodDeclarationSyntax in _methodDeclarationSyntaxs)
        {
            members = members.Add(methodDeclarationSyntax);
        }

        return members;
    }
}
