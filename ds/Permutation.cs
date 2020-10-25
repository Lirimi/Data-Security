using System;
using System.Diagnostics;
using System.Text;


namespace ds
{
    public class Permutation : Encryption_Standard
    {

        private string key = "";
        private bool Valid = true;

        public void setKey(string key)
        {
            this.key = key;
            /* Unaza qe nuk lejon numra te perseritur */
            for (int i = 0; i < this.key.Length - 1; i++)
            for (int j = i + 1; j < this.key.Length && Valid; j++)
                Valid = this.key[i] != this.key[j];
            /* -------------------------------------------------------- */
        }

        public string getKey() { return this.key; }
        
    
        public string Encrypt(string message)
        {
            if(!Valid)
                throw new Exception("Invalid key! Make sure the numbers are not repeated!");


            message = PadMessage(message);
            char[] cipherChars = new char[message.Length];
            
            for (int i = 0; i < message.Length; i++)
                cipherChars[i] = message[key[i % 4] - '1' + i / 4 * 4];
            
            return new string(cipherChars);
        }

        public string Decrypt(string cipher)
        {
            char[] plainChars = new char[cipher.Length];
            for (int i = 0; i < cipher.Length; i++)
                plainChars[key[i % 4] - '1' + i / 4 * 4] = cipher[i];

            string Plaintext = new string(plainChars);
            /* ------------------- */
            return RemovePaddingChars(Plaintext).ToString();
        
        }

        private string PadMessage(String message)
        {
            if (message.Length % 4 != 0)
                message = message.PadRight(message.Length + 4 - message.Length % 4, 'w');
            return message;

        }

        public string EncryptionScheme(String message, String Cipher)
        {
            message = PadMessage(message);
            StringBuilder sb = new StringBuilder();
            sb.Append("Plaintext:  ");
            for (int i = 0; i < message.Length; i += 4)
                sb.Append(message.Substring(i, 4) + ' ');
            while (key.Length % message.Length != 0)
                key += key;

            sb.Append("\nKey:\t    ");
            for (int i = 0; i < key.Length; i += 4)
                sb.Append(key.Substring(i, 4) + ' ');
            
            sb.Append("\nCipherText: ");
            for (int i = 0; i < Cipher.Length; i += 4)
                sb.Append(Cipher.Substring(i, 4) + ' ');

            sb.Append("\n");
            return sb.ToString();
        }
        
        
        private StringBuilder RemovePaddingChars(String text)
        {
            StringBuilder _stringBuilder  = new StringBuilder(text);
            _stringBuilder.Replace("w", "");
            return _stringBuilder;
        }

    }
}