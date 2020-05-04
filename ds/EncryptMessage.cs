using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ds
{
    public class EncryptMessage
    {
        public void EncryptToConsole(string userName, string message) 
        {
           
            byte[] iv = new byte[8];
            byte[] key = new byte[8];
            
            GenerateKeys(iv, key);
            string encodeUserName = EncodeName(userName);
            string ivToString = Convert.ToBase64String(iv);
            string encryptKey = EncryptKeyWithRSA(userName, key);
            string encryptMessage = EncryptMessageWithDES(message, key, iv);
            
            
            /*-----CIPHERTEXT-----*/
            String encryptedtext = String.Format("{0}.{1}.{2}.{3}", encodeUserName, ivToString, encryptKey, encryptMessage);
            Console.WriteLine(encryptedtext);

        }
        
        public void EncryptToPath(string userName, string message, string ToPath) 
        {
          
            byte[] iv = new byte[8];
            byte[] key = new byte[8];
            
            GenerateKeys(iv, key);
            string encodeUserName = EncodeName(userName);
            string ivToString = Convert.ToBase64String(iv);
            string encryptKey = EncryptKeyWithRSA(userName, key);
            string encryptMessage = EncryptMessageWithDES(message, key, iv);
            
            
            /*-----CIPHERTEXT-----*/
            String encryptedtext = String.Format("{0}.{1}.{2}.{3}", encodeUserName, ivToString, encryptKey, encryptMessage);
            File.WriteAllText(ToPath, encryptedtext);
        }

        private void GenerateKeys(byte[] iv, byte[] key)
        {
            Random random = new Random();
            /*--- Gjenerojme nje mode iv ----*/
            random.NextBytes(iv);
           
            
            /*---Gjenerojme nje random key ----*/
            random.NextBytes(key);
        }

        private string EncryptKeyWithRSA(string userName, byte[] key)
        {
            
            /*----Krijojme nje RSA instance-------*/
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();   
            
            /*----Lexojme RSA key parametrat prej XML file---*/
            string strXmlParameters = "";
            string publicKeyPath = "keys//" + userName + ".pub.xml";
            StreamReader sr = new StreamReader(publicKeyPath);
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
            objRSA.FromXmlString(strXmlParameters);

            /*------Enkriptojme qelsin--------*/
            byte[] byteCiphertext = objRSA.Encrypt(key, true);
                
            return Convert.ToBase64String(byteCiphertext);
        }

        private string EncodeName(string username)
        {
            /*------Enkodojme username ne byte array------*/
            byte[] encodeUserName = Encoding.UTF8.GetBytes(username);
            return Convert.ToBase64String(encodeUserName);
        }

        private string EncryptMessageWithDES(string message, byte[] key, byte[] iv)
        {
            /* -------Kthejme Plaintext ne Byte Array------------*/
            byte[] messageToByte = Encoding.UTF8.GetBytes(message);
            
            /* -------Krijojme nje DES instance----------*/
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            
            /* --Ne instance marrim iv, qelsin, vleren e modes dhe padding--*/
            objDES.IV = iv;
            objDES.Key = key;
            objDES.Mode = CipherMode.ECB;
            objDES.Padding = PaddingMode.Zeros;
            
            /*--- Krijojme nje memory stream dhe cryptostream  ---*/
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(messageToByte, 0, messageToByte.Length);
            
            /*------ Largojme padding bllokun ------*/
            cs.FlushFinalBlock();
            byte[] byteCiphertext = ms.ToArray();

            return Convert.ToBase64String(byteCiphertext);
        }
    }
}