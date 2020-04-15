using System;

namespace ds
{
    public class Beale
    {
        public void BealeEncrypt(string plainteksti)
        {
            //Kodi per tekstin qe ndodhet ne liber
            // Nese deshironi ta beni qe te lexoj path
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
            // string teksti = System.IO.File.ReadAllText("C:\\Users\\Gentrit\\Desktop\\teksti.txt");
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale

            string libri = "fakulteti teknik";
            char[] test = libri.ToCharArray();


            char[] ch = plainteksti.ToCharArray();



            for (int i = 0; i < plainteksti.Length; i++)
            {
                for (int j = 0; j < libri.Length; j++)
                {
                    if (test[j] == ch[i])
                    {

                        Console.Write(j + " ");
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }


        public void BealeDecrypt(string[] ciphertekst)
        {
            //Kodi per tekstin qe ndodhet ne liber
            // Nese deshironi ta beni qe te lexoj path
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
            // string teksti = System.IO.File.ReadAllText("C:\\Users\\Gentrit\\Desktop\\teksti.txt");
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale 

            string libri = "fakulteti teknik";

            for (int i = 0; i < ciphertekst.Length; i++)
            {
                for (int j = 0; j < libri.Length; j++)

                    if (Convert.ToInt32(ciphertekst[i]) == j)
                    {

                        Console.Write(libri[j] + "");
                    }
            }
        }

    }
}
