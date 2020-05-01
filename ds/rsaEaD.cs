using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ds
{
    public class rsaEaD
    {
        static RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        
        public void Encrypt(string userName, string message) 
        {
            
            string strXmlParameters = "";
            string publicKeyPath = "keys//" + userName + ".pub.xml";
            StreamReader sr = new StreamReader(publicKeyPath);
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
                
            objRSA.FromXmlString(strXmlParameters);
                
            byte[] byteCiphertext = EncryptPlaintext(message);

            Console.WriteLine(Convert.ToBase64String(byteCiphertext));
            
        }

        private static byte[] EncryptPlaintext(string plainText)
        {
            byte[] bytePlaintext = Encoding.UTF8.GetBytes(plainText);
            byte[] byteCiphertext = objRSA.Encrypt(bytePlaintext, true);
                
            return byteCiphertext;
        }

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