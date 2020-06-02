using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;

namespace ds
{
    public class Token
    {
        DatabaseConnection DB = new DatabaseConnection();
        static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

        private string Sign(string privateKey)
        {
            List<string> segments = new List<string>();

            var header = new {alg = "RS256", typ = "JWT"};

            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddMinutes(20);

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            //byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            segments.Add(Base64UrlEncode(headerBytes));
            //segments.Add(Base64UrlEncode(payloadBytes));

            string stringToSign = string.Join(".", segments.ToArray());

            byte[] bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            //byte[] keyBytes = Convert.FromBase64String(privateKey);
            String RSAParameters = "";
            string path = "keys//" + privateKey + ".xml";
            StreamReader sr = new StreamReader(path);
            RSAParameters = sr.ReadToEnd();
            sr.Close();
            RSA.FromXmlString(RSAParameters);
            byte[] key = RSA.ExportRSAPrivateKey();

            var privKeyObj = Asn1Object.FromByteArray(key);
            var privStruct = RsaPrivateKeyStructure.GetInstance((Asn1Sequence) privKeyObj);

            ISigner sig = SignerUtilities.GetSigner("SHA256withRSA");

            sig.Init(true, new RsaKeyParameters(true, privStruct.Modulus, privStruct.PrivateExponent));

            sig.BlockUpdate(bytesToSign, 0, bytesToSign.Length);
            byte[] signature = sig.GenerateSignature();

            segments.Add(Base64UrlEncode(signature));
            return string.Join(".", segments.ToArray());
        }

        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        public void Login(string username, string password)
        {
            DB.Open();
            
            string query = "Select * FROM users WHERE USER =" + "'" + username + "'";
            
            DataSet ds;
            ds = DB.DataSet(query);
            
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string dbPassword = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                string dbSalt = ds.Tables[0].Rows[0]["SALT"].ToString();

                byte[] plainText = Encoding.UTF8.GetBytes(password);
                byte[] salt = Convert.FromBase64String(dbSalt);

                HashAlgorithm algorithm = new SHA256Managed();

                byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                byte[] byteHashSaltPw = algorithm.ComputeHash(plainTextWithSaltBytes);

                string SaltedHashPassword = Convert.ToBase64String(byteHashSaltPw);

                if (dbPassword.Equals(SaltedHashPassword))
                {
                    Console.WriteLine("Token: " + Sign(username));
                }
                else
                    Console.WriteLine("Gabim: Fjalekalimi i gabuar.");
            }
            else
            {
                Console.WriteLine("Shfrytezuesi nuk ekziston!");
            }
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 1:
                    output += "===";
                    break; // Three pad chars
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
    }
}