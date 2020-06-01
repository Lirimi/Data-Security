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

            var header = new { alg = "RS256", typ = "JWT" };
 
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
            var privStruct = RsaPrivateKeyStructure.GetInstance((Asn1Sequence)privKeyObj);
 
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
            
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
               // String query = "Select * from users where USER=" + "'" +  @username + "'" +  " AND PASSWORD ='" +  @password + "'";
                //bool dbopen = DB.Open();
                MySqlDataReader row;
                
                //row = DB.ExecuteReader(query);
                MySqlCommand cmd = new MySqlCommand();
                string MyConnection2 = "datasource=localhost;database=DSusers;username=root;password=password;CharSet=utf8";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);  
                MyConn2.Open();
                cmd = new MySqlCommand("SELECT USER FROM users WHERE USER=@val1", MyConn2);
                cmd.Parameters.AddWithValue("@val1", username);
                //cmd.Parameters.AddWithValue("@val2", password);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                int i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    throw new Exception("Fjalekalimi eshte i gabuar!");
                }
                
                string token = Sign(username);
                Console.Write("Token: " + token);
                MyConn2.Close();


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
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
               case 1: output += "==="; break; // Three pad chars
               case 2: output += "=="; break; // Two pad chars
               case 3: output += "="; break; // One pad char
               default: throw new System.Exception("Illegal base64url string!");
           }
           var converted = Convert.FromBase64String(output); // Standard base64 decoder
           return converted;
       }
    }
}