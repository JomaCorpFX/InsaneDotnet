using InsaneIO.BlazorWebAssembly;
using InsaneIO.Insane.Cryptography;
using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Strings;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.Versioning;
[RequiresPreviewFeatures]
internal class Program
{

    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        //Console.WriteLine("    ".ToByteArrayUtf8().EncryptAesCbc("        ".ToByteArrayUtf8()).ToBase64());
        Console.WriteLine("    ".ToHash(Base64Encoder.DefaultInstance));
        Console.Write(StringsConstants.Underscore);

        await builder.Build().RunAsync();
    }
}