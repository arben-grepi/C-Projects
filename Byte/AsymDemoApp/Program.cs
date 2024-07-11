using System.Security.Cryptography;
using System.Text;

namespace Byteharjoittelua
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "Timo Taikuri";
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);

            RSA rsa = RSA.Create(2048);
            byte[] encryptedBytes = rsa.Encrypt(nameBytes, RSAEncryptionPadding.OaepSHA1);

            var base64 = Convert.ToBase64String(encryptedBytes);

            Console.WriteLine(name + " RSAsalattuna: " + base64);

            var decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA1);

            name = Encoding.UTF8.GetString(decryptedBytes);

            Console.WriteLine($"Salaus purettuna: {name}");


        }
    }
}