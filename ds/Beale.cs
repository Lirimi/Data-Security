using System;
using System.Collections.Generic;
using System.Text;

namespace ds
{

   public class Beale
    {
        static void Main(string[] args)
        {
            Console.Write("Shkruani plaintekstin: ");
            string plainteksti = Console.ReadLine();
            BealeEncrypt(plainteksti);

            Console.Write("\nShkruani ciphertekstin: ");
            string[] ciphertekst = Console.ReadLine().Split(' ');
            BealeDecrypt(ciphertekst);
        }

        // Funksioni BealeEncrypt

        public static string BealeEncrypt(string plainteksti)
        {
            //Kodi per tekstin qe ndodhet ne liber
            string teksti = "fakulteti teknik eshte fakulteti me i mire!";
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

        public static string BealeDecrypt(string[] ciphertekst)
        {
            string teksti = "fakulteti teknik eshte fakulteti me i mire!";

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
}
