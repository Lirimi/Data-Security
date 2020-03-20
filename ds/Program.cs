using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            BealeEncrypt();
            Console.Write("\nShenoni tekstin qe doni ta enkodoni: ");
            string teksti =Console.ReadLine();
            string enk = "";
            string n = EncodeNumerical(teksti, enk);
            Console.Write("Teksti i enkoduar eshte: " + n + "\n");
         
            Console.Write("Shkruaj kodin qe doni ta dekodoni: ");
            string dek = Console.ReadLine();
            int gjat = dek.Length;
            string z = DecodeNumerical(dek, gjat);
            //Decode(dek, gjat);
            Console.Write("Kodi i dekoduar eshte: " + z + "\n");
            Console.Write("\n");
            
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
            Console.Write("Teksti i enkriptuar eshte: ");
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

        static string EncodeNumerical(string teksti, string enk)
        {
            for (int i = 0; i < teksti.Length; ++i)
            {
                if (teksti[i]>=(int)'a' && teksti[i]<=(int)'z')
            {
                char ch = teksti[i];
                if (!string.IsNullOrEmpty(enk))
                    {
                    enk += " ";
                    }
                int n = (int)ch- (int)'a' + 1;


                enk += Convert.ToInt32(n);
            }
            else
            {
                continue;
            }
            }

            return new string(enk);
        }

        static string DecodeNumerical(String dek, int gjat)
        {
            StringBuilder Decode = new StringBuilder(dek);
            //Console.Write("Kodi i dekoduar eshte: ");
            int num = 0;
            for (int i = 0; i < gjat; i++)
            {
                num = (dek[i] - '1') + (int)'a';
                if (num >= (int)'a' && num <= (int)'z')
                {
                    char c = (char)num;
                    Decode[i] = c;
                    //Console.Write(c);
                    //num = 0;
                }
            }
            //Console.Write("\n");
            return Decode.ToString();
        }
    }
}




