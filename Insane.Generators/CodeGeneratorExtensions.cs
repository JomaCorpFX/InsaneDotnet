using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.Generators
{
    public static class CodeGeneratorExtensions
    {
        public static string? GetNamespaceFrom(this SyntaxNode s)
        {
            switch(s.Parent)
            {
                case NamespaceDeclarationSyntax ns:
                    return ns.Name.ToString();
                case null: 
                    return null;
                default:
                    return GetNamespaceFrom(s.Parent);
            }
        }

        
    }
}
