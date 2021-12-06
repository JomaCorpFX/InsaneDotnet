using Insane.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
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

        private static char ValueToChar(byte value)
        {
            return value switch
            {
                var b when (b < 26) => (char)(b + 65),
                var b when (b < 32) => (char)(b + 24),
                _ => throw new ArgumentException("Byte is not a value Base32 value.")
            };
        }

        public static string ToBase32(this byte[] data, bool removePadding = false)
        {
            data.ThrowIfNull();
            int charCount = (int)Math.Ceiling(data.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in data)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }
                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                if (!removePadding)
                {
                    while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';
                }
            }

            return new string(returnArray).Replace("\0","");
        }

        public static string ToBase32(this string data, bool removePadding = false)
        {
            return ToBase32(data.ToByteArray());
        }

        public static byte[] FromBase32(this string data)
        {

            data = data.TrimEnd('=');
            int byteCount = data.Length * 5 / 8;
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
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




//_ = new byte[] { 0b11110101, 0b11110000, 0b11110000, 0b11110000, 0b11110000, 0b11110000, 0b11110000, 0b11110000, 0b11110000 }.ToBase32();
//  1               2           3           4           5               6           7           8               9           10
// █11110█101    11█11000█0   1111█0000   1█11100█00   111█10000     █11110█000    11█11000█0    1111█0000    1█11100█00    xxx█xxxxx
//111█10101█ - 4 - 8
//11█11010█1 - 3 - 7
//1█11101█01 - 2 - 6
//█11110█101 - 1 - 5