using System;
using System.Xml;


namespace ds
{
  
    public class ExportKey
    {
        
        public void PublicKeyToPath(string userKey, string exportToPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            String exportFrom = "keys//" + userKey + ".pub.xml";
            xmlDoc.Load(exportFrom);
            xmlDoc.Save(exportToPath);
        }

        public void PrivateKeyToPath(string userKey, string exportToPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            String exportFrom = "keys//" + userKey + ".xml";
            xmlDoc.Load(exportFrom);
            xmlDoc.Save(exportToPath);
            
        }
        
        public void PublicKeyToConsole(string userKey)
        {
            XmlDocument xmlDoc = new XmlDocument();
            String exportFrom = "keys//" + userKey + ".pub.xml";
            xmlDoc.Load(exportFrom);
            xmlDoc.Save(Console.Out);
        }
        
        public void PrivateKeyToConsole(string userKey)
        {
            XmlDocument xmlDoc = new XmlDocument();
            String exportFrom = "keys//" + userKey + ".xml";
            xmlDoc.Load(exportFrom);
            xmlDoc.Save(Console.Out);
        }
    }
}