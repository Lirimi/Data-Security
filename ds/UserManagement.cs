using System;
using System.IO;
using System.Security.Cryptography;

namespace ds
{
    public class UserManagement : DatabaseManipulation, UserInterface
    {
        public int Keysize { get; set; } 
        public string User { get; set; }
        
        
        
        protected static readonly String keyPath = "keys//";
        protected static readonly String publicNotation = ".pub.xml";
        protected static readonly String privateNotation = ".xml";
        protected static String publicKeyPath = "";
        protected static String privateKeyPath = "";
        
        public UserManagement(int Keysize)
        {
            this.Keysize = Keysize;
        }

        public bool CreatePath()
        {
            publicKeyPath = keyPath + this.User + publicNotation;
            privateKeyPath = keyPath + this.User + privateNotation;

            bool privateKeyExist = File.Exists(privateKeyPath);
            bool publicKeyExist = File.Exists(publicKeyPath);
            if (!(privateKeyExist || publicKeyExist))
                return true;
            return false;
        }


        public void CreateUser()
        {

            bool freepath = CreatePath();
            if(!freepath)
                throw new Exception("File with that name is found!");
            //nje stream qe i ruan qelsat
            FileStream fs = null;
            StreamWriter sw = null;
            
            //krijojm nje RSA provider
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(this.Keysize);
            try
            {
                //save private key
                fs = new FileStream(privateKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                sw?.Close();
                fs?.Close();
            }

            try
            {
                //save public key
                fs = new FileStream(publicKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(false));
                sw.Flush();
            }
            finally
            {
                sw?.Close();
                fs?.Close();
            }

            rsa.Clear();
            try
            {
                _insertIntoDB(this.User);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }


        public void DeleteUser()
        {
            bool fileNotExist = CreatePath();
            if (fileNotExist)
                throw new FileNotFoundException("User does not exist!");
            File.Delete(privateKeyPath);
            File.Delete(publicKeyPath);
            try
            {
                _deletefromDB(this.User);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }

        }


        public static string  _generatePassword()
        {
            Console.Write("Jepni fjalekalimin: ");
            string password = null;
            password = HidePassword(password);

            Console.Write("\nPerserit fjalekalimin: ");
            string repeatpassword = null;
            repeatpassword = HidePassword(repeatpassword);

            if (!String.Equals(password, repeatpassword))
                throw new Exception("Gabim: Fjalekalimet nuk perputhen.");

            Console.WriteLine();
            return password;
        }
        
        
        private static string HidePassword(String pass)
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                pass += key.KeyChar;
            }

            return pass;
        }
        
    }
}