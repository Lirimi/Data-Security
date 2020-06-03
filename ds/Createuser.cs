using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace ds
{
    
    public class Createuser 
    {
        DatabaseConnection DB = new DatabaseConnection();
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

       

        private byte[] GeneratePassword(byte[] salt)
        {
            Console.Write("Jepni fjalekalimin: ");
            string password = Console.ReadLine();

            ValidatePassword(password);
            
            Console.Write("Perserit fjalekalimin: ");
            string repeatpassword = Console.ReadLine();

            if (!String.Equals(password, repeatpassword)) 
                    throw new Exception("Gabim: Fjalekalimet nuk perputhen.");

           
            byte[] Saltedpassword = GenerateSaltedHash(Encoding.UTF8.GetBytes(password), salt);
            
            return Saltedpassword;
        }

        private void ValidatePassword(string password)
        {
            const int MIN_LENGTH =  6 ;
            const int MAX_LENGTH = 15 ;

            if ( password == null ) throw new ArgumentNullException("Gabim: Passwordi nuk eshte shtypur!") ;

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH ;
            bool hasLetter               = false ;
            bool hasDecimalDigitorSymbol = false ;

            if ( meetsLengthRequirements )
            {
                foreach (char c in password )
                {
                    if      ( char.IsLetter(c) )                   hasLetter = true ;
                    else if ( char.IsDigit(c) || char.IsSymbol(c)) hasDecimalDigitorSymbol   = true ;
                }
            }

            if(meetsLengthRequirements && hasLetter && hasDecimalDigitorSymbol)
                return;
            if (!meetsLengthRequirements)
            {
                throw new Exception("Gabim: Fjalëkalimi yt duhet të jetë të paktën 6 karaktere i gjatë.");
                
            }
            if (!hasLetter)
            {
                throw new Exception("Gabim: Fjalekalimi duhet te permbaje se paku nje karakter.");
                
            }
            if (!hasDecimalDigitorSymbol)
            {
                throw new Exception("Gabim: Fjalekalimi duhet te permbaje se paku nje numer ose simbol.");
                
            }

        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);            
        }

        private static byte[] CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }

        private static byte[] Salt()
        {
            return CreateSalt(10);
        }

        public void InsertIntoDB(string user)
        {
            byte[] salt = Salt();
            byte[] pass = GeneratePassword(salt);
            String saltBytes = Convert.ToBase64String(salt);
            String password = Convert.ToBase64String(pass);
            
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                String query = "INSERT INTO users VALUES" + "('" + user + "','" + password + "','" + saltBytes + "')";
                
                DB.Open();
                MySqlDataReader row;
                row = DB.ExecuteReader(query);
                Console.WriteLine("@Eshte krijuar shfrytezuesi " + user);
                DB.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
