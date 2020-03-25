using System;
using System.Text;
namespace ds
{
    public class Permutation
    {
        public Permutation()
         {
            try
            {
                /* Enkriptimi */
                Console.Write("\nType message: ");
                string message = Console.ReadLine();
                Console.Write("Type key: ");
                string key = Console.ReadLine();
                string cipher = encrypt(message, key);
                Console.WriteLine("Cipher: " + cipher + '\n');
                /* ---------- */

                /* Dekriptimi */
                Console.Write("Type cipher: ");
                cipher = Console.ReadLine();
                Console.Write("Type key: ");
                key = Console.ReadLine();
                string plain = decrypt(cipher, key);
                Console.WriteLine("Plain: " + plain);
                /* ---------- */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /* Funksioni per enkriptim */
        static string encrypt(string message, string key)
        {
            /* Qikjo osht per mos me mujt perdorusi me jep qels jovalid */
            if (key.Length > 4)
                throw new Exception("Invalid key!");
            for (int i = 0; i < 4; i++)
                if (key[i] < '1' || key[i] > '4')
                    throw new Exception("Invalid key!");
            for (int i = 0; i < 3; i++)
                for (int j = i + 1; j < 4; j++)
                    if (key[i] == key[j])
                        throw new Exception("Invalid key!");
            /* -------------------------------------------------------- */

            /* Qikjo osht pjesa e enkriptimit */
            if (message.Length % 4 != 0)
                message = message.PadRight(message.Length + 4 - message.Length % 4, 'x');
            char[] cipherChars = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
                cipherChars[i] = message[key[i % 4] - '1' + i / 4 * 4];
            return new string(cipherChars);
            /* ------------------------------ */
        }

        /* Funksioni per dekriptim */
        static string decrypt(string cipher, string key)
        {
            /* Qikjo osht per mos me mujt perdorusi me jep qels jovalid */
            if (key.Length > 4)
                throw new Exception("Invalid key!");
            for (int i = 0; i < 4; i++)
                if (key[i] < '1' || key[i] > '4')
                    throw new Exception("Invalid key!");
            for (int i = 0; i < 3; i++)
                for (int j = i + 1; j < 4; j++)
                    if (key[i] == key[j])
                        throw new Exception("Invalid Key!");
            /* -------------------------------------------------------- */

            /* Qikjo osht pjesa e dekriptimit */
            char[] plainChars = new char[cipher.Length];
            for (int i = 0; i < cipher.Length; i++)
                plainChars[key[i % 4] - '1' + i / 4 * 4] = cipher[i];
            return new string(plainChars);
            /* ------------------------------ */
        }
    }
}
