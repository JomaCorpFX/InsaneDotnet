using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Insane.Interop
{
    public class Exchange
    {
        [DllImport(@"InsaneNative.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetI(byte[] data, int a, int b, int c, int d, int e, ref int sz);

        [DllImport(@"InsaneNative.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetJ(byte[] data, int[] parts, ref int sz);

        [DllImport(@"InsaneNative.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void FreeAP(IntPtr ptr);

        public static String GetS(String data, int a, int b, int c, int d, int e)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            int sz = bytes.Length;
            IntPtr ptr = GetI(bytes, a, b, c, d, e, ref sz);
            byte[] ret = new byte[sz];
            Marshal.Copy(ptr, ret, 0, sz);
            FreeAP(ptr);
            return Encoding.UTF8.GetString(ret);
        }

        public static String GetS(String data, Boolean toJson)
        {
            int[] parts = new int[5];
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            int size = bytes.Length;
            IntPtr ptr = GetJ(bytes, parts, ref size);
            byte[] ret = new byte[size];
            Marshal.Copy(ptr, ret, 0, size);
            FreeAP(ptr);
            if(toJson)
            {
                return $"{{ \"Data\": \"{Encoding.UTF8.GetString(ret)}\", \"A\": {parts[0]}, \"B\": {parts[1]}, \"C\": {parts[2]}, \"D\": {parts[3]}, \"E\": {parts[4]} }}";
            }
            else
            {
                return $"Exchange.GetS(\"{Encoding.UTF8.GetString(ret)}\", {parts[0]}, {parts[1]}, {parts[2]}, {parts[3]}, {parts[4]});";
            }

        }

    }
}
