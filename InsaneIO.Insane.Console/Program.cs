// See https://aka.ms/new-console-template for more information
using InsaneIO.Insane.Cryptography;
using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Security;
using System.Runtime.Versioning;

[RequiresPreviewFeatures]
internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        TotpManager manager = new()
        {
            Secret = "xdatasecret".ToByteArrayUtf8(), 
            Issuer = "X-Data",
            Label = "user@x-data.com"
        };
        var serialized = manager.Serialize();
        manager = TotpManager.Deserialize(serialized);
            Console.WriteLine(manager.ToOtpUri());
        while(true)
        {
            Console.WriteLine(manager.ComputeCode() + "- Remaining: " + DateTimeOffset.Now.ComputeTotpRemainingSeconds(manager.TimePeriodInSeconds));
            await Task.Delay(1000);
        }
        Console.ReadLine();
    }
}