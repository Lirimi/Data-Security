using System;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            BealeEncrypt();
        }

        public static void BealeEncrypt()
        {
            //Kodi per tekstin qe ndodhet ne liber
            Console.Write("Shenoni Tekstin qe ndodhet ne liber: ");
            string teksti = Console.ReadLine();
            char[] test = teksti.ToCharArray();

            //Kodi per plainteksitin qe deshirojme me mshef.
            Console.Write("Shenoni plaintextin: ");
            string plaintekst = Console.ReadLine();
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



    }
}
