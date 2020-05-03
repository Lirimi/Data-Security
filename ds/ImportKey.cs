using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ds
{
    public class ImportKey
    {
        public void Import(string AddToPath, string SourcePath) 
        {
           
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
            
            string RSAParemeters = "";
            
            /*-------Check if sourcepath has a web client----*/
            bool HttpPath = PathIsHttp(SourcePath);
            
            /*----Read Rsa key from Web----*/
            if (HttpPath)
            {
                GetRequest(SourcePath).Wait();
             
                StreamReader sr = new StreamReader(SourcePath);
                RSAParemeters = sr.ReadToEnd();
                sr.Close();
                objRSA.FromXmlString(RSAParemeters);
            }
            /*---Read RSA Key from path---*/
            else
            {
                StreamReader sr = new StreamReader(SourcePath);
                RSAParemeters = sr.ReadToEnd();
                sr.Close();
                objRSA.FromXmlString(RSAParemeters);
            }

            /*-----------Check if key exists-----------------*/
            bool KeyExists = KeyExistsCheck(AddToPath);
            
            /*------------Check if key is private------------*/
            bool PublicKey = objRSA.PublicOnly;

            
            
            if (!KeyExists)
            {
                if (PublicKey)
                {
                    /* ------- Importojme qelsin Publik -----*/
                    StreamWriter sw = new StreamWriter("keys//" + AddToPath + ".pub.xml");
                    sw.Write(RSAParemeters);
                    sw.Close();
                }
                else
                {
                    /*-------- Importojme qelsin privat ------*/
                    RSAParameters exportPrivateParameters = objRSA.ExportParameters(true);
                    
                    RSACryptoServiceProvider objRSA2 = new RSACryptoServiceProvider(); 
                    objRSA2.ImportParameters(exportPrivateParameters);
            
                    /*------- Include Public ParemetersOnly ----*/
                    string PublicRsaParemeters = objRSA2.ToXmlString(false);
          
                    StreamWriter swPublic = new StreamWriter("keys//" + AddToPath + ".pub.xml");
                    swPublic.Write(PublicRsaParemeters);
                    swPublic.Close();
          
                    /*------- Include All Paremeters ------*/
                    string PrivateRsaParemters = objRSA2.ToXmlString(true);
        
                    StreamWriter swPrivate = new StreamWriter("keys//" + AddToPath + ".xml");
                    swPrivate.Write(PrivateRsaParemters);
                    swPrivate.Close();
                    
                    Console.WriteLine("Celsi privat u ruajt ne fajllin keys/" + AddToPath + ".xml");
                    Console.WriteLine("Celsi publik u ruajt ne fajllin keys/" + AddToPath + ".pub.xml");
                }
            }
            else
            {
                Console.WriteLine("Celsi " + AddToPath + " ekziston paraprakisht!");
            }


        }
        
        private bool KeyExistsCheck(string AddToPath)
        {
            string filenamePublic = "keys//" + AddToPath + ".pub.xml";
            string filenamePrivate = "keys//" + AddToPath + ".xml";
            if (File.Exists(filenamePublic) || File.Exists(filenamePrivate))
            {
                return true;
            }

            return false;
        }

        private bool PathIsHttp(string SourcePath)
        {
            bool getRequest = Regex.IsMatch(SourcePath,"^(http|https)://.*$");
            if (getRequest)
            {
                return true;
            }
            return false;
        }

        private async Task<Stream> GetRequest(string SourcePath)
        {
           
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(SourcePath))
                {
                    using (HttpContent content = response.Content)
                    {
                        Stream mycontent = await content.ReadAsStreamAsync();
                        return mycontent;
                    }
                }
            }
        }
    }
}