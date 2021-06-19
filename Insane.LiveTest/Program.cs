using Insane.Cryptography;

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Insane.LiveTest
{
    class Program
    {

        public static void RsaManagerTests()
        {
            string data = "HelloWorld!!!";

            Console.WriteLine("█ BER");
            RsaKeyPair keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Ber);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            string encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            Console.WriteLine("█ XML");
            keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Xml, false);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            Console.WriteLine("█ XML INDENTED");
            keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Xml);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            Console.WriteLine("█ JSON");
            keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Json, false);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            Console.WriteLine("█ JSON INDENTED");
            keyPair = RsaManager.CreateKeyPair(512, RsaKeyEncoding.Json);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            Console.WriteLine("█ PEM");
            keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Pem);
            Console.WriteLine("Public:\n" + keyPair.PublicKey);
            Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            encrypted = RsaManager.EncryptToBase64(data, keyPair.PublicKey);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + RsaManager.DecryptFromBase64(encrypted, keyPair.PrivateKey));

        }

        static void HashManagerTests()
        {
            string str = "A";
            str = HashManager.ToBase64(str, 0, false);
            Console.WriteLine(str);
            Console.WriteLine(HashManager.ToString(HashManager.FromBase64(str)));
        }


        public record JwtToken(string Token, string Jti, DateTimeOffset ExpirationTime)
        {
            public string x { get; set; } = "";

        }

      

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var jwtToken = new JwtToken("Token", "Jti", DateTimeOffset.Now);
            jwtToken.x = "Joma";
            var jwtToken2 = jwtToken;
            jwtToken.x = "JomaCorp";

            Console.WriteLine(jwtToken == jwtToken2);
            Console.WriteLine(jwtToken.Equals(jwtToken2));
            Console.WriteLine(jwtToken.GetHashCode());
            Console.WriteLine(jwtToken2.GetHashCode());
            jwtToken2.x = "X";
            Console.WriteLine(jwtToken);
            Console.WriteLine(jwtToken2);

            var jw3 = jwtToken2 with { Token = "MyToken XXX" };
            var jw4 = jw3 with { };
            jw3.x = "Antonio";
            Console.WriteLine(jw3);
            Console.WriteLine(jw4);

            var (Token, Jti, ExpirationTime) = jwtToken2;

            sw.Stop();
            Console.ReadLine();
        }
    }
}
