using Insane.Cryptography;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework.ValueGeneration
{
    public class Base64ValueGenerator : ValueGenerator<string>
    {
        private readonly int maxLength;

        public Base64ValueGenerator(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool GeneratesTemporaryValues => false;


        public override string Next(EntityEntry entry)
        {
            return Base64Encoder.Instance.Encode(RandomManager.Next(maxLength)).Substring(0, maxLength);
        }
    }
}
