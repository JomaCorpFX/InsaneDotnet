using InsaneIO.Insane.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InsaneIO.Insane.Tests
{
    [RequiresPreviewFeatures]
    [TestClass]
    public class TotpManagerUnitTests
    {
        [TestMethod]
        public async Task MyTestMethod()
        {
            int x = 0;
            while (x <= 2)
            {
                long seconds = TotpExtensions.ComputeTotpRemainingSeconds(DateTimeOffset.UnixEpoch.AddSeconds(60));
                Console.WriteLine(seconds);
                await Task.Delay(1000);
                x++;
            }
            Assert.AreEqual(1, 1);
        }
    }
}
