using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ds
{
    public class EncryptMessage
    {
        
        
        public void Encrypt(string userName, string message) 
        {
            byte[] key = new byte[8]; 
            Random random = new Random();
            random.NextBytes(key);

            string encodeUserName = EncodeName(userName);
            string encryptKey = EncryptKeyWithRSA(userName, key);
            string encryptMessage = EncryptMessageWithDES(message, key);

            Console.WriteLine(encodeUserName + "." + encryptKey + "." + encryptMessage);

        }

        private string EncryptKeyWithRSA(string userName, byte[] key)
        {
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();   
            string strXmlParameters = "";
            string publicKeyPath = "keys//" + userName + ".pub.xml";
            StreamReader sr = new StreamReader(publicKeyPath);
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
            objRSA.FromXmlString(strXmlParameters);
            
            
            //byte[] bytePlaintext = Encoding.UTF8.GetBytes(key);
            byte[] byteCiphertext = objRSA.Encrypt(key, true);
                
            return Convert.ToBase64String(byteCiphertext);
        }

        private string EncodeName(string username)
        {
            byte[] encodeUserName = Encoding.UTF8.GetBytes(username);
            return Convert.ToBase64String(encodeUserName);
        }

        private string EncryptMessageWithDES(string message, byte[] key)
        {
            byte[] messageToByte = Encoding.UTF8.GetBytes(message);
            
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = key;
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.ECB;
            
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(messageToByte, 0, messageToByte.Length);
            cs.Close();
            byte[] byteCiphertext = ms.ToArray();

            return Convert.ToBase64String(byteCiphertext);
        }


    }
}