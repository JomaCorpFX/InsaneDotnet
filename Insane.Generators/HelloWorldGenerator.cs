// INSANE0001 | Insane.EntityFrameworkCore.CoreDbContext | Warning | CoreDbContextGenerator
// INSANE0002 | Insane.EntityFrameworkCore.CoreDbContextFactory | Warning | CoreDbContextGenerator
// INSANE0003 | Insane.EntityFrameworkCore.CoreDbContextFactory | Warning | CoreDbContextGenerator
// INSANE0004 | Insane.EntityFrameworkCore.CoreDbContext | Warning | CoreDbContextGenerator
//
//
// using Microsoft.CodeAnalysis;
// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

//namespace Insane.Generators
//{
//    [Generator]
//    public class HelloWorldGenerator : ISourceGenerator
//    {
//        public void Initialize(GeneratorInitializationContext context)
//        {

//        }

//        public void Execute(GeneratorExecutionContext context)
//        {

//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
//            var code = @"
//namespace Utils
//{
//    public static class HelloWorld 
//    {
//        public static void Show() 
//        {
//            System.Console.WriteLine(""Hello world!"");
//        }
//    }        
//}";
//            context.AddSource("HelloWorld.g.cs", code);
//        }
//    }
//}
