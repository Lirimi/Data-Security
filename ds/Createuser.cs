using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ds
{
    public class Createuser
    {
        public void GenerateRsaKey(string privateKeyPath, string publicKeyPath, int size)
        {
            //nje stream qe i ruan qelsat
            FileStream fs = null;
            StreamWriter sw = null;

            //krijojm nje RSA provider
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(size);
            try
            {
                //ruaj private key
                fs = new FileStream(privateKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }

            try
            {
                //ruaj public key
                fs = new FileStream(publicKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(false));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            rsa.Clear();
        }
    }

}
