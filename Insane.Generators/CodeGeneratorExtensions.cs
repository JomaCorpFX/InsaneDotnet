using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Insane.Generators
{
    public static class CodeGeneratorExtensions
    {
        public static string GetNamespace(this SyntaxNode node)
        {
            StringBuilder ret = new StringBuilder();
            while (true)
            {
                switch (node)
                {
                    case NamespaceDeclarationSyntax ns:
                        ret.Insert(0, ret.Length == 0 ? $"{ns.Name}" : $"{ns.Name}.");
                        break;
                    case CompilationUnitSyntax:
                    case null:
                        return ret.ToString();
                }
                node = node.Parent!;
            }
        }

        public static IEnumerable< string> GetCompilationUnitUsings(this SyntaxNode node)
        {
            List<string> usings = new List<string>();
            while (true)
            {
                switch (node)
                {
                    case CompilationUnitSyntax compilationUnit:
                        foreach(var uusing in compilationUnit.Usings)
                        {
                            usings.Add(uusing.Name.ToString());
                        }
                        return usings;
                    case null:
                        return usings;
                    default:
                        node = node.Parent!;
                        break;
                } 
            }
        }

        public static string ToHex(this byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ToMd5(this byte[] data)
        {
            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(data).ToHex();
            }
        }
        public static string ToMd5(this string data)
        {
            return ToMd5(Encoding.UTF8.GetBytes(data));
        }


    }
}
