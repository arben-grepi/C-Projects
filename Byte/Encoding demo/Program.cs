using System.Dynamic;

namespace Encoding_demo
{
    internal class Program
    {
       
        static void Main(string[] args)
        {

            byte[] bytes = PrintMessagesBytes("mitä älämölöä täältä kuuluu",  System.Text.Encoding.UTF8);
            PrintMessage(bytes, System.Text.Encoding.ASCII);
            PrintMessage(bytes, System.Text.Encoding.UTF8);

            string base64String = PrintBytesAsBase64String(bytes);
            PrintBase64StringAsString(base64String, System.Text.Encoding.UTF8);
        }
        static byte[] PrintMessagesBytes(string message, System.Text.Encoding encoding)
        {
            Console.WriteLine("Merkkijono tavuiksi");
            Console.WriteLine("Encoding: " + encoding.EncodingName);
            Console.WriteLine("Teksti: " + message);

            byte[] bytes = encoding.GetBytes(message);

            Console.WriteLine($"Taulukon pituus {bytes.Length}");

            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine();
            return bytes;
        }
        static void PrintMessage(byte[] bytes, System.Text.Encoding encoding)
        {
            Console.WriteLine("Tavut merkkijonoksi: ");
            Console.WriteLine("Encoding: " + encoding.EncodingName);
            var message = encoding.GetString(bytes);
            Console.WriteLine("Taulukko merkkijonona: " + message);
            Console.WriteLine();


        }
        static string PrintBytesAsBase64String(byte[] bytes)
        {
            string base64String = Convert.ToBase64String(bytes);
            Console.WriteLine("Taulukko base64 muodossa");
            Console.WriteLine();

            return base64String;
        }
        static void PrintBase64StringAsString(string base64String, System.Text.Encoding encoding)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            string message = encoding.GetString(bytes);

            Console.WriteLine("Base64 merkkijono: " + base64String);
            Console.WriteLine("Encode: " + encoding.EncodingName);
            Console.WriteLine("Merkkijono: " + message);
        }
    }

}