using System;
using System.Security.Cryptography;
using System.Text;

namespace ds
{
    public class GenerateHashedPassword
    {
        private string password = "";
       
        public void SetPassword(String password)
        {
            password = ValidatePassword(password);
            byte[] Saltedpassword = GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Salt());
            password = Convert.ToBase64String(Saltedpassword);
            this.password = password;
        }

        public string GetPassword()
        {
            return this.password;
        }

        private string ValidatePassword(string password)
        {
            const int MIN_LENGTH = 6;
            const int MAX_LENGTH = 15;

            if (password == null) throw new ArgumentNullException("Gabim: Passwordi nuk eshte shtypur!");

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasLetter = false;
            bool hasDecimalDigitorSymbol = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsLetter(c)) hasLetter = true;
                    else if (char.IsDigit(c) || char.IsSymbol(c)) hasDecimalDigitorSymbol = true;
                }
            }

            if (meetsLengthRequirements && hasLetter && hasDecimalDigitorSymbol)
                return password;
            if (!meetsLengthRequirements)
                throw new Exception("Gabim: Fjalëkalimi yt duhet të jetë të paktën 6 karaktere i gjatë.");
            if (!hasLetter)
                throw new Exception("Gabim: Fjalekalimi duhet te permbaje se paku nje karakter.");
            if (!hasDecimalDigitorSymbol)
                throw new Exception("Gabim: Fjalekalimi duhet te permbaje se paku nje numer ose simbol.");
            return password;
        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
                plainTextWithSaltBytes[i] = plainText[i];

            for (int i = 0; i < salt.Length; i++)
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private static byte[] CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }

        protected static byte[] Salt()
        {
            return CreateSalt(10);
        }
        
        
    }
}