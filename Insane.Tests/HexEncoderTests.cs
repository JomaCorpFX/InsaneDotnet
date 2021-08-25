using Insane.Cryptography;
using Insane.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Tests
{
    [TestClass]
    public class HexEncoderTests
    {
        [TestMethod]
        public void TestDecodeUppercase()
        {
            string hexString = "FF0A0122";
            byte[] bytes = HexEncoder.Instance.Decode(hexString);
            Assert.IsTrue(Enumerable.SequenceEqual(bytes, new byte[] { 0xff, 0xa, 1, 0x22}));
        }

        [TestMethod]
        public void TestDecodeLowercase()
        {
            string hexString = "ff0a0122";
            byte[] bytes = HexEncoder.Instance.Decode(hexString);
            Assert.IsTrue(Enumerable.SequenceEqual(bytes, new byte[] { 0xff, 0xa, 1, 0x22 }));
        }

        [TestMethod]
        public void TestEncodeUpper()
        {
            string hexData = "FF0A0122";
            byte[] bytes = new byte[] { 0xff, 0xa, 1, 0x22 };
            IEncoder encoder = new HexEncoder(true);
            Assert.AreEqual(hexData, encoder.Encode(bytes));
        }

        [TestMethod]
        public void TestEncodeLower()
        {
            string hexData = "ff0a0122";
            byte[] bytes = new byte[] { 0xff, 0xa, 1, 0x22 };
            IEncoder encoder = new HexEncoder(false);
            Assert.AreEqual(hexData, encoder.Encode(bytes));
        }

    }

    
}
