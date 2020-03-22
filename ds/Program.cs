using System;
using System.Text;


namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            BealeEncrypt();
            Console.WriteLine();
            BealeDecrypt();
            Console.WriteLine();
            PermutationEncrypt();
            Console.WriteLine();
            PermutationDecrypt();
            Numerikal N = new Numerikal();
            
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

        public static void BealeDecrypt()
        {

            string teksti = "gentrit ibishi";

            Console.Write("Shkruani CipherTekstin(Example: 1 2 3, duke lene nje hapesire): ");
            string[] ciphertekst = Console.ReadLine().Split(' ');


            // Nese eshte zero merre karakterin zero prej tekstit

            for (int i = 0; i < ciphertekst.Length; i++)
            {
                for (int j = 0; j < teksti.Length; j++)

                    if (Convert.ToInt32(ciphertekst[i]) == j)
                    {
                        Console.Write(teksti[j] + " ");
                    }

            }


            //Dekripton ciphertextin <ciphertext> duke u bazuar në një text file <book> që e paraqet librin. 
            //Plaintexti i fituar shfaqet në ekran.

        }

        
        
        
        

        static void PermutationEncrypt()
        {
            string celsi, mesazhi;
            int x = 0, y = 0;
            Console.Write("Jepni celsin per enkriptim: ");
            celsi = Console.ReadLine();
            if (Int64.Parse(celsi) >= 1 && Int64.Parse(celsi) <= 4 || celsi.Length == 4)
            {
                Console.Write("Jepni tekstin: ");
                mesazhi = Console.ReadLine();


                int blloku = 4;
                while (mesazhi.Length % blloku != 0)
                {
                    mesazhi = mesazhi + "w";
                }
                StringBuilder mesazhibuild = new StringBuilder(mesazhi);
                int stringLength = mesazhibuild.Length;

                char[,] temp = new char[celsi.Length, mesazhi.Length];
                string v = temp.ToString();
                //string v = new string();
                // Console.Write(v);
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

                Console.Write("Plaintext: ");

                for (int i = 0; i < stringLength; i = i + blloku)
                {

                    Console.Write(mesazhi.Substring(i, blloku) + ' ');

                }



                Console.Write("\nCiphertext i enkriptuar: ");

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
                        Console.Write(temp[pos, j]);

                    }
                    Console.Write(' ');
                }
            }
            else
            {
                Console.Write("Celsi duhet te jete ndermjet 1 dhe 4 dhe duhet te jete 4 bitesh!\n");
                //Environment.Exit(0);
                PermutationEncrypt();
            }
        }

        static void PermutationDecrypt()
        {
            string cels, mesazhi;
            int x = 0, y = 0;

            Console.Write("Jepni celsin per dekriptim: ");
            cels = Console.ReadLine();
            char[] key = cels.ToCharArray();
            Array.Reverse(key);

            string celsi = new string(key);


            if (Int64.Parse(celsi) >= 1 && Int64.Parse(celsi) <= 4 || celsi.Length == 4)
            {
                Console.Write("Jepni tekstin: ");
                mesazhi = Console.ReadLine();

                int blloku = 4;
                while (mesazhi.Length % blloku != 0)
                {
                    mesazhi = mesazhi + "w";
                }
                StringBuilder mesazhibuild = new StringBuilder(mesazhi);
                int stringLength = mesazhibuild.Length;

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

                Console.Write("Plaintext: ");

                for (int i = 0; i < stringLength; i = i + blloku)
                {

                    Console.Write(mesazhi.Substring(i, blloku) + ' ');

                }


                Console.Write("\nCiphertext i dekriptuar: ");
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

                StringBuilder mesazhibuild2 = new StringBuilder(mesazhidekriptuar);
                int stringLength2 = mesazhibuild2.Length;
                for (int i = 0; i < stringLength2; i = i + blloku)
                {

                    string f = (mesazhidekriptuar.Substring(i, blloku));
                    StringBuilder F = new StringBuilder(f);
                    F.Replace("w", "");
                    string Dekriptimi = F.ToString();
                    Console.Write(Dekriptimi);

                }


            }
            else
            {
                Console.Write("Celsi duhet te jete ndermjet 1 dhe 4 dhe duhet te jete 4 bitesh!\n");
                PermutationDecrypt();
            }


        }

       
    }
}




