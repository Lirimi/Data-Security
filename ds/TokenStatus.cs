using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;


namespace ds
{
    public class TokenStatus
    {
        DatabaseConnection DB = new DatabaseConnection();
        static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        private static string UserofJWT = "";
        private static DateTime ExpiringDateofJWT;
        private static string Validition = ""; 
        public bool Status(String jwtInput)
        {
            var jwtHandler = new JwtSecurityTokenHandler();


            //Check if it's readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (!readableToken) throw new Exception("Gabim: Vlera e dhene nuk eshte token valid.");
            var token = jwtHandler.ReadJwtToken(jwtInput);

            //Extract the payload of the JWT
            var claims = token.Claims;

            var expireClaim = claims.FirstOrDefault(x =>
                x.Type.ToString().Equals("expire", StringComparison.InvariantCultureIgnoreCase));
            var nameClaim = claims.FirstOrDefault(x =>
                x.Type.ToString().Equals("Name", StringComparison.InvariantCultureIgnoreCase));

            var username = nameClaim.Value;
            var expireDate = expireClaim.Value;

            bool VerifySignature = Decode(jwtInput, username);

            string valid = "";
            bool isValid = false;

            DB.Open();

            string query = "Select * FROM users WHERE USER =" + "'" + username + "'";

            DataSet ds;
            ds = DB.DataSet(query);

            
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Parse(expireDate);

            if (issued < expire && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && VerifySignature)
                valid = "po";
            else 
                valid = "jo";

            if (valid.Equals("po"))
                isValid = true;
            
            UserofJWT = username;
            ExpiringDateofJWT = expire;
            Validition = valid;

            DB.Close();

            return isValid;
        }

        public void PasstheValue()
        {
            Console.WriteLine("User: " + UserofJWT);
            Console.WriteLine("Valid: " + Validition);
            Console.WriteLine("Skadimi: " + ExpiringDateofJWT.ToString("dd/MM/yyyy HH:mm"));
        }

        public bool Decode(string token, string publickey, bool verify = true)
        {
            string[] parts = token.Split('.');
            string header = parts[0];
            string payload = parts[1];
            byte[] crypto = Base64UrlDecode(parts[2]);
            string headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
            JObject headerData = JObject.Parse(headerJson);
            string payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));

            JObject payloadData = JObject.Parse(payloadJson);
            if (verify)
            {
                String RSAParameters = "";
                string path = "keys//" + publickey + ".pub.xml";
                StreamReader sr = new StreamReader(path);
                RSAParameters = sr.ReadToEnd();
                sr.Close();
                RSA.FromXmlString(RSAParameters);
                byte[] key = RSA.ExportSubjectPublicKeyInfo();

                AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(key);
                RsaKeyParameters rsaKeyParameters = (RsaKeyParameters) asymmetricKeyParameter;
                RSAParameters rsaParameters = new RSAParameters();
                rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
                rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(rsaParameters);

                SHA256 sha256 = SHA256.Create();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(parts[0] + '.' + parts[1]));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                if (!rsaDeformatter.VerifySignature(hash, FromBase64Url(parts[2])))
                    return false;
            }

            return true;
        }

        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url
                : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                .Replace("-", "+");
            return Convert.FromBase64String(base64);
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