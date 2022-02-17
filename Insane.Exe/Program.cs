using System;
using System.Text;

namespace Insane.Exe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Insane.Cryptography.HexEncoder.Instance.Encode(Encoding.UTF8.GetBytes("grape")));
            Console.ReadLine();
            
        }

    }
}
