using System;
using System.IO;
using System.Net;

namespace ds
{
    public abstract class PathManagement
    {
        
        protected static readonly String keyPath = "keys//";
        protected static readonly String publicNotation = ".pub.xml";
        protected static readonly String privateNotation = ".xml";
        protected static String publicKeyPath = "";
        protected static String privateKeyPath = "";
        
        public static bool CreatePath(String User)
        {
            publicKeyPath = keyPath + User + publicNotation;
            privateKeyPath = keyPath + User + privateNotation;

            bool privateKeyExist = File.Exists(privateKeyPath);
            bool publicKeyExist = File.Exists(publicKeyPath);
            if (!(privateKeyExist || publicKeyExist))
                return true;
            return false;
        }

        protected abstract void ReadRSA(string path);

        protected abstract void WriteRSA(string path);


    }
}