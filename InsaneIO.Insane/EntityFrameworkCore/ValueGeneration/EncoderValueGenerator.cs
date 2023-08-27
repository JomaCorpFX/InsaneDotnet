using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.EntityFrameworkCore.ValueGeneration
{
    [RequiresPreviewFeatures]
    public class EncoderValueGenerator : ValueGenerator<string>
    {
        private readonly IProperty property;
        private readonly IEncoder encoder;

        public EncoderValueGenerator(IProperty property, IEncoder encoder)
        {
            this.property = property;
            this.encoder = encoder ?? Base64Encoder.DefaultInstance;
        }

        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            int maxLen = property.GetMaxLength() ?? EntityFrameworkCoreConstants.GuidLength;
            return encoder.Encode(RandomExtensions.NextBytes((uint)maxLen)).Substring(0, maxLen);
        }
    }
}
