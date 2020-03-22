using System;
using System.Text;

namespace ds
{
    public class Numerikal
    {
        public Numerikal()
        {
            Console.Write("\nShenoni tekstin qe doni ta enkodoni: ");
            string teksti = Console.ReadLine();
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

        static string EncodeNumerical(string teksti, string enk)
        {
            for (int i = 0; i < teksti.Length; ++i)
            {
                if (teksti[i] >= (int)'a' && teksti[i] <= (int)'z')
                {
                    char ch = teksti[i];
                    if (!string.IsNullOrEmpty(enk))
                    {
                        enk += " ";
                    }
                    int n = (int)ch - (int)'a' + 1;


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
                num = (dek[i] - '1') + (int)'a'; //1 = 97 
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
