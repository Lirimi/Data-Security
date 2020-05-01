using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace ds
{
    public class DecryptMessage
    {
        static RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();

        public void Decrypt(string ciphertext)
        {
            Console.WriteLine("Teksti i dekriptuar: "+ DecryptCiphertext(ciphertext));
        }
        
        private static string DecryptCiphertext(string ciphertext)
        {
            byte[] byteCiphertext = Convert.FromBase64String(ciphertext);
            byte[] byteDecryptedtext = objRSA.Decrypt(byteCiphertext, true);

            return Encoding.UTF8.GetString(byteDecryptedtext);
            
        }
    }
}