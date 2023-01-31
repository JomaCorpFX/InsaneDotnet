//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Text;
//using Microsoft.CSharp;
//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Diagnostics;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace Insane.Generators
//{
//    class SyntaxReceiver : ISyntaxReceiver
//    {
//        readonly List<SyntaxNode> _candidateSyntaxes = new List<SyntaxNode>();

//        public IReadOnlyList<SyntaxNode> CandidateSyntaxes => _candidateSyntaxes;

//        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
//        {
//            if (!(syntaxNode is ClassDeclarationSyntax c))
//                return;

//            //_candidateSyntaxes.Add(syntaxNode);
//        }
//    }

//    //[Generator(LanguageNames.CSharp)]
//    public class CoreDbContextGenerator : ISourceGenerator
//    {
//        private static readonly (string DbProviderPreffix, string DbProviderInterfaceName)[] dbContextInterfaces = { ("SqlServer", "ISqlServerDbContext"), ("PostgreSql", "IPostgreSqlDbContext"), ("MySql", "IMySqlDbContext"), ("Oracle", "IOracleDbContext") };
//        private const string DbContextAttributeName = "CoreDbContext";
//        private const string DbContextAttributeFullName = "Insane.EntityFrameworkCore.CoreDbContext";
//        private const string DbContextFactoryAttributeName = "CoreDbContextFactory";
//        private const string DbContextFactoryAttributeFullName = "Insane.EntityFrameworkCore.CoreDbContextFactory";

//        public void Initialize(GeneratorInitializationContext context)
//        {
//#if DEBUG1
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
//            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
//        }


//        private static readonly DiagnosticDescriptor DuplicatedCoreDbContext = new DiagnosticDescriptor(id: "INSANE0001",
//                                                                                              title: "CoreDbContextAttribute",
//                                                                                              messageFormat: @"[CoreDbContext(""{0}"")] in class ""{1}"". Duplicated DbContext name ""{0}"". No DbContexts generated for this name.",
//                                                                                              category: "Insane.EntityFrameworkCore.CoreDbContext",
//                                                                                              DiagnosticSeverity.Warning,
//                                                                                              isEnabledByDefault: true);
//        private static readonly DiagnosticDescriptor DuplicatedCoreDbContextFactory = new DiagnosticDescriptor(id: "INSANE0002",
//                                                                                              title: "CoreDbContextFactoryAttribute",
//                                                                                              messageFormat: @"[CoreDbContextFactory(""{0}"")] in class ""{1}"". Duplicated DbContextfactory name ""{0}"". No DbContextFactories generated for this name.",
//                                                                                              category: "Insane.EntityFrameworkCore.CoreDbContextFactory",
//                                                                                              DiagnosticSeverity.Warning,
//                                                                                              isEnabledByDefault: true);

//        private static readonly DiagnosticDescriptor NotFoundDbContextFactory = new DiagnosticDescriptor(id: "INSANE0003",
//                                                                                              title: "DbContextFactoryAttribute",
//                                                                                              messageFormat: @"[CoreDbContextFactory(""{0}"")] in class ""{1}"". No DbContext with name ""{0}"" was found. No DbContextFactories generated for this DbContext name.",
//                                                                                              category: "Insane.EntityFrameworkCore.CoreDbContextFactory",
//                                                                                              DiagnosticSeverity.Warning,
//                                                                                              isEnabledByDefault: true);
//        private static readonly DiagnosticDescriptor InvalidIdentifierDbContext = new DiagnosticDescriptor(id: "INSANE0004",
//                                                                                              title: "CoreDbContextAttribute",
//                                                                                              messageFormat: @"[CoreDbContext({0})] in class ""{1}"". Invalid DbContext identifier name <{0}>. A valid string literal is needed. No DbContexts generated for this name.",
//                                                                                              category: "Insane.EntityFrameworkCore.CoreDbContext",
//                                                                                              DiagnosticSeverity.Warning,
//                                                                                              isEnabledByDefault: true);

//        public string GenerateDbContextFactoryCode(string dbContextFactoryName, string dbContextFactoryBaseClassName, string dbContextFactoryNamespace, string dbProviderPreffix)
//        {
//            string dbContextFactoryClassName = $"{dbContextFactoryName}{dbProviderPreffix}DbContextFactory";
//            string dbContextClassName = $"{dbContextFactoryName}{dbProviderPreffix}DbContext";
//            StringBuilder sourceBuilder = new StringBuilder();

