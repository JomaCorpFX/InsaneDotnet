using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insane.Extensions;
using Insane.Cryptography;
using System.Diagnostics;

namespace Insane.Tests
{
    [TestClass]
    public class AesExtensionsUnitTests
    {
        public const string Data = "Hello World!!!";
        public const string Key = "12345678";
        private void TestEncryptDecryptAes(string data, string key, IEncoder encoder)
        {
            string encrypted = data.EncryptAes(key, encoder);
            string decrypted = encrypted.DecryptAes(key, encoder);

            Console.WriteLine($"Data: {data}");
            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");

            Assert.AreEqual(data, decrypted);
        }

        [TestMethod]
        public void TestEncryptDecryptAesWithBase64()
        {
            TestEncryptDecryptAes(Data, Key, Base64Encoder.Instance);
        }

        [TestMethod]
        public void TestEncryptDecryptAesWithHex()
        {
            TestEncryptDecryptAes(Data, Key, HexEncoder.Instance);
        }

        [TestMethod]
        public void TestEncryptDecryptAesWithEmptyData()
        {
            TestEncryptDecryptAes(string.Empty, Key, Base64Encoder.Instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptDecryptAesWithEmptyKey()
        {
            TestEncryptDecryptAes(Data, string.Empty, Base64Encoder.Instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEncryptDecryptAesWithNullKey()
        {
            TestEncryptDecryptAes(Data, null!, Base64Encoder.Instance);
        }

        [TestMethod]
        public void TestEncryptDecryptAesWithNullData()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                TestEncryptDecryptAes(null!, Key, Base64Encoder.Instance);
            });
        }

    }
}
