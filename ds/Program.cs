using System;
using System.Text.RegularExpressions;

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
                Console.WriteLine("\n@Per ekzekutimin e kodit Beale shtyp | ds.exe Beale Encrypt <text> | ose | ds.exe Beale Decrypt <text> ku text te decrypt duhet te jete me thonjeza like: 0 1 2 |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Permutation shtyp | ds.exe Permutation Encrypt <key><text> | ose | ds.exe Permutation Decrypt <key><text> |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Numerical shtyp | ds.exe Numerical Encode <text> | ose | ds.exe Numerical Decode <text> |");
                Environment.Exit(1);

            }
            if (args.Length <= 2 || args.Length > 4)
            {
                Console.WriteLine("\n@Numri i argumenteve jo i lejuar!");
                Console.WriteLine("\n@Per ekzekutimin e kodit Beale shtyp | ds.exe Beale Encrypt <text> | ose | ds.exe Beale Decrypt <text> ku text te decrypt duhet te jete me thonjeza like: 0 1 2 |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Permutation shtyp | ds.exe Permutation Encrypt <key><text> | ose | ds.exe Permutation Decrypt <key><text> |");
                Console.WriteLine("\n@Per ekzekutimin e kodit Numerical shtyp | ds.exe Numerical Encode <text> | ose | ds.exe Numerical Decode <text> |");
                Environment.Exit(1);
            }

            /*---------------Args per Numerical-----------------*/
            if (args[0].Equals("Numerical"))
            {
                if (args[1].Equals("Encode"))
                {

                    String text = args[2];
                    if (Regex.IsMatch(text, "^[a-z]+"))
                    {
                        string Cipher = N.Encode(text);
                        Console.WriteLine("Encoded text is:" + Cipher);
                    }
                    else
                    {
                        Console.Write("Argumenti i fundit lejohet te permbaje vetem shkronja te vogla sipas alfabetit anglez prej a-z!");

                    }
                }
                else if (args[1].Equals("Decode"))
                {
                    String text = args[2];
                    if (Regex.IsMatch(text, "^[0-9]+"))
                    {
                        string Plain = N.Decode(text);
                        Console.WriteLine("Decoded cipher is:" + Plain);
                    }
                    else
                    {
                        Console.Write("Argumenti i fundit lejohet te permbaje vetem numra 0-9!");
                    }

                }
                else
                {
                    Console.Write("Argumenti eshte jo valid! (Args must be | Encode | or | Decode |)");
                }
            }



            /*---------------Args per Permutation-----------------*/
            else if (args[0].Equals("Permutation"))
            {
                if (args[1].Equals("Encrypt"))
                {

                    String key = args[2];
                    String text = args[3];
                    if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4 && Regex.IsMatch(text, "^[a-zA-Z]+$"))
                    {
                        string Cipher = P.Encrypt(key, text);
                        Console.WriteLine("Encryptet plaintext is: " + Cipher);
                    }
                    else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4 && Regex.IsMatch(text, "^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Key is either too long or too short (Make sure its 4 charecters only!");
                    }
                    else
                    {

                        throw new Exception("Keep in mind that the first argument allows only numbers from 1-4 and the second argument allows only EN alphabetic characters!");
                    }
                }
                else if (args[1].Equals("Decrypt"))
                {
                    String key = args[2];
                    String text = args[3];
                    if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4 && Regex.IsMatch(text, "^[a-zA-Z]+$"))
                    {
                        string Plain = P.Decrypt(key, text);
                        Console.WriteLine("Decryptet Ciphertext is: " + Plain);
                    }
                    else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4 && Regex.IsMatch(text, "^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Key is either too long or too short (Make sure its 4 charecters only!");
                    }
                    else
                    {

                        throw new Exception("Keep in mind that the first argument allows only numbers from 1-4 and the second argument allows only EN alphabetic characters!");
                    }
                }
                else
                {
                    Console.Write("E R R O R  ! Make sure you passed the argument right | Encrypt | or | Decrypt |!");
                }

            }

            /*---------------Args per Beale-----------------*/
            else if (args[0].Equals("Beale"))
            {
                if (args[1].Equals("Encrypt"))
                {
                    String plainteksti = args[2];
                    if (Regex.IsMatch(plainteksti, "^[a-zA-Z]+"))
                    {
                        Console.Write("Encrypted plaintext is: ");
                        B.BealeEncrypt(plainteksti);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Argumenti i fundit duhet te permbaje tekst a-z ose A-Z ne varesi nga teksti");
                    }

                }
                else if (args[1].Equals("Decrypt"))
                {
                    String[] ciphertekst = args[2].Split(" ");
                    Console.Write("Decrypted Ciphertext is: ");
                    B.BealeDecrypt(ciphertekst);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Argumenti eshte jo valid! (Args must be Encrypt or Decrypt) !");
                }
            }
            else
            {
                Console.Write("Argumenti duhet te jete | Beale | ose | Permutation | ose | Numerical |");
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
            public string Encrypt( string key, string message)
            {
                /* Unaza qe nuk lejon numra te perseritur */
                for (int i = 0; i < key.Length - 1; i++)
                {
                    for (int j = i + 1; j < key.Length; j++)
                    {
                        if (key[i] == key[j])
                        {
                            throw new Exception("Invalid key!");
                           
                        }
                    }
                }
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
            public string Decrypt( string key , string cipher)
            {
                /* Unaza qe nuk lejon numra te perseritur */
                for (int i = 0; i < key.Length - 1; i++)
                {
                    for (int j = i + 1; j < key.Length; j++)
                    {
                        if (key[i] == key[j])
                        {
                            throw new Exception("Invalid key!");

                        }
                    }
                }
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


