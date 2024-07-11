using Karelia.Security.Demos;

namespace Byteharjoittelua
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("RSA-SALAUS");
            var rsa = new MyRSA();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Salaa = 0");
                Console.WriteLine("Pura = 1");
                Console.WriteLine("Näytä julkinen avain = 2");
                Console.WriteLine("Aseta julkinen avain ja salaa syöte = 3 ");
                Console.WriteLine("Näytä salainen avain = 4");
                Console.WriteLine("Muodosta RSA avaimien avulla = 5");

                var action = int.Parse(Console.ReadLine());
                var input = "";
                string encryptedText = "";
                string publicKey = "";
                string password = "";
                string privateKey = "";

                Console.WriteLine();

                switch (action)
                {
                    case 0:
                        Console.WriteLine("Anna salattava teksti");
                        input = Console.ReadLine();
                        encryptedText = rsa.Encrypt(input);
                        Console.WriteLine($"'{input}' salattuna: {encryptedText}");

                        break;
                    case 1:
                        Console.WriteLine("Anna salattu teksti: ");
                        encryptedText = Console.ReadLine();
                        input = rsa.Decrypt(encryptedText);
                        Console.WriteLine("Teksti ilman salausta: " + input);
                        break;
                    case 2:
                        Console.WriteLine("Public key " + rsa.ExportPublicKey());

                        break;
                    case 3:
                        Console.WriteLine("Anna julkinen avain");
                        publicKey = Console.ReadLine() ;
                        rsa.ImportPublicKey(publicKey);

                        Console.WriteLine("Anna salattava teksti");
                        input = Console.ReadLine();

                        rsa.ImportPublicKey(publicKey);
                        encryptedText = rsa.Encrypt(input) ;

                        Console.WriteLine(input + " salattuna: " + encryptedText);

                        break;
                    case 4:
                        Console.WriteLine("Anna salasana avaimen salaamiseen: ");
                        password = Console.ReadLine() ;
                        privateKey = rsa.ExportPrivateKey(password) ;
                        Console.WriteLine("Private key: " + privateKey);

                        break;
                    case 5:
                        Console.WriteLine("Anna salainen avain (base64 muodossa): ");
                        privateKey = Console.ReadLine() ;
                        Console.WriteLine("Anna salaisen avaimen salasana: ");
                        password = Console.ReadLine();
                        rsa.ImportEncryptedPrivateKey(privateKey, password);

                        break;



                }


            }

        }
    }
}