//            if (string.IsNullOrWhiteSpace(dbContextFactoryNamespace))
//            {
//                sourceBuilder.Append($@"public partial class {dbContextFactoryClassName} : {dbContextFactoryBaseClassName}<{dbContextClassName}>
//{{

//}}
//");
//            }
//            else
//            {
//                sourceBuilder.Append($@"namespace {dbContextFactoryNamespace}
//{{
//    public partial class {dbContextFactoryClassName} : {dbContextFactoryBaseClassName}<{dbContextClassName}>
//    {{
        
//    }}
//}}
//");
//            }
//            return sourceBuilder.ToString();
//        }

//        private string GenerateDbContextConstructors(string dbContextClassName, IEnumerable<ConstructorDeclarationSyntax> constructors, bool withNamespace)
//        {
//            StringBuilder sourceBuilder = new StringBuilder();
//            string namespaceTab = withNamespace ? "\t" : string.Empty;
//            for (int i = 0; i < constructors.Count(); i++)
//            {
//                sourceBuilder.Append($"{namespaceTab}\tpublic {dbContextClassName}{constructors.ElementAt(i).ParameterList}:base(");
//                int j;
//                for (j = 0; j < constructors.ElementAt(i).ParameterList.Parameters.Count - 1; j++)
//                {
//                    sourceBuilder.Append($"{constructors.ElementAt(i).ParameterList.Parameters.ElementAt(j).Identifier.ValueText}, ");
//                }
//                sourceBuilder.Append($"{constructors.ElementAt(i).ParameterList.Parameters.ElementAt(j).Identifier.ValueText}){Environment.NewLine}");
//                sourceBuilder.Append($"{namespaceTab}\t{{{Environment.NewLine}{namespaceTab}\t}}{Environment.NewLine}{Environment.NewLine}");
//            }
//            string source = sourceBuilder.ToString();
//            return source.Substring(0, source.Length - Environment.NewLine.Length);
//        }

//        public string GenerateDbContextCode(string dbContextName, string parentDbContextClassName, string dbContextNamespace, string dbProviderInterfaceName, string dbProviderPreffix, IEnumerable<ConstructorDeclarationSyntax> constructors)
//        {
//            string dbContextClassName = $"{dbContextName}{dbProviderPreffix}DbContext";
//            StringBuilder sourceBuilder = new StringBuilder();
//            bool withNamespace = !string.IsNullOrWhiteSpace(dbContextNamespace);
//            string namespaceTab = withNamespace ? "\t" : string.Empty;
//            sourceBuilder.Append($"{namespaceTab}public partial class {dbContextClassName} : {parentDbContextClassName}, {dbProviderInterfaceName}{Environment.NewLine}");
//            sourceBuilder.Append($"{namespaceTab}{{{Environment.NewLine}");
//            sourceBuilder.Append(GenerateDbContextConstructors(dbContextClassName, constructors, withNamespace));
//            sourceBuilder.Append($"{namespaceTab}}}{Environment.NewLine}");
//            return sourceBuilder.ToString();
//        }

//        private static Regex IdentifierRegex = new Regex("^[a-zA-Z_]\\w*$");
        

//        public void Execute(GeneratorExecutionContext context)
//        {
            
//            var contextClasses = context.Compilation.SyntaxTrees
//                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
//                .Where(x => x is ClassDeclarationSyntax)
//                .Cast<ClassDeclarationSyntax>()
//                .Select(cls => new
//                {
//                    Namespace = cls.GetNamespace(),
//                    Class = cls,
//                    DbContextNames = cls.AttributeLists.SelectMany(atLst => atLst.Attributes).Where(at =>
//                    {
//                        string attributeName = new string(at.Name.ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
//                        return (attributeName.Equals(DbContextAttributeName) || attributeName.Equals(DbContextAttributeFullName) || attributeName.Equals($"{DbContextAttributeName}Attribute") || attributeName.Equals($"{DbContextAttributeFullName}Attribute"));
//                    }).Select(at =>
//                    {
//                        return new
//                        {
//                            Name = at.ArgumentList?.Arguments.First().Expression is LiteralExpressionSyntax ? (at.ArgumentList?.Arguments.First().Expression as LiteralExpressionSyntax)!.Token.ValueText : $"\0{at.ArgumentList?.Arguments.First().Expression}",
//                            Name2 = at.ArgumentList?.Arguments.First().Expression.ToString(),
//                            TextSpan = at.ArgumentList?.Arguments.First().Span
//                        };
//                    }),
//                })
//                .Where(e =>
//                {
//                    return e.DbContextNames.Any();
//                })
//                .SelectMany(e =>
//                {
//                    return e.DbContextNames.Select(n => new
//                    {
//                        e.Namespace,
//                        n.Name2,
//                        DbContextName = n.Name ?? string.Empty,
//                        DbContextNameTextSpan = n.TextSpan,
//                        DbContextBaseClassName = e.Class.Identifier.ValueText,
//                        e.Class
//                    });
//                });

//            var factoryClasses = context.Compilation.SyntaxTrees
//                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
//                .Where(x => x is ClassDeclarationSyntax)
//                .Cast<ClassDeclarationSyntax>()
//                .Select(cls => new
//                {
//                    Namespace = cls.GetNamespace(),
//                    Class = cls,
//                    DbContextFactoryNames = cls.AttributeLists.SelectMany(al => al.Attributes).Where(at =>
//                    {
//                        string name = new string(at.Name.ToString().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
//                        var parameterNames = cls.TypeParameterList?.Parameters.Select(p => p.ToFullString());
//                        return parameterNames is not null && parameterNames.Count() == 1 && (name.Equals(DbContextFactoryAttributeName) || name.Equals(DbContextFactoryAttributeFullName) || name.Equals($"{DbContextFactoryAttributeName}Attribute") || name.Equals($"{DbContextFactoryAttributeFullName}Attribute"));
//                    }).Select(at => new { Name = ((LiteralExpressionSyntax)at?.ArgumentList?.Arguments.First().Expression!).Token.ValueText, TextSpan = at.Span }),
//                })
//                .Where(e => e.DbContextFactoryNames.Any())
//                .SelectMany(e =>
//                {
//                    return e.DbContextFactoryNames.Select(n => new
//                    {
//                        e.Namespace,
//                        DbContextFactoryName = n.Name,
//                        DbContextFactoryNameTextSpan = n.TextSpan,
//                        DbContextFactoryBaseClassName = e.Class.Identifier.ValueText,
//                        e.Class,
//                    });
//                });

           
//            var dbContextClasses = contextClasses.Select(e => new
//            {
//                e.Namespace,
//                e.DbContextName,
//                ExpressionText = e.Name2,
//                e.DbContextBaseClassName,
//                Discarded = contextClasses.Where(c => c.Namespace.Equals(e.Namespace) && c.DbContextName.Equals(e.DbContextName)).Count() > 1,
//                e.DbContextNameTextSpan,
//                e.Class,
//                IsValidDbContextName = IdentifierRegex.IsMatch(e.DbContextName),
//                Constructors = e.Class.Members.Where(m => m is ConstructorDeclarationSyntax).Cast<ConstructorDeclarationSyntax>()
//            });

//            var dbContextFactoryClasses = factoryClasses.Select(e => new
//            {
//                e.Namespace,
//                e.DbContextFactoryName,
//                e.DbContextFactoryBaseClassName,
//                Discarded = factoryClasses.Where(c => c.Namespace.Equals(e.Namespace) && c.DbContextFactoryName.Equals(e.DbContextFactoryName)).Count() > 1,
//                e.DbContextFactoryNameTextSpan,
//                e.Class,
//            });

//            foreach (var discardedClass in dbContextClasses.Where(e => e.Discarded || !e.IsValidDbContextName))
//            {
//                if (discardedClass.Discarded)
//                {
//                    context.ReportDiagnostic(Diagnostic.Create(DuplicatedCoreDbContext, Location.Create(discardedClass.Class.SyntaxTree, discardedClass.DbContextNameTextSpan!.Value), $"{discardedClass.DbContextName}", $"{(string.IsNullOrWhiteSpace(discardedClass.Namespace) ? "global::" : $"{discardedClass.Namespace}.")}{discardedClass.DbContextBaseClassName}"));
//                }
//                if(!discardedClass.IsValidDbContextName)
//                {
//                    context.ReportDiagnostic(Diagnostic.Create(InvalidIdentifierDbContext, Location.Create(discardedClass.Class.SyntaxTree, discardedClass.DbContextNameTextSpan!.Value), $"{discardedClass.ExpressionText}", $"{(string.IsNullOrWhiteSpace(discardedClass.Class.GetNamespace()) ? "global::" : $"{discardedClass.Class.GetNamespace()}.")}{discardedClass.Class.Identifier.ValueText}"));
//                }
//            }


//            foreach (var discardedClass in dbContextFactoryClasses.Where(e => e.Discarded))
//            {
//                context.ReportDiagnostic(Diagnostic.Create(DuplicatedCoreDbContextFactory, Location.Create(discardedClass.Class.SyntaxTree, discardedClass.DbContextFactoryNameTextSpan), $"{discardedClass.DbContextFactoryName}", $"{(string.IsNullOrWhiteSpace(discardedClass.Namespace) ? "global::" : $"{discardedClass.Namespace}.")}{discardedClass.DbContextFactoryBaseClassName}"));
//            }

//            StringBuilder generated = new StringBuilder();
//            dbContextClasses = dbContextClasses.Where(e => !e.Discarded && e.IsValidDbContextName);
//            List<string> usings = new List<string> {
//                "Microsoft.EntityFrameworkCore",
//                "Insane.EntityFrameworkCore"
//            };
//            foreach (var selectedClass in dbContextClasses)
//            {
//                usings.AddRange(selectedClass.Class.GetCompilationUnitUsings());
//            }

//            usings.Distinct(StringComparer.InvariantCulture).ToList().ForEach(us =>
//            {
//                generated.AppendLine($"using {us};");
//            });
//            generated.AppendLine();


//            var namespaces = dbContextClasses.GroupBy(e => e.Namespace);
//            foreach (var ns in namespaces)
//            {
//                generated.Append(string.IsNullOrWhiteSpace(ns.Key) ? string.Empty : $@"namespace {ns.Key}{Environment.NewLine}");
//                generated.Append(string.IsNullOrWhiteSpace(ns.Key) ? string.Empty : $"{{{Environment.NewLine}");
//                foreach (var selectedClass in dbContextClasses.Where(e => e.Namespace.Equals(ns.Key)))
//                {
//                    foreach (var dbContextInterface in dbContextInterfaces)
//                    {
//                        var source = GenerateDbContextCode(selectedClass.DbContextName, selectedClass.DbContextBaseClassName, selectedClass.Namespace, dbContextInterface.DbProviderInterfaceName, dbContextInterface.DbProviderPreffix, selectedClass.Constructors);
//                        generated.AppendLine(source);
//                    }
//                }
//                generated.Append(string.IsNullOrWhiteSpace(ns.Key) ? string.Empty : $"}}{Environment.NewLine}");
//            }


//            foreach (var selectedClass in dbContextFactoryClasses.Where(e => !e.Discarded))
//            {
//                if (dbContextClasses.Where(e => selectedClass.Namespace.Equals(e.Namespace) && selectedClass.DbContextFactoryName.Equals(e.DbContextName)).Any())
//                {
//                    foreach (var dbContextInterface in dbContextInterfaces)
//                    {
//                        var source = GenerateDbContextFactoryCode(selectedClass.DbContextFactoryName, selectedClass.DbContextFactoryBaseClassName, selectedClass.Namespace, dbContextInterface.DbProviderPreffix);
//                        generated.AppendLine(source);
//                    }
//                }
//                else
//                {
//                    context.ReportDiagnostic(Diagnostic.Create(NotFoundDbContextFactory, Location.Create(selectedClass.Class.SyntaxTree, selectedClass.DbContextFactoryNameTextSpan), $"{selectedClass.DbContextFactoryName}", $"{(string.IsNullOrWhiteSpace(selectedClass.Namespace) ? "global::" : $"{selectedClass.Namespace}.")}{selectedClass.DbContextFactoryBaseClassName}"));
//                }
//            }
//            var str = generated.ToString();
//            Console.WriteLine(str);
//            context.AddSource("CoreDbContextsAndFactories.g.cs", SourceText.From(generated.ToString(), Encoding.UTF8));
//        }

//    }
//}

