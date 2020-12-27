using Insane.Cryptography;
using Scrypt;
using System;
using System.Diagnostics;
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

        static void HashManagerTests()
        {
            string str = "A";
            str = HashManager.ToBase64(str, 0, false);
            Console.WriteLine(str);
            Console.WriteLine(HashManager.ToString(HashManager.FromBase64(str)));
        }
        static void Main(string[] args)
        {
            //RsaManagerTests();
            //HashManagerTests();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string data = "mypassword";
            ScryptResult hash = HashManager.ToBase64Scrypt(data, 32768,8,1, 16, 4);
            Console.WriteLine(hash.Hash);
            Console.WriteLine(HashManager.ToBase64Scrypt(data, HashManager.FromBase64( hash.Salt)).Hash, 32768, 8, 1, 1025);
            sw.Stop();
            Console.WriteLine("Elapsed seconds: " + sw.ElapsedMilliseconds/1000.0);
            Console.WriteLine("Elapsed milliseconds: " + sw.ElapsedMilliseconds);
            ScryptEncoder encoder = new ScryptEncoder();

            string hashedPassword = encoder.Encode("mypassword");

            

            bool areEquals = encoder.Compare("mypassword", hashedPassword);
            Console.WriteLine(hashedPassword);
            HashManagerTests();
            Console.ReadLine();
        }
    }
}
