using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.EntityFrameworkCore.ValueGeneration
{
    [RequiresPreviewFeatures]
    public class HexValueGenerator : ValueGenerator<string>
    {
        private readonly uint maxLength;

        public HexValueGenerator(uint maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool GeneratesTemporaryValues => false;


        public override string Next(EntityEntry entry)
        {
            return HexEncoder.DefaultInstance.Encode(RandomManager.Next(maxLength)).Substring(0, (int)maxLength);
        }
    }
}
