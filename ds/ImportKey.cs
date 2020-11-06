using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ds
{
    public class ImportKey : ExportKey
    {
        private String publicRSAParameters = "";
        private String privateRSAParameters = "";
        private Boolean publicKey = true;

        public void Import(string AddToPath, string SourcePath, out bool PublicKey)
        {
            /*-------Check if sourcepath has a web client----*/
            bool httpPath = PathIsHttp(SourcePath);

            /*----Read Rsa key from Web----*/
            if (httpPath)
            {
                Task<Stream> xmlParameters = GetRequest(SourcePath);
                xmlParameters.Wait();
                var readXml = xmlParameters.Result;

                /*StreamReader sr = new StreamReader(readXml);
                RSAParameters = sr.ReadToEnd();
                sr.Close();

                objRSA.FromXmlString(RSAParameters);*/
                ReadRSA(readXml.ToString());
            }
            /*---Read RSA Key from path---*/
            else
            {
                ReadRSA(SourcePath);
            }

            bool privateKey = RSAParameters.Contains("<P>");
            if (!privateKey) // if key is public only
            {
                /* ------- Importojme qelsin Publik -----*/
                WriteRSA(keyPath + AddToPath + publicNotation);
                PublicKey = true;
              
            }
            else
            {
                /*-------- Importojme qelsin privat ------*/
                

                /*------- Include Public ParemetersOnly ----*/
                publicRSAParameters = objRSA.ToXmlString(false);

                WriteRSA(keyPath + AddToPath + publicNotation);
                
                publicKey = false;
                /*------- Include All / Private Paremeters ------*/
                privateRSAParameters = objRSA.ToXmlString(true);
                WriteRSA(keyPath + AddToPath + privateNotation);

                PublicKey = false;

            }
        }


        protected override void WriteRSA(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(publicKey ? publicRSAParameters : privateRSAParameters);
            sw.Close();
        }


        private bool PathIsHttp(string SourcePath)
        {
            bool getRequest = Regex.IsMatch(SourcePath, "^(http|https)://.*$");
            if (getRequest)
            {
                return true;
            }

            return false;
        }

        private async Task<Stream> GetRequest(string SourcePath)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(SourcePath);
            HttpContent content = response.Content;

            Stream mycontent = await content.ReadAsStreamAsync();
            return mycontent;
        }
    }
}