using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;


namespace ds
{
    public class ExportKey : PathManagement
    {
        protected String RSAParameters = "";
        public String userKey { get; set; }

        protected RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        
        public void Export(bool isPublic, [Optional, DefaultParameterValue(0)] object ToPath)
        {
            string path = keyPath + userKey + (isPublic ? publicNotation : privateNotation);
            ReadRSA(path);

            if (ToPath.Equals(0)) Console.WriteLine(RSAParameters);
            else WriteRSA(ToPath.ToString());
        }

        protected override void ReadRSA(string path)
        {
            StreamReader sr = new StreamReader(path);
            RSAParameters = sr.ReadToEnd();
            sr.Close();
            objRSA.FromXmlString(RSAParameters);
        }

        protected override void WriteRSA(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(RSAParameters);
            sw.Close();
        }
    }
}