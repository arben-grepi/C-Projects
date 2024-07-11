using System.Text;
using System.Security.Cryptography;

namespace Karelia.Security.Demos
{
    public class MyRSA
    {

        private RSA rsa;

        public MyRSA()
        {
            rsa = RSA.Create(2048);

        }

        /// <summary>
        /// SALAUS
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Encrypt(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            var encryptedBytes = rsa.Encrypt(bytes, RSAEncryptionPadding.OaepSHA1);
            var base64 = Convert.ToBase64String(encryptedBytes);
            return base64;
        }
        /// <summary>
        /// PURKU
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Decrypt(string message) 
        {
            var bytes = Convert.FromBase64String(message);
            var decrypted = rsa.Decrypt(bytes, RSAEncryptionPadding.OaepSHA1);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            return decryptedMessage;

        }
        public string ExportPublicKey()
        {
            var key = rsa.ExportRSAPublicKey();
            return Convert.ToBase64String(key);

        }
        public void ImportPublicKey(string base64PublicKey)
        {
            var keyBites = Convert.FromBase64String(base64PublicKey);
            rsa.ImportRSAPublicKey(keyBites, out _);


        }
        public string ExportPrivateKey(string password)
        {
            var keyParams = new PbeParameters(PbeEncryptionAlgorithm.Aes128Cbc, HashAlgorithmName.SHA256, 100000);
            var passwordBytes = Encoding.UTF8.GetBytes(password);


            var privateKey = rsa.ExportEncryptedPkcs8PrivateKey(passwordBytes, keyParams);
            var base64 = Convert.ToBase64String(privateKey);
            return base64;



        }
        public void ImportEncryptedPrivateKey(string encryptedBase64Key, string password)
        {
            var keyBytes = Convert.FromBase64String(encryptedBase64Key);
            rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), keyBytes, out _);
        }


    }
}
