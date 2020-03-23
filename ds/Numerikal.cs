using System;
using System.Text;

namespace ds
{
    public class Numerikal
    {
        public Numerikal()
        { 
           
                Console.Write("\nShkruani tekstin: ");
                string text = Console.ReadLine();
                string cipher = Encode(text);
                Console.WriteLine("Cipher teksti i enkriptuar: " + cipher);

                Console.Write("Shkruani cipher tekstin: ");
                cipher = Console.ReadLine();
                string plain = Decode(cipher);
                Console.WriteLine("Plain teksti i dekriptuar: " + plain);
            }

            static string Encode(string plain)
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

            static string Decode(string cipher)
            {
                string[] chars = cipher.Split(" ");
                string plain = "";
                foreach (string s in chars)
                    plain += (char)(Int16.Parse(s) + 'a' - 1);
                return plain;
            }
        }
    }


