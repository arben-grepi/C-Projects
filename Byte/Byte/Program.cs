using System.Security.Cryptography;

namespace Byteharjoittelua
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte aByte = 1;
            PrintByteValue(aByte);

            byte oneByteBits = 0b00000011;
            PrintByteValue(oneByteBits);

            byte letterA = 97;
            PrintByteValue(letterA);
            PrintByteAsChar(letterA);

            PrintByteAsBits(letterA);

        }
        static void PrintByteValue(byte aByte)
        {
            Console.WriteLine("Tavu kokonaislukuna " + aByte);
        }
        static void PrintByteAsChar(byte aByte)
        {
            char merkki =  (char)aByte;
            Console.WriteLine("Tavu merkkinä: " + merkki);
        }
        static void PrintByteAsBits(byte aByte)
        {
            string bitString = Convert.ToString(aByte, 2).PadLeft(8, '0');

            Console.WriteLine(aByte + " Tavu bitteinä: " + bitString);


        }
      
    }

}