using System;
using System.IO;
using System.Xml;
using System.Security.Cryptography;


namespace ds
{
    public class ImportKey
    {
        public void Import(string AddToPath, string SourcePath) 
        {
            /*---Load Xml Document ---*/
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(SourcePath); 
            xmlDoc.Normalize();
          
            /*------Check if key exists-------------*/
            bool KeyExists = KeyExistsCheck(AddToPath);
            
            /*-------Check if key is private-------*/
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
    }
}