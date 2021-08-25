using Insane.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework.ValueGeneration
{
    public class EncoderValueGenerator : ValueGenerator<string>
    {
        private readonly IProperty property;
        private readonly IEncoder encoder;

        public EncoderValueGenerator(IProperty property, IEncoder encoder)
        {
            this.property = property;
            this.encoder = encoder ?? Base64Encoder.Instance;
        }

        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            int maxLen = property.GetMaxLength() ?? EfConstants.GuidLength;
            return encoder.Encode(RandomManager.Next(maxLen)).Substring(0, maxLen);
        }
    }
}
