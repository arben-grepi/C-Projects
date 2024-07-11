using System.Security.Cryptography;

namespace SymmetricEncryptionApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Valitse salaa = salaus tai pura = salauksen purku");
            var action = Console.ReadLine();
            switch (action.ToLower())
            {
                case "salaa":
                    Encrypt();
                    break;

                case "pura":
                    Decrypt();
                    break;
                default:
                    Console.WriteLine("Valitsit väärin");
                    break;
            }


        }
        private static void Encrypt()
        {
            Console.WriteLine("Anna salattava teksti: ");    
            var text = Console.ReadLine();

            var key = RandomNumberGenerator.GetBytes(16);
            var iv = RandomNumberGenerator.GetBytes(16);

            Console.WriteLine("\t Key: " + Convert.ToBase64String(key));
            Console.WriteLine("\t Key iv: " + Convert.ToBase64String(iv));

            using (var aes = Aes.Create())
            {
                //128, 192, 256
                aes.Key = key;

                var bytes = System.Text.Encoding.UTF8.GetBytes(text);
                var encryptedBytes = aes.EncryptCbc(bytes, iv);
                var base64String = Convert.ToBase64String(encryptedBytes);
                Console.WriteLine("\t Salattu teksti: " +base64String);
            }
        }
        private static void Decrypt()
        {
            Console.WriteLine("Anna salattu teksti");
            var bytes = Convert.FromBase64String(Console.ReadLine());
            Console.WriteLine("Anna salausavain");
            var key = Convert.FromBase64String(Console.ReadLine());
            Console.WriteLine("Anna salausvektori");
            var iv = Convert.FromBase64String(Console.ReadLine());

            using (var aes = Aes.Create())
            {
                aes.Key = key;

                var decryptedBytes = aes.DecryptCbc(bytes, iv);
                var textString = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                Console.WriteLine(textString);
            }

        }


    }

}