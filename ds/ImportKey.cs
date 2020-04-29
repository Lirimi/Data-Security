using System;
using System.IO;
using System.Net.Http;
using System.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ds
{
    public class ImportKey
    {
        public void Import(string AddToPath, string SourcePath) 
        {
           
          
            XmlDocument xmlDoc = new XmlDocument();

            /*-------Check if sourcepath has a web client----*/
            bool HttpPath = PathIsHttp(SourcePath);
            /*---Load Http Document ----*/
            if (HttpPath)
            {
                Task<Stream> HttpDoc = GetRequest(SourcePath);
                HttpDoc.Wait();
                var GetString = HttpDoc.Result;
                xmlDoc.Load(SourcePath);
                xmlDoc.Normalize();
                
                
            }
            /*---Load Xml Document from path---*/
            else
            {
                xmlDoc.Load(SourcePath); 
                xmlDoc.Normalize();
            }

            /*-----------Check if key exists-----------------*/
            bool KeyExists = KeyExistsCheck(AddToPath);
            
            /*------------Check if key is private------------*/
            bool PrivateKey = PrivateKeyCheck(xmlDoc);

            
            
            if (!KeyExists)
            {
                if (!PrivateKey)
                {
                    /* ------- Ruajme qelsin Publik -----*/
                    string filename = "keys//" + AddToPath + ".pub.xml"; 
                    xmlDoc.Save(filename);
                    Console.WriteLine("Celsi publik u ruajt ne filen keys/" + AddToPath + ".pub.xml");
                }
                else
                {
                    /*--Ruajme qelsin privat--*/
                    string filename = "keys//" + AddToPath + ".xml";
                    xmlDoc.Save(filename);
                    
                    /*--Gjenerojme qelsin publik--*/
                    FileStream fs = null;
                    StreamWriter sw = null;
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
                    string publicKeyPath = "keys//" + AddToPath + ".pub.xml";
                    fs = new FileStream(publicKeyPath, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.Write(rsa.ToXmlString(false));
                    sw.Flush();
                    /*-----------------------------*/
                    
                    Console.WriteLine("Celsi privat u ruajt ne fajllin keys/" + AddToPath + ".xml");
                    Console.WriteLine("Celsi publik u ruajt ne fajllin keys/" + AddToPath + ".pub.xml");
                }
            }
            else
            {
                Console.WriteLine("Celsi " + AddToPath + " ekziston paraprakisht!");
            }


        }

        bool PrivateKeyCheck(XmlDocument xmlDoc)
        {
            /*---Create Node---*/
            XmlNodeList Node = xmlDoc.GetElementsByTagName("P"); // Node shikon se a ekziston nje tag <P>
            if (Node.Item(0) == null)
            {
                return false;
            }

            return true;
        }

        bool KeyExistsCheck(string AddToPath)
        {
            string filenamePublic = "keys//" + AddToPath + ".pub.xml";
            string filenamePrivate = "keys//" + AddToPath + ".xml";
            if (File.Exists(filenamePublic) && File.Exists(filenamePrivate))
            {
                return true;
            }

            return false;
        }

        bool PathIsHttp(string SourcePath)
        {
            bool getRequest = Regex.IsMatch(SourcePath,"^(http|https)://.*$");
            if (getRequest)
            {
                return true;
            }
            return false;
        }

        async Task<Stream> GetRequest(string SourcePath)
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