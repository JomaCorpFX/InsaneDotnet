using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Insane.Exe
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string pattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
            var match = Regex.Match("6.0.1-preview.40.4-5-5+5AAABBsdswewrwer-5", pattern, RegexOptions.Multiline);
            bool succcess = match.Success;
            var major = match.Groups[1].Value;
            var minor = match.Groups[2].Value;
            var patch = match.Groups[3].Value;
            var prerelease = match.Groups[4].Value;
            var buildmetadata = match.Groups[5].Value;


            Console.ReadLine();
            
        }

    }
}
