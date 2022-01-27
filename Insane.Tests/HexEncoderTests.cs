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
    public class HexEncoderUnitTests
    {
        private readonly byte[] testTytes = new byte[] { 0xff, 0xa, 1, 0x22 };
        private string testhexStringUppercase = "FF0A0122";
        private string testhexStringLowercase = "ff0a0122";
        [TestMethod]
        public void TestDecodeUppercase()
        {
            byte[] bytes = HexEncoder.Instance.Decode(testhexStringUppercase);
            Assert.IsTrue(Enumerable.SequenceEqual(bytes, testTytes));
        }

        [TestMethod]
        public void TestDecodeLowercase()
        {
            byte[] bytes = HexEncoder.Instance.Decode(testhexStringLowercase);
            Assert.IsTrue(Enumerable.SequenceEqual(bytes, testTytes));
        }

        [TestMethod]
        public void TestEncodeUpper()
        {
            IEncoder encoder = new HexEncoder(true);
            Assert.AreEqual(testhexStringUppercase, encoder.Encode(testTytes));
        }

        [TestMethod]
        public void TestEncodeLower()
        {
            IEncoder encoder = new HexEncoder(false);
            Assert.AreEqual(testhexStringLowercase, encoder.Encode(testTytes));
        }

    }

    
}
