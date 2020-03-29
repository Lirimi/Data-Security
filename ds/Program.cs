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

            if (args.Length <= 2 || args.Length > 4)
            {
                Console.WriteLine("\n@Argumentet Mungojne / Numri i argumenteve jo i lejuar!");
                Console.WriteLine("\n@Per ekzekutimin e funksionit Beale shtyp | ds.exe Beale Encrypt <text> | ose | ds.exe Beale Decrypt <text> ku text te decrypt duhet te jete me thonjeza like: 0 1 2 |");
                Console.WriteLine("\n@Per ekzekutimin e funksionit Permutation shtyp | ds.exe Permutation Encrypt <key><text> | ose | ds.exe Permutation Decrypt <key><text> |");
                Console.WriteLine("\n@Per ekzekutimin e funksionit Numerical shtyp | ds.exe Numerical Encode <text> | ose | ds.exe Numerical Decode <text> |");
                Environment.Exit(1);
            }

            /*---------------Args per Numerical-----------------*/
            if (args[0].Equals("Numerical"))
            {
                if (args[1].Equals("Encode"))
                {

                    String text = args[2];
                    if (Regex.IsMatch(text, "^[a-z ]+$"))
                    {
                        string Cipher = N.Encode(text);
                        Console.WriteLine("Encoded text is: " + Cipher);
                    }
                    else
                    {
                        Console.Write("\n@Argumenti i fundit lejohet te permbaje vetem shkronja te vogla sipas alfabetit anglez prej a-z!");

                    }
                }
                else if (args[1].Equals("Decode"))
                {
                    String text = args[2];
                    if (Regex.IsMatch(text, "^[0-9]+"))
                    {
                        string Plain = N.Decode(text);
                        Console.WriteLine("Decoded cipher is: " + Plain);
                    }
                    else
                    {
                        Console.Write("\n@Argumenti i fundit lejohet te permbaje vetem numra 0-9!");
                    }

                }
                else
                {
                    Console.Write("\n@Argumenti eshte jo valid! (Args must be | Encode | or | Decode |)");
                    Environment.Exit(1);
                }
            }



            /*---------------Args per Permutation-----------------*/
            else if (args[0].Equals("Permutation"))
            {
                if (args[1].Equals("Encrypt"))
                {

                    String key = args[2];
                    String text = args[3];
                    if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                    {
                        Console.WriteLine();
                        P.Encrypt(key, text);
                     
                    }
                    else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                    {
                        Console.WriteLine("\n@Key is either too long or too short (Make sure its 4 charecters only!");
                    }
                    else
                    {

                        throw new Exception("\n@Keep in mind that the first argument allows only numbers from 1-4!");
                    }
                }
                else if (args[1].Equals("Decrypt"))
                {
                    String key = args[2];
                    String text = args[3];
                    if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                    {
                        Console.WriteLine();
                        P.Decrypt(key, text);

                    }
                    else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                    {
                        Console.WriteLine("\n@Key is either too long or too short (Make sure its 4 charecters only!");
                    }
                    else
                    {

                        throw new Exception("\n@Keep in mind that the first argument allows only numbers from 1-4!");
                    }
                }
                else
                {
                    Console.Write("\n@E R R O R  ! Make sure you passed the argument right | Encrypt | or | Decrypt |!");
                    Environment.Exit(1);
                }

            }

            /*---------------Args per Beale-----------------*/
            else if (args[0].Equals("Beale"))
            {
                if (args[1].Equals("Encrypt"))
                {
                    String plainteksti = args[2];
                    if (Regex.IsMatch(plainteksti, "^[a-zA-Z ]+$"))
                    {
                        Console.Write("Encrypted plaintext is: ");
                        B.BealeEncrypt(plainteksti);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\n@Argumenti i fundit duhet te permbaje tekst a-z ose A-Z ne varesi nga teksti");
                    }

                }
                else if (args[1].Equals("Decrypt"))
                {
                    String[] ciphertekst = args[2].Split(" ");
                    String Cipher = String.Concat(ciphertekst);
                    if (Regex.IsMatch(Cipher, "^[0-9]+$"))
                    {
                        Console.Write("Decrypted Ciphertext is: ");
                        B.BealeDecrypt(ciphertekst);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\n@Argumenti i fundit duhet te permbaje vetem kod");
                    }
                }
                else
                {
                    Console.Write("\n@Argumenti eshte jo valid! (Args must be Encrypt or Decrypt) !");
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.Write("\n@Argumenti duhet te jete | Beale | ose | Permutation | ose | Numerical |");
                Environment.Exit(1);
            }
        }

        public class Beale
        {
            public void BealeEncrypt(string plainteksti)
            {
                //Kodi per tekstin qe ndodhet ne liber
                // Nese deshironi ta beni qe te lexoj path
                // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
                // string teksti = System.IO.File.ReadAllText("C:\\Users\\Gentrit\\Desktop\\teksti.txt");
                // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
                
                string teksti = "fakulteti teknik";
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
              
            }

            // Funksioni BealeDecrypt

            public void BealeDecrypt(string[] ciphertekst)
            {
               //Kodi per tekstin qe ndodhet ne liber
               // Nese deshironi ta beni qe te lexoj path
               // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
               // string teksti = System.IO.File.ReadAllText("C:\\Users\\Gentrit\\Desktop\\teksti.txt");
               // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale 
                 
               string teksti = "fakulteti teknik";
                
                for (int i = 0; i < ciphertekst.Length; i++)
                {
                    for (int j = 0; j < teksti.Length; j++)

                        if (Convert.ToInt32(ciphertekst[i]) == j)
                        {

                            Console.Write(teksti[j] + "");
                        }
                }
            }
        }



        public class Permutation
        {
            private string Key(string key)
            {
                /* Unaza qe nuk lejon numra te perseritur */
                for (int i = 0; i < key.Length - 1; i++)
                {
                    for (int j = i + 1; j < key.Length; j++)
                    {
                        if (key[i] == key[j])
                        {
                            throw new Exception("@Invalid key! Make sure the numbers are not repeated!");

                        }
                    }
                }
                /* -------------------------------------------------------- */
                return new string(key);
            }
            public void Encrypt(string key, string message)
            {

                Key(key);

                /* Pjesa e enkriptimit */
                if (message.Length % 4 != 0)
                {
                    message = message.PadRight(message.Length + 4 - message.Length % 4, 'w');
                }
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
                {
                    Console.Write(message.Substring(i, 4) + ' ');
                }

                if (key.Length % message.Length != 0) 
                {
                    key += key;
                }
                Console.Write("\nKey:\t    ");
                for (int i = 0; i < key.Length; i += 4)
                {
                    Console.Write(key.Substring(i, 4) + ' ');
                }
                Console.Write("\nCipherText: ");
                for (int i = 0; i < Cipher.Length; i += 4) 
                {
                    Console.Write(Cipher.Substring(i, 4) + ' ');
                }
                Console.WriteLine();
                /* ------------ */


            }

            /* Funksioni per dekriptim */
            public void Decrypt( string key , string cipher)
            {
               
                 Key(key);

                 /* Pjesa e dekriptimit */
                char[] plainChars = new char[cipher.Length];
                for (int i = 0; i < cipher.Length; i++)
                {
                    plainChars[key[i % 4] - '1' + i / 4 * 4] = cipher[i];
                 

                }
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


        }

        public class Numerical
        {


            public string Encode(string plain)
            {
                //char[] seperator = { ' ', '+' };
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


