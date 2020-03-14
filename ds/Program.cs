using System;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {

            // Fjala
            Console.Write("Shenoni Tekstin qe ndodhet ne liber: ");
            string teksti = Console.ReadLine();
            string[] arrayTeksti = teksti.Split(" ");
            // gentrit(0) ibishi(1)

            // Plainteksti
            Console.Write("Shenoni plaintextin: ");
            string plaintekst = Console.ReadLine();
            char[] ch = plaintekst.ToCharArray();
            for(int i=0;i<arrayTeksti.Length;i++)
            {
                if(arrayTeksti[i].StartsWith(ch[0]))
                {
                    Console.WriteLine(i);
                }
                else
                {
                    continue;
                }
            }
        }

        




    }
}
