using System;
using System.Text.RegularExpressions;
using System.Text;

namespace ds
{
    public class Program
    {
        static void Main(string[] args)
        {

            Beale B = new Beale();
            Permutation P = new Permutation();
            Numerical N = new Numerical();
            if (args.Length == 0) 
            {
                Console.WriteLine("\n@Argumentet mungojne!");
                Console.WriteLine("\n@Per ekzekutimin e kodit Permutation shtyp | Permutation Encrypt <text><text> | ose | Permutation Decrypt <text><text> |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Numerical shtyp | Numerical Encode <text> | ose | Numerical Decode <text> |");
                Environment.Exit(1);

            }
            if (args.Length <= 2 || args.Length > 4) 
            {
                Console.WriteLine("\n@Numri i argumenteve jo i lejuar!");
                Console.WriteLine("\n@Per ekzekutimin e kodit Permutation shtyp | Permutation Encrypt <text><text> | ose | Permutation Decrypt <text><text> |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Numerical shtyp | Numerical Encode <text> | ose | Numerical Decode <text> |");
                Environment.Exit(1);
            }
            if (args[0].Equals("Numerical"))
            {
                if (args[1].Equals("Encode"))
                {

                    String text = args[2];
                    if (Regex.IsMatch(text, "^[a-z]+"))
                    {
                        string Cipher = N.Encode(text);
                        Console.WriteLine("Encode is:" + Cipher);
                    }
                    else
                    {
                        Console.Write("Argumenti i fundit lejohet te permbaje vetem shkronja te vogla sipas alfabetit anglez prej a-z!");
                    }
                }
                else if (args[1].Equals("Decode"))
                {
                    String text = args[2];
                    if(Regex.IsMatch(text, "^[0-9]+"))
                    {
                        string Plain = N.Decode(text);
                        Console.WriteLine("Decode is:" + Plain);
                    }
                    else 
                    {
                        Console.Write("Argumenti i fundit lejohet te permbaje vetem numra 0-9!");
                    }

                }
                else
                {
                    Console.Write("Argumenti eshte jo valid! (Args must be Encode or Decode)");
                }
            }
       }

        public class Beale 
        {
            public string BealeEncrypt(string plainteksti)
            {
                //Kodi per tekstin qe ndodhet ne liber
                string teksti = "grupi 28i";
                char[] test = teksti.ToCharArray();

                //Kodi per plainteksitin qe deshirojme me mshef.
                string plaintekst = plainteksti;
                char[] ch = plaintekst.ToCharArray();

                //for loop 
                for (int i = 0; i < plaintekst.Length; i++)
                {
                    for (int j = 0; j < teksti.Length; j++)
                    {
                        if (test[j] == ch[i])
                        {

                            Console.Write(j + " ");
                            j = test.Length - 1;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return plainteksti.ToString();
            }

            // Funksioni BealeDecrypt

            public string BealeDecrypt(string[] ciphertekst)
            {
                string teksti = "grupi 28";

                for (int i = 0; i < ciphertekst.Length; i++)
                {
                    for (int j = 0; j < teksti.Length; j++)

                        if (Convert.ToInt32(ciphertekst[i]) == j)
                        {

                            Console.Write(teksti[j] + "");
                        }

                }
                return ciphertekst.ToString();
            }
        }



        public class Permutation 
        {
            public string Encrypt(string message, string key)
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
            public string Decrypt(string cipher, string key)
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

        public class Numerical
        {


            public string Encode(string plain)
            {
                string cipher = "";
                foreach (char c in plain)
                    if (c >= 'a' && c <= 'z')
                    {
                        cipher += (c - 'a' + 1).ToString();
                        cipher += ' ';
                    }
                return cipher.Trim();
            }

            public string Decode(string cipher)
            {
                string[] chars;
                chars = cipher.Split(" ");
                string plain = "";
                foreach (string s in chars)
                    plain += (char)(Int16.Parse(s) + 'a' - 1);
                return plain;
            }
        }


    }

}


    