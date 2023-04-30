namespace InsaneIO.Insane.Extensions
{
    public static class HexEncodingExtensions
    {
        public static string ToHex(this byte[] data, bool toUpper = false)
        {
            data.ThrowIfNull();
            StringBuilder ret = new(string.Empty);
            foreach (byte value in data)
            {
                ret.Append(value.ToString(toUpper? "X2": "x2"));
            }
            return ret.ToString();
        }

        public static string ToHex(this string data, bool toUpper = false)
        {
            return ToHex(data.ToByteArrayUtf8(), toUpper);
        }

        public static byte[] FromHex(this string data)
        {
            byte[] ret = new byte[data.Length / 2];
            for (int i = 0; i < data.Length / 2; i++)
            {
                ret[i] = Convert.ToByte(data.Substring(i * 2, 2), 16);
            }
            return ret;
        }
    }
}
