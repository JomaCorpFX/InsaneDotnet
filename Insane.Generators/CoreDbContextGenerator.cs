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
        private const string DbContextClassSuffix = "CoreDbContextBase";
        private const string DbContextFactoryClassSuffix = "CoreDbContextFactoryBase";
        private const string DbContextAttributeName = "CoreDbContext";
        private const string DbContextFactoryAttributeName = "CoreDbContextFactory";

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG1
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
        }

        public void Execute(GeneratorExecutionContext context)
        {

            var contextClasses = context.Compilation.SyntaxTrees
                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
                .Where(x => x is ClassDeclarationSyntax cds && cds.Identifier.ValueText.EndsWith(DbContextClassSuffix, StringComparison.OrdinalIgnoreCase)
                && !cds.Identifier.ValueText.Equals(DbContextClassSuffix)
                && cds.AttributeLists.SelectMany(al => al.Attributes).Where(at => at.Name.ToString().Equals(DbContextAttributeName)).Any()
                ).Cast<ClassDeclarationSyntax>();

            foreach (var contextClass in contextClasses)
            {
                IEnumerable<string>? contextClassTypeParameters = contextClass?.TypeParameterList?.Parameters.Select(p => p.ToFullString());
                if (contextClassTypeParameters.Any() && contextClassTypeParameters.Count() == 1)
                {
                    var contextFactoryClass = context.Compilation.SyntaxTrees
                        .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
                        .Where(x => x is ClassDeclarationSyntax cds && cds.Identifier.ValueText.EndsWith(DbContextFactoryClassSuffix, StringComparison.OrdinalIgnoreCase)
                        && !cds.Identifier.ValueText.Equals(DbContextFactoryClassSuffix)
                        && cds.AttributeLists.SelectMany(al => al.Attributes).Where(at => at.Name.ToString().Equals(DbContextFactoryAttributeName)).Any()
                        ).Cast<ClassDeclarationSyntax>().FirstOrDefault();

                    string dbContextRawName = $"{contextClass?.Identifier.ValueText.Replace(DbContextClassSuffix, string.Empty)}";
                    string? dbContextNamespace = contextClass?.GetNamespaceFrom();
                    string? dbContextFactoryNamespace = contextFactoryClass?.GetNamespaceFrom();
                    foreach (var value in dbContextInterfaces)
                    {
                        string contextClassName = $"{dbContextRawName}{value.DbProviderPreffix}DbContext";
                        StringBuilder sourceBuilder = new StringBuilder($@"// DbContext generated from {(dbContextNamespace == null ? $"{contextClass?.Identifier.ValueText}<{string.Join(",", contextClassTypeParameters)}>" : dbContextNamespace + "." + $"{contextClass?.Identifier.ValueText}<{string.Join(",", contextClassTypeParameters)}>")}  

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

                        if(contextFactoryClass != null)
                        {
                            string contextFactoryClassName = $"{dbContextRawName}{value.DbProviderPreffix}DbContextFactory";
                            sourceBuilder.Clear();
                            sourceBuilder.Append(dbContextNamespace == null ? string.Empty : $@"using {dbContextNamespace};
");
                            sourceBuilder.Append(dbContextFactoryNamespace == null ? string.Empty : $@"
namespace {dbContextFactoryNamespace}
{{");
                            sourceBuilder.Append(dbContextNamespace == null ? $@"
public class {contextFactoryClassName} : {contextFactoryClass?.Identifier.ValueText}<{contextFactoryClassName}>
{{
}}
" :
$@"
    public class {contextFactoryClassName} : {contextFactoryClass?.Identifier.ValueText}<{contextClassName}>
    {{
    }}
");
                            sourceBuilder.AppendLine(dbContextFactoryNamespace == null ? string.Empty : $"}}");
                            var source = sourceBuilder.ToString();
                            context.AddSource($"{contextFactoryClassName}.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
                        }
                    };
                }

            };



        }

    }
}
