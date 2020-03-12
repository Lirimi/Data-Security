using System;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Shenoni Tekstin qe ndodhet ne liber: ");
            string teksti = Console.ReadLine();
            string[] arrayTeksti = teksti.Split(" ");
            foreach (string i in arrayTeksti)
            {
                Console.WriteLine(i);
            }
            Console.Write("Shenoni plaintextin");
            string plaintekst = Console.ReadLine();
            char[] charPlaintekst = plaintekst.ToCharArray();

            //Kontrollo Nese fjala fillon me karakterin e njejte exp: plaintext=g, teksti=gentrit

            foreach(string i in arrayTeksti)
            {
                i.StartsWith(charPlaintekst[i]);
            }

        }






    }
}
