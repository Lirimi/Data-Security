using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ds
{
    public class EncryptMessage
    {
        public void Encrypt(string userName, string message, [Optional, DefaultParameterValue(0)] object ToPath)
        {
            byte[] iv = new byte[8];
            byte[] key = new byte[8];

            GenerateKeys(iv, key);
            string encodeUserName = EncodeName(userName);
            string ivToString = Convert.ToBase64String(iv);
            string encryptKey = EncryptKeyWithRSA(userName, key);
            byte[] encryptMessage = EncryptMessageWithDES(message, key, iv);
            string signedData = SignData(encryptMessage, "keys//Jon.xml");

            /*-----CIPHERTEXT-----*/
            String encryptedtext = String.Format("{0}.{1}.{2}.{3}.{4}", encodeUserName, ivToString, encryptKey, 
                Convert.ToBase64String(encryptMessage), signedData);

            if (ToPath.Equals(0))
            {
                Console.WriteLine(encryptedtext);
            }
            else
            {
                File.WriteAllText(ToPath.ToString(), encryptedtext);
            }
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

        private byte[] EncryptMessageWithDES(string message, byte[] key, byte[] iv)
        {
            /* -------Kthejme Plaintext ne Byte Array------------*/
            byte[] messageToByte = Encoding.UTF8.GetBytes(message);

            /* -------Krijojme nje DES instance----------*/
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();

            /* --Ne instance marrim iv, qelsin, vleren e modes dhe padding--*/
            objDES.IV = iv;
            objDES.Key = key;
            objDES.Mode = CipherMode.CBC;
            objDES.Padding = PaddingMode.Zeros;

            /*--- Krijojme nje memory stream dhe cryptostream  ---*/
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(messageToByte, 0, messageToByte.Length);

            /*------ Largojme padding bllokun ------*/
            cs.FlushFinalBlock();
            byte[] byteCiphertext = ms.ToArray();

            return byteCiphertext;
        }

        

        public static string SignData(byte[] message, string privateKey)
        {
            //// The array to store the signed message in bytes
            byte[] signedBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                //// Write the message to a byte array using UTF8 as the encoding.
                var encoder = new UTF8Encoding();
                //byte[] originalData = encoder.GetBytes(message);

                try
                {
                    //Get RSA private key of sender from path//
                    string RSAParameters = "";
                    StreamReader sr = new StreamReader(privateKey);
                    RSAParameters = sr.ReadToEnd();
                    sr.Close();
                    rsa.FromXmlString(RSAParameters);

                    //// Import the private key used for signing the message
                    RSAParameters exportPrivateParameters = rsa.ExportParameters(true);
                    RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider();
                    rsa2.ImportParameters(exportPrivateParameters);


                    //// Sign the data, using SHA512 as the hashing algorithm 
                    signedBytes = rsa.SignData(message, CryptoConfig.MapNameToOID("SHA512"));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                finally
                {
                    //// Set the keycontainer to be cleared when rsa is garbage collected.
                    rsa.PersistKeyInCsp = false;
                }
            }

            //// Convert the a base64 string before returning
            return Convert.ToBase64String(signedBytes);
        }
    }
}