namespace InsaneIO.Insane.Extensions
{
    public static class HexEncodingExtensions
    {
        public static string ToHex(this byte[] data)
        {
            data.ThrowIfNull();
            StringBuilder ret = new StringBuilder(string.Empty);
            foreach (byte Value in data)
            {
                ret.Append(Value.ToString("x2"));
            }
            return ret.ToString();
        }

        public static string ToHex(this string data)
        {
            return ToHex(data.ToByteArrayUtf8());
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
