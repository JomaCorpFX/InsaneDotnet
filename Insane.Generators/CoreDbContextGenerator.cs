using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Insane.Generators
{
    [Generator]
    public class CoreDbContextGenerator : ISourceGenerator
    {
        private static readonly (string DbProviderPreffix, string DbProviderInterfaceName)[] dbContextInterfaces = { ("SqlServer", "ISqlServerDbContext"), ("PostgreSql", "IPostgreSqlDbContext"), ("MySql", "IMySqlDbContext"), ("Oracle", "IOracleDbContext") };
        private const string classSuffix = "CoreDbContextBase";
        private const string attributeName = "CoreDbContext";

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG1
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
            context.RegisterForSyntaxNotifications(() => new CoreDbContextSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {

            var contextClasses = context.Compilation.SyntaxTrees
                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
                .Where(x => x is ClassDeclarationSyntax cds && cds.Identifier.ValueText.EndsWith(classSuffix, StringComparison.OrdinalIgnoreCase)
                && !cds.Identifier.ValueText.Equals(classSuffix)
                && cds.AttributeLists.SelectMany(al => al.Attributes).Where(at => at.Name.ToString().Equals(attributeName)).Any()
                )
                .Cast<ClassDeclarationSyntax>();

            foreach (var contextClass in contextClasses)
            {
                IEnumerable<string>? typeParameters = contextClass?.TypeParameterList?.Parameters.Select(p => p.ToFullString());
                if (typeParameters.Any() && typeParameters.Count() == 1)
                {
                    string dbContextRawName = $"{contextClass?.Identifier.ValueText.Replace(classSuffix, string.Empty)}";
                    string? dbContextNamespace = contextClass?.GetNamespaceFrom();
                    foreach (var value in dbContextInterfaces)
                    {
                        string contextClassName = $"{dbContextRawName}{value.DbProviderPreffix}DbContext";
                        StringBuilder sourceBuilder = new StringBuilder($@"// DbContext generated from {(dbContextNamespace == null ? $"{contextClass?.Identifier.ValueText}<{string.Join(",", typeParameters)}>": dbContextNamespace + "." + $"{contextClass?.Identifier.ValueText}<{string.Join(",", typeParameters)}>")}  

using Microsoft.EntityFrameworkCore;
using Insane.EntityFrameworkCore;
");
                        sourceBuilder.Append(dbContextNamespace == null ? string.Empty : $@"
namespace {dbContextNamespace}
{{");
                        sourceBuilder.Append(dbContextNamespace == null ? $@" 
public partial class {contextClassName} : {contextClass?.Identifier.ValueText}<{contextClassName}>, {value.DbProviderInterfaceName}
{{
    public {contextClassName}(DbContextOptions<{contextClassName}> options) : base(options)
    {{
    }}
}}
" : $@" 
    public partial class {contextClassName} : {contextClass?.Identifier.ValueText}<{contextClassName}>, {value.DbProviderInterfaceName}
    {{
        public {contextClassName}(DbContextOptions<{contextClassName}> options) : base(options)
        {{
        }}
    }}
");
                        sourceBuilder.AppendLine(dbContextNamespace == null ? string.Empty : $"}}");
                        context.AddSource($"{contextClassName}.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
                    };
                }

            };



        }


        public class CoreDbContextSyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax cds)
                {
                    //if(cds.Identifier.ValueText.EndsWith("CoreDbContextBase") && cds.HasAnnotation(new SyntaxAnnotation("CoreDbContext")))
                    {
                        Classes.Add(cds);
                    }
                }
            }


        }
    }
}
