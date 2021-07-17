using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insane.Extensions;
using Insane.Cryptography;

namespace Insane.Tests
{
    [TestClass]
    public class RsaExtensionsUnitTests
    {
        private void TestCreateKeyPair(uint size, RsaKeyEncoding encoding, bool indented)
        {
            var keyPair = size.CreateRsaKeyPair(encoding, indented);
            Console.WriteLine($"PublicKey: {Environment.NewLine}{keyPair.PublicKey}");
            Console.WriteLine($"PrivateKey: {Environment.NewLine}{keyPair.PrivateKey}");
            Assert.IsNotNull(keyPair);
        }

        [TestMethod]
        public void TestCreateKeyPairBerEncodedIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Ber, true);
        }

        [TestMethod]
        public void TestCreateKeyPairBerEncodedNotIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Ber, false);
        }

        [TestMethod]
        public void TestCreateKeyPairJsonEncodedIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Json, true);
        }

        [TestMethod]
        public void TestCreateKeyPairJsonEncodedNotIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Json, false);
        }

        [TestMethod]
        public void TestCreateKeyPairPemEncodedIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Pem, true);
        }

        [TestMethod]
        public void TestCreateKeyPairPemEncodedNotIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Pem, false);
        }

        [TestMethod]
        public void TestCreateKeyPairXmlEncodedIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Xml, true);
        }

        [TestMethod]
        public void TestCreateKeyPairXmlEncodedNotIndented()
        {
            TestCreateKeyPair(4096u, RsaKeyEncoding.Xml, false);
        }

        
    }
}
