using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ds
{
    public class EncryptMessage
    {
        TokenStatus TS = new TokenStatus();

        public void Encrypt(string userName, string message, [Optional, DefaultParameterValue(0)] object TokenorPath,
            [Optional, DefaultParameterValue(0)] object ToPath)
        {
            byte[] iv = new byte[8];
            byte[] key = new byte[8];

            GenerateKeys(iv, key);
            string encodeUserName = EncodeName(userName);
            string ivToString = Convert.ToBase64String(iv);
            string encryptKey = EncryptKeyWithRSA(userName, key);
            byte[] encryptMessage = EncryptMessageWithDES(message, key, iv);

            // Check if third parameter is a valid formatted JWT token
            var jwtHandler = new JwtSecurityTokenHandler();
            bool readableToken = jwtHandler.CanReadToken(TokenorPath.ToString());
            
            string encryptedtext = "";

            if (!TokenorPath.Equals(0))
            {
               
                if (readableToken)
                {
                    bool TokenisValid = TS.Status(TokenorPath.ToString());
                    if (!TokenisValid)
                        throw new Exception("Tokeni nuk eshte valid");
                    string user = TS.UserofJWT;
                    
                    byte[] senderbytes = Encoding.UTF8.GetBytes(user);
                    string signedData = SignData(encryptMessage, user);

                    /*-----CIPHERTEXT-----*/
                    encryptedtext = String.Format("{0}.{1}.{2}.{3}.{4}.{5}", encodeUserName, ivToString, encryptKey,
                        Convert.ToBase64String(encryptMessage), Convert.ToBase64String(senderbytes), signedData);
                    if (ToPath.Equals(0))
                        Console.WriteLine(encryptedtext);
                    else
                    {
                        File.WriteAllText(ToPath.ToString(), encryptedtext);
                        Console.WriteLine("Mesazhi i enkriptuar u ruajt ne filen " + ToPath);
                    }
                }
                else if (!readableToken && !ToPath.Equals(0))
                    throw new Exception(
                        "Invalid Arguments! Fourth Argument isn't a valid token hence it must be path already!");
                else if (!readableToken)
                {
                    encryptedtext = String.Format("{0}.{1}.{2}.{3}", encodeUserName, ivToString, encryptKey,
                        Convert.ToBase64String(encryptMessage));
                    File.WriteAllText(TokenorPath.ToString(), encryptedtext);
                    Console.WriteLine("Mesazhi i enkriptuar u ruajt ne filen " + TokenorPath);
                }
            }
            else
            {
                encryptedtext = String.Format("{0}.{1}.{2}.{3}", encodeUserName, ivToString, encryptKey,
                    Convert.ToBase64String(encryptMessage));
                Console.WriteLine(encryptedtext);
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
            if(!File.Exists("keys//" + userName + ".pub.xml")) 
                throw new Exception("Celsi publik i marresit " + userName + " nuk ekziston!"); 
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
                try
                {
                    //Get RSA private key of sender from path//
                    string RSAParameters = "";
                    StreamReader sr = new StreamReader("keys//" + privateKey + ".xml");
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