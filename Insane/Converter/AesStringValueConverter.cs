using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Converter
{
    public class AesStringValueConverter : IValueConverter<string>
    {
        private readonly AesEncryptor encryptor;

        public AesStringValueConverter(AesEncryptor encryptor)
        {
            this.encryptor = encryptor;
        }

        public string Convert(string value) 
        {
            return encryptor.Encrypt(value);
        }

        public string Deconvert(string value) 
        {
            return encryptor.Decrypt(value);
        }
    }
}
