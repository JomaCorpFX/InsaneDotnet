using Insane.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Device
{
    public static class DeviceInfo
    {
        private const String WINDOWS = "Windows";
        private const String LINUX = "Linux";
        private const String OSX = "macOS/OSX";
        private const String UNKNOWN = "Unknown";


        private static String GetRealDeviceId()
        {
            
            return HashManager.ToBase64Hash("DeviceID", HashAlgorithm.Sha256);
        }

        private static String _RealDeviceId = GetRealDeviceId();

        public static String RealDeviceId
        {
            get
            {
                return _RealDeviceId;
            }
        }

        public static String DeviceId
        {
            get
            {
                return HashManager.ToBase64Hash(RealDeviceId, HashAlgorithm.Sha256);
            }
        }

        public static String Manufacturer
        {
            get
            {
                return "Samsung";
            }
        }

        public static String DeviceNameOrModel
        {
            get
            {
                return "S8 Edge";
            }
        }

        public static String OSDescription
        {
            get
            {
                return RuntimeInformation.OSDescription;
            }
        }

        public static String Platform
        {
            get
            {
                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return WINDOWS;
                }

                if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return LINUX;
                }

                if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return OSX;
                }

                return UNKNOWN;
            }
        }

        public static String FriendlyName
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public static String Architecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.ToString();

            }
        }

        public static string ApplicationName
        {
            get
            {
                return Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;
            }
        }

        public static string ApplicationVersion
        {
            get
            {
                return Assembly.GetEntryAssembly()?.GetCustomAttributes<AssemblyFileVersionAttribute>().FirstOrDefault()?.Version.ToString()?? string.Empty;
            }
        }

        public static string ApplicationDescription
        {
            get
            {
                return Assembly.GetEntryAssembly()?.GetCustomAttributes<AssemblyDescriptionAttribute>().FirstOrDefault()?.Description ?? string.Empty;
            }
        }

        public static String Summary()
        {

            return $"DeviceId: {DeviceId}{Environment.NewLine}Real DeviceId: {RealDeviceId}{Environment.NewLine}Manufacturer: {Manufacturer}{Environment.NewLine}Device Name or Model: {DeviceNameOrModel}{Environment.NewLine}OS Description: {OSDescription}{Environment.NewLine}Platform: {Platform}{Environment.NewLine}Architecture: {Architecture}{Environment.NewLine}Friendly name: {FriendlyName}{Environment.NewLine}Application Name: {ApplicationName}{Environment.NewLine}Application Description: {ApplicationDescription}{Environment.NewLine}Application Version: {ApplicationVersion}";
        }
    }
}
