using System;
using System.Text;
namespace ds
{
    public class Permutation
    {
        public Permutation()
        {
            Console.Write("\nPlaintext: ");
            string tekstiE = Console.ReadLine();
            Console.Write("Celsi: ");
            string celsiE = Console.ReadLine();
            PermutationEncrypt(celsiE, tekstiE);

            Console.Write("\nCiphertext: ");
            string tekstiD = Console.ReadLine();
            Console.Write("Celsi: ");
            string celsiD = Console.ReadLine();
            PermutationDecrypt(celsiD, tekstiD);
        }
        static string Ndarja(string mesazhi)
        {

            int blloku = 4;
            while (mesazhi.Length % blloku != 0)
            {
                mesazhi += "w";
            }
            StringBuilder mesazhibuild = new StringBuilder(mesazhi);
            int stringLength = mesazhibuild.Length;
            Console.Write("Plaintext:  ");

            for (int i = 0; i < stringLength; i += blloku)
            {

                Console.Write(mesazhi.Substring(i, blloku) + ' ');
            }


            return mesazhibuild.ToString();
        }






        static string PermutationEncrypt(string celsi, string mesazh)
        {

            int x = 0, y = 0;

            string mesazhi = Ndarja(mesazh);


            Console.Write("\nKey:\t    ");
            StringBuilder celsiBuild = new StringBuilder(celsi);
            String NCelsi = celsiBuild.ToString();
            while (NCelsi.Length / mesazhi.Length != 1)
            {
                NCelsi += NCelsi;

            }
            StringBuilder celsibuild2 = new StringBuilder(NCelsi);
            int celsiLength = celsibuild2.Length;
            for (int i = 0; i < celsiLength; i += 4)
            {
                Console.Write(NCelsi.Substring(i, 4) + ' ');
            }

            char[,] temp = new char[celsi.Length, mesazhi.Length];
            char[] msg = mesazhi.ToCharArray();

            for (int i = 0; i < msg.Length; i++)
            {
                temp[x, y] = msg[i];

                if (x == (celsi.Length - 1))
                {
                    x = 0;
                    y += 1;

                }
                else
                {
                    x++;
                }

            }

            char[] t;
            t = celsi.ToCharArray();
            Array.Sort(t);
            Console.Write("\nCiphertext: ");
            string mesazhidekriptuar = "";

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < celsi.Length; i++)
                {
                    //position
                    int pos = 0;
                    for (pos = 0; pos < t.Length; pos++)
                    {
                        if (celsi[i] == t[pos]) { break; }
                    }
                    mesazhidekriptuar = temp[pos, j].ToString();
                    Console.Write(mesazhidekriptuar);
                    //    Console.Write(' ');

                }
                Console.Write(' ');
            }



            return mesazhidekriptuar.ToString();

        }

        static string PermutationDecrypt(string celsi, string mesazh)
        {

            int x = 0, y = 0;

            string mesazhi = Ndarja(mesazh);
            /*char[] key = cels.ToCharArray();
            Array.Reverse(key);
            string celsi = new string(key);
            */

            Console.Write("\nKey:\t    ");
            StringBuilder celsiBuild = new StringBuilder(celsi);
            String NCelsi = celsiBuild.ToString();
            while (NCelsi.Length / mesazhi.Length != 1)
            {
                NCelsi += NCelsi;

            }
            StringBuilder celsibuild2 = new StringBuilder(NCelsi);
            int celsiLength = celsibuild2.Length;
            for (int i = 0; i < celsiLength; i += 4)
            {
                Console.Write(NCelsi.Substring(i, 4) + ' ');
            }





            //temp-ruajme data te perkohshme
            char[,] temp = new char[celsi.Length, mesazhi.Length];

            char[] msg = mesazhi.ToCharArray();

            for (int i = 0; i < msg.Length; i++)
            {
                temp[x, y] = msg[i];
                if (x == (celsi.Length - 1))
                {
                    x = 0;
                    y += 1;

                }
                else
                {
                    x++;
                }
            }

            char[] t;
            t = celsi.ToCharArray();
            Array.Sort(t);




            Console.Write("\nPlaintext: ");
            string mesazhidekriptuar = "";
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < celsi.Length; i++)
                {
                    //position
                    int pos = 0;
                    for (pos = 0; pos < t.Length; pos++)
                    {
                        if (celsi[i] == t[pos]) { break; }
                    }
                    //Console.Write(temp[pos, j]);
                    mesazhidekriptuar += temp[pos, j];

                }

            }
            //Funksioni me popshte sherben per te hequr shkronjat qe kane sherbuar me heret per te mbushur plaintextin dhe per enkriptim me efikes te plaintextit
            string Dekriptimi = "";
            StringBuilder mesazhibuild2 = new StringBuilder(mesazhidekriptuar);
            int stringLength2 = mesazhibuild2.Length;
            for (int i = 0; i < stringLength2; i = i + 4)
            {

                string f = (mesazhidekriptuar.Substring(i, 4));
                StringBuilder F = new StringBuilder(f);
                F.Replace("w", "");
                Dekriptimi = F.ToString();
                Console.Write(Dekriptimi);

            }
            return Dekriptimi.ToString();

        }
    }
}