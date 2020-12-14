using Insane.Cryptography;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Insane.LiveTest
{
    class Program
    {
        public static void RsaManagerTest()
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
            keyPair = RsaManager.CreateKeyPair(4096, RsaKeyEncoding.Xml,false);
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
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!\n\n");
            RsaManagerTest();
            string pattern = @"[0-9]{4}";
            string input = @"4444";
            Console.WriteLine(Regex.IsMatch(input, pattern, RegexOptions.Singleline, TimeSpan.FromSeconds(1))? "OK": "BAD");
            string str = "A";
            str = HashManager.ToBase64(str, 0, false);
            Console.WriteLine(str);
            Console.WriteLine(HashManager.ToString(HashManager.FromBase64(str)));
            Console.ReadLine();
        }
    }
}
