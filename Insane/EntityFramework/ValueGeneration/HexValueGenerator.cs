using Insane.Cryptography;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework.ValueGeneration
{
    public class HexValueGenerator : ValueGenerator<string>
    {
        private readonly int maxLength;

        public HexValueGenerator(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool GeneratesTemporaryValues => false;


        public override string Next(EntityEntry entry)
        {
            return HexEncoder.Instance.Encode(RandomManager.Next(maxLength)).Substring(0, maxLength);
        }
    }
}
