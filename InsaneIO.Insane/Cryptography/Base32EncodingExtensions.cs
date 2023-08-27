namespace InsaneIO.Insane.Extensions
{
    //Original author: https://stackoverflow.com/a/7135008 https://stackoverflow.com/users/904128/shane
    public static class Base32EncodingExtensions
    {

        private static int CharToValue(char c)
        {
            return c switch
            {
                char value when (value < 91 && value > 64) => value - 65,
                char value when (value < 56 && value > 49) => value - 24,
                char value when (value < 123 && value > 96) => value - 97,
                _ => throw new ArgumentException("Character is not a Base32 character.")
            };
        }

        private static char ValueToChar(byte value, bool toLower)
        {
            return value switch
            {
                var b when (b < 26) => (char)(b + (toLower ? 97 : 65)),
                var b when (b < 32) => (char)(b + 24),
                _ => throw new ArgumentException("Byte is not a value Base32 value.")
            };
        }
        
        public static string EncodeToBase32(this byte[] data, bool removePadding = false, bool toLower = false)
        {
            data.ThrowIfNull();
            int charCount = (int)Math.Ceiling(data.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in data)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar, toLower);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar, toLower);
                    bitsRemaining += 5;
                }
                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar, toLower);
                if (!removePadding)
                {
                    while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';
                }
            }
            return new string(returnArray,0, arrayIndex-1);
        }

        public static string EncodeToBase32(this string data, bool removePadding = false, bool toLower = false)
        {
            return EncodeToBase32(data.ToByteArrayUtf8(), removePadding, toLower);
        }

        public static byte[] DecodeFromBase32(this string data)
        {

            data = data.Trim().TrimEnd('=');
            int byteCount = data.Length * 5 / 8;
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0;
            int bitsRemaining = 8;
            int arrayIndex = 0;

            foreach (char c in data)
            {
                int cValue = CharToValue(c);
                int mask;
                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }
            return returnArray;
        }


    }
}