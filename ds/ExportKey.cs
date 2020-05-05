using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;


namespace ds
{
  
    public class ExportKey
    {
        public void PublicKey(string userKey, [Optional, DefaultParameterValue(0)] object ToPath)
        {

            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();

            String RSAParameters = "";
            string path = "keys//" + userKey + ".pub.xml";
            StreamReader sr = new StreamReader(path);
            RSAParameters = sr.ReadToEnd();
            sr.Close();

            objRSA.FromXmlString(RSAParameters);


            if (ToPath.Equals(0))
            {
                Console.WriteLine(RSAParameters);
            }
            else
            {
                StreamWriter sw = new StreamWriter(ToPath.ToString());
                sw.Write(RSAParameters);
                sw.Close();
            }
        }

        public void PrivateKey(string userKey, [Optional, DefaultParameterValue(0)] object ToPath)
        {
            RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();

            String RSAParameters = "";
            string path = "keys//" + userKey + ".xml";
            StreamReader sr = new StreamReader(path);
            RSAParameters = sr.ReadToEnd();
            sr.Close();

            objRSA.FromXmlString(RSAParameters);


            if (ToPath.Equals(0))
            {
                Console.WriteLine(RSAParameters);
            }
            else
            {
                StreamWriter sw = new StreamWriter(ToPath.ToString());
                sw.Write(RSAParameters);
                sw.Close();
            }
        }
    }
}