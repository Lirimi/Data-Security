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
           
            /*------Rsa instance-----*/
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
            
            String RSAParameters = "";
            
            /*-------Check if sourcepath has a web client----*/
            bool HttpPath = PathIsHttp(SourcePath);
            
            /*----Read Rsa key from Web----*/
            if (HttpPath)
            {
                Task<Stream> xmlParameters = GetRequest(SourcePath);
                xmlParameters.Wait();
                var readXml = xmlParameters.Result;
                
                StreamReader sr = new StreamReader(readXml);
                RSAParameters = sr.ReadToEnd();
                sr.Close();
                
                objRSA.FromXmlString(RSAParameters);
            }
            /*---Read RSA Key from path---*/
            else
            {
                StreamReader sr = new StreamReader(SourcePath);
                RSAParameters = sr.ReadToEnd();
                sr.Close();
                objRSA.FromXmlString(RSAParameters);
            }

            
            /*-----------Check if key exists-----------------*/
            bool KeyExists = KeyExistsCheck(AddToPath);
            
            /*------------Check if key is Public------------*/
            //------bool publicKey = objRSA.PublicOnly;--------//
            /*----For unknown reason it always gives false------*/
            
            
            if (!KeyExists)
            {
                if (!RSAParameters.Contains("<P>"))
                {
                    /* ------- Importojme qelsin Publik -----*/
                    StreamWriter sw = new StreamWriter("keys//" + AddToPath + ".pub.xml");
                    sw.Write(RSAParameters);
                    sw.Close();
                    Console.WriteLine("Celsi publik u ruajt ne fajllin keys/" + AddToPath + ".pub.xml");
                }
                else
                {
                    /*-------- Importojme qelsin privat ------*/
                    RSAParameters exportPrivateParameters = objRSA.ExportParameters(true);
                    
                    RSACryptoServiceProvider objRSA2 = new RSACryptoServiceProvider(); 
                    objRSA2.ImportParameters(exportPrivateParameters);
            
                    /*------- Include Public ParemetersOnly ----*/
                    string PublicRSAParameters = objRSA2.ToXmlString(false);
          
                    StreamWriter swPublic = new StreamWriter("keys//" + AddToPath + ".pub.xml");
                    swPublic.Write(PublicRSAParameters);
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
            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(SourcePath);
            HttpContent content = response.Content;

            Stream mycontent = await content.ReadAsStreamAsync();
            return mycontent;
            
        }
    }
}