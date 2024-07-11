using System.Security.Cryptography;

namespace Viikkotehtävä2
{
    public class Tehtävä1
    {
        public const int TavuNum = 8;
        enum PrintStyle
        {
            Tavut,
            Base64,
            Kahdeksan_bitin_sarjoina
        }
        static void Main(string[] args)
        {
            var cryptoRandomBytes = CreateRandomBytes(TavuNum);

            PrintBytes(cryptoRandomBytes, PrintStyle.Tavut);
            PrintBytes(cryptoRandomBytes, PrintStyle.Base64);
            PrintBytes(cryptoRandomBytes, PrintStyle.Kahdeksan_bitin_sarjoina);


        }
        static byte[] CreateRandomBytes(int count)
        {
            byte[] bytes = new byte[0];
            bytes = RandomNumberGenerator.GetBytes(count);
            return bytes;
        }
        static void PrintBytes(byte[] bytes, PrintStyle style)
        {
            Console.Write(style + ": ");
            switch (style)
            {
                case PrintStyle.Tavut:
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        Console.Write(bytes[i]);
                        if (i != bytes.Length - 1)
                        {
                            Console.Write(", ");
                        }

                    }
                    break;

                case PrintStyle.Base64:
                    string base64String = Convert.ToBase64String(bytes);
                    Console.WriteLine(base64String);
                    break;

                case PrintStyle.Kahdeksan_bitin_sarjoina:
                    Console.WriteLine();
                    foreach (var abite in bytes)
                    {
                        string bitString = Convert.ToString(abite, 2).PadLeft(8, '0');
                        Console.WriteLine(bitString);

                    }

                    break;
            }
            Console.WriteLine("\n");
        }


    }



}