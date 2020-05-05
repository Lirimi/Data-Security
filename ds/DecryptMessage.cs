using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace ds
{
    public class DecryptMessage
    {
        public void Decrypt(string ciphertext)
        {
            /*---- Ndajme stringun ku ka seperator '.' ----*/
            string[] ciphertextSplit = {};
            
            if (!File.Exists(ciphertext))
            {
                ciphertextSplit = ciphertext.Split('.');
            }
            else
            {
                string encryptedCipher = File.ReadAllText(@ciphertext);
                ciphertextSplit = encryptedCipher.Split('.');
            }
            
            /*----- Incializojme strings nga String Array ----*/
            string username = ciphertextSplit[0];
            string iv = ciphertextSplit[1];
            string key = ciphertextSplit[2];
            string encryptedMessage = ciphertextSplit[3];
            
            string user = DecodeUserName(username);
            byte[] DecryptetKey = DecryptKeywithRSA(username, key);
            String DecryptedText = DecryptCipher(encryptedMessage, iv, DecryptetKey);
            Console.WriteLine("Marresi: " + user);
            Console.WriteLine("Mesazhi: " + DecryptedText);
        }

        private string DecodeUserName(string username)
        {
            /*-----Gets Bytes from String------*/
            byte[] GetUserNameBytes = Convert.FromBase64String(username);

            /*-----Encode String from byte array----*/
            string GetUserName = Encoding.UTF8.GetString(GetUserNameBytes);

            /*----Checks if private key of user exits in dir keys----*/
            bool checkUser = CheckifFUserExists(GetUserName);
            if (checkUser)
            {
                return GetUserName;
            }
            
            throw new Exception("Celsi privat keys/" + GetUserName + ".xml nuk ekziston");
        }

        private bool CheckifFUserExists(string username)
        {
            if (File.Exists("keys//" + username + ".xml")) 
            {
                return true;
            }

            return false;
        }

        private byte[] DecryptKeywithRSA(string username, string key)
        {
            /*-----------Krijojme nje RSA instance--------*/
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();

            /*------Gets Bytes from key--------*/
            byte[] GetKeyBytes = Convert.FromBase64String(key);

            /*------Therrasim funksionin qe mundeson dekodimin e user-----*/
            string user = DecodeUserName(username);

            /*------Lexojme XML dokumentin ne string------*/
            string strXmlParameters = "";
            StreamReader sr = new StreamReader("keys//" + user + ".xml");
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
            /*------E lexojme stringun ne instance-----*/
            objRSA.FromXmlString(strXmlParameters);

            /*------Dekriptojme qelsin-------*/
            byte[] Decryptedkey = objRSA.Decrypt(GetKeyBytes, true);

            return Decryptedkey;
        }


        private string DecryptCipher(string encryptedMessage, string iv, byte[] key)
        {
            /*------Krijojme nje Des Instance-------*/
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            
            /*------Gets Bytes from encryptet message and IV-----*/
            byte[] getEncryptedBytes = Convert.FromBase64String(encryptedMessage);
            byte[] getIVbytes = Convert.FromBase64String(iv);

            /* --Ne instance marrim iv, qelsin, vleren e modes dhe padding--*/
            objDES.IV = getIVbytes;
            objDES.Key = key;
            objDES.Mode = CipherMode.ECB;
            objDES.Padding = PaddingMode.Zeros;

           
            /*--------- Dekripotojme Bytat e enkriptuar --------*/
            MemoryStream ms = new MemoryStream(getEncryptedBytes);
            byte[] byteDecrypted = new byte[ms.Length];
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(byteDecrypted, 0, byteDecrypted.Length);
            cs.Close();
            
            /*------- Kthen  string te enkoduar nga bajtat -------*/ 
            return Encoding.UTF8.GetString(byteDecrypted);

        }

    }
}