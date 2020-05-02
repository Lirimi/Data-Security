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
            string[] ciphertextSplit = ciphertext.Split('.');
            string username = ciphertextSplit[0];
            string iv = ciphertextSplit[1];
            string key = ciphertextSplit[2];
            string encryptedMessage = ciphertextSplit[3];
        }
        
         
        private string DecryptCiphertext(string ciphertext)
        {
            byte[] byteCiphertext = Convert.FromBase64String(ciphertext);
            byte[] byteDecryptedtext = objRSA.Decrypt(byteCiphertext, true);

            return Encoding.UTF8.GetString(byteDecryptedtext);
            
        }
    }
}