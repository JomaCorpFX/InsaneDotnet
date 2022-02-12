using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Generators
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"
namespace Utils
{
    public static class HelloWorld 
    {
        public static void Show() 
        {
            System.Console.WriteLine(""Hello world!"");
        }
    }        
}";
            context.AddSource("Generated.cs", code);
        }
    }
}
