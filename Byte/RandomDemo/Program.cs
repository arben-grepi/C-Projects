using System.Security.Cryptography;

namespace Encoding_demo
{
    internal class Program
    {
        enum RandomSource
        {
            System, 
            Cryptography
        }
        enum PrintStyle
        {
            Bytes,
            Base64String,
            UTF8
        }

        static void Main(string[] args)
        {
            const int Count = 10;

            Console.WriteLine("Some random things to share");

            var systemRandomBytes = CreateRandomBytes(Count, RandomSource.System);
            var cryptoRandomBytes = CreateRandomBytes(Count, RandomSource.Cryptography);

            //PrintBytes(systemRandomBytes, PrintStyle.Bytes);
            //PrintBytes(cryptoRandomBytes, PrintStyle.Bytes);

            //PrintBytes(systemRandomBytes, PrintStyle.Base64String);
            //PrintBytes(cryptoRandomBytes, PrintStyle.Base64String);

            PrintBytes(systemRandomBytes, PrintStyle.UTF8);
            PrintBytes(cryptoRandomBytes, PrintStyle.UTF8);





        }
        static byte[] CreateRandomBytes(int count, RandomSource randomSource)
        {
            byte[] bytes = new byte[0];
            switch (randomSource)
            {   
                case RandomSource.System:
                    bytes = new byte[count];
                    System.Random r = new System.Random();
                    r.NextBytes(bytes);
                    break;

                case RandomSource.Cryptography:
                    bytes = RandomNumberGenerator.GetBytes(count);
                    break;
            }
            return bytes;
        }
        static void PrintBytes(byte[] bytes, PrintStyle style)
        {
            Console.WriteLine("Style: " + style);
            switch (style)
            {
                case PrintStyle.Bytes:
                    foreach (byte b in bytes) Console.WriteLine(b);

                    break;
                     
                case PrintStyle.Base64String:
                    string base64String = Convert.ToBase64String(bytes);
                    Console.WriteLine(base64String);
                    break;

                case PrintStyle.UTF8:
                    string utfString = System.Text.Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(utfString);
                    break;
            }
        }
        
   
    }

}