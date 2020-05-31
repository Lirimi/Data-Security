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

        public void GeneratePassword()
        {
            Console.Write("Jepni fjalekalimin: ");
            string password = Console.ReadLine();

            ValidatePassword(password);
            
            Console.Write("Perserit fjalekalimin: ");
            string repeatpassword = Console.ReadLine();
            
            if (!String.Equals(password, repeatpassword)) 
                    throw new Exception("Gabim: Fjalekalimet nuk perputhen.");

        }
        
        
        private void ValidatePassword( string password )
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
                Console.WriteLine("Gabim: Fjalëkalimi yt duhet të jetë të paktën 6 karaktere i gjatë. Provo përsëri.");
                GeneratePassword();
            }
            else if (!hasLetter)
            {
                Console.WriteLine("Gabim: Fjalekalimi duhet te permbaje se paku nje karakter. Provo përsëri.");
                GeneratePassword();
            }
            else if (!hasDecimalDigitorSymbol)
            {
                Console.WriteLine("Gabim: Fjalekalimi duhet te permbaje se paku nje numer ose simbol. Provo përsëri.");
                GeneratePassword();
            }

        }
    }

}
