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

            try
            {
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
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        /*--------------------------------------------------------------------------*/

    }

}


