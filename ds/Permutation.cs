using System;
using System.Text;

namespace ds
{
    public class Permutation : Beale
    { 
        
        public void Encrypt(string key, string message)
        {
            Key(key);

            /* Pjesa e enkriptimit */
            if (message.Length % 4 != 0)
                message = message.PadRight(message.Length + 4 - message.Length % 4, 'w');

            char[] cipherChars = new char[message.Length];
            string Cipher = "";
            for (int i = 0; i < message.Length; i++)
            {
                cipherChars[i] = message[key[i % 4] - '1' + i / 4 * 4];
                Cipher += cipherChars[i];
            }

            /* ---------------------- */
            Console.WriteLine("Plaintexti i enkriptuar: " + Cipher);
            /* Pika Shtese! */
            Console.Write("Plaintext:  ");
            for (int i = 0; i < message.Length; i += 4)
                Console.Write(message.Substring(i, 4) + ' ');
            while (key.Length % message.Length != 0)
                key += key;

            Console.Write("\nKey:\t    ");
            for (int i = 0; i < key.Length; i += 4)
                Console.Write(key.Substring(i, 4) + ' ');
            Console.Write("\nCipherText: ");

            for (int i = 0; i < Cipher.Length; i += 4)
                Console.Write(Cipher.Substring(i, 4) + ' ');

            Console.WriteLine();
            /* ------------ */
        }

        /* Funksioni per dekriptim */
        public void Decrypt(string key, string cipher)
        {
            Key(key);

            /* Pjesa e dekriptimit */
            char[] plainChars = new char[cipher.Length];
            for (int i = 0; i < cipher.Length; i++)
                plainChars[key[i % 4] - '1' + i / 4 * 4] = cipher[i];

            string Plain = new string(plainChars);
            /* ------------------- */

            Console.Write("Ciphertexti i dekriptuar: ");

            /* Pjesa qe mundeson heqjen e karekterit special */
            StringBuilder plain = new StringBuilder(Plain);
            plain.Replace("w", "");
            Console.Write(plain);
            Console.WriteLine();
            /* --------------------------------------------- */
        }
        
        private void Key(string key)
        {
            /* Unaza qe nuk lejon numra te perseritur */
            for (int i = 0; i < key.Length - 1; i++)
            for (int j = i + 1; j < key.Length; j++)
                if (key[i] == key[j])
                    throw new Exception("Invalid key! Make sure the numbers are not repeated!");
            /* -------------------------------------------------------- */
        }
        
    }
}