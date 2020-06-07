using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;


namespace ds
{
    public class ExportKey
    {
        
        String RSAParameters = "";
        public void PublicKey(string userKey, [Optional, DefaultParameterValue(0)] object ToPath)
        {
            string path = "keys//" + userKey + ".pub.xml";
            Exportkey(path);

            if (ToPath.Equals(0)) Console.WriteLine(RSAParameters);
            else
            {
                StreamWriter sw = new StreamWriter(ToPath.ToString());
                sw.Write(RSAParameters);
                sw.Close();
            }
        }

        public void PrivateKey(string userKey, [Optional, DefaultParameterValue(0)] object ToPath)
        {
            string path = "keys//" + userKey + ".xml";
            Exportkey(path);

            if (ToPath.Equals(0)) Console.WriteLine(RSAParameters);
            else
            {
                StreamWriter sw = new StreamWriter(ToPath.ToString());
                sw.Write(RSAParameters);
                sw.Close();
            }
        }

        private void Exportkey(string path)
        {
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();

            StreamReader sr = new StreamReader(path);
            RSAParameters = sr.ReadToEnd();
            sr.Close();

            objRSA.FromXmlString(RSAParameters);
        }
    }
}