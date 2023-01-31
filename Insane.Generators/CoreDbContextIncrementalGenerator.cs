using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Insane.Generators
{
    [Generator()]
    public class IncrementalCoreDbContextGenerator : IIncrementalGenerator
    {
        private const string CoreDbContextAttributeFullName = "Insane.EntityFrameworkCore.CoreDbContextAttribute";
        private const string Clz = @"
namespace Insane.Generated
{
    public class HelloWorldClass
    {
        public static void Greetings()
        {
            System.Console.WriteLine(""HelloWorld"");//   
        }
    }
}
";
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            //throw new Exception("Test exception!"); // delete me after test
#if DEBUG
            SpinWait.SpinUntil(() => Debugger.IsAttached);
            // if (!Debugger.IsAttached)
            // {
            //     Debugger.Launch();
            // }
#endif
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource("GeneratorTests.g.cs", SourceText.From(Clz, Encoding.UTF8)));
            
            
        }
    }
}