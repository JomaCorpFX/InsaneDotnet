using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.EntityFrameworkCore.ValueGeneration
{
    [RequiresPreviewFeatures]
    public class Base64ValueGenerator : ValueGenerator<string>
    {
        private readonly uint maxLength;

        public Base64ValueGenerator(uint maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool GeneratesTemporaryValues => false;


        public override string Next(EntityEntry entry)
        {
            return Base64Encoder.DefaultInstance.Encode(RandomExtensions.Next(maxLength)).Substring(0, (int)maxLength);
        }
    }
}
