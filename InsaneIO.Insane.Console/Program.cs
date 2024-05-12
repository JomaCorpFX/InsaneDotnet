using InsaneIO.Insane.Cryptography;
using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Security;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Unicode;


internal class Program
{


    private readonly static TotpManager manager = new()
    {
        Secret = "insaneiosecret".ToByteArrayUtf8(),
        Issuer = "InsaneIO",
        Label = "insane@insaneio.com"
    };

    public static async Task Main(string[] args)
    {
        

        Console.ReadLine();

        await Task.Delay(0);
    }
}