using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace ds
{
    public class Deleteuser
    {
        public void DeleteRsaKey(string privateKeyPath, string publicKeyPath, int size)
        {
            File.Delete(privateKeyPath);
            File.Delete(publicKeyPath);
        }
    }
}