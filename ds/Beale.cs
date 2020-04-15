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

            string libri = "fakulteti teknik";  // libri  
            char[] test = libri.ToCharArray();  // shendrrojme stringun librin ne varg karakteresh  

            //Kodi per plainteksitin qe deshirojme me mshef.
            //string plaintekst = plainteksti;
            char[] ch = plainteksti.ToCharArray(); // shendrrojme stringun plaintext ne varg karakteresh 

            //for loop 

            for (int i = 0; i < plainteksti.Length; i++)      // fiek         
            {
                for (int j = 0; j < libri.Length; j++)       // fakulteti teknik    === 0 8 6 0 
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

        // Funksioni BealeDecrypt

        public void BealeDecrypt(string[] ciphertekst)
        {
            //Kodi per tekstin qe ndodhet ne liber
            // Nese deshironi ta beni qe te lexoj path
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale
            // string teksti = System.IO.File.ReadAllText("C:\\Users\\Gentrit\\Desktop\\teksti.txt");
            // Per me lexu FilePath, te pjesa Gentrit shenoni userin tuaj gjithashtu krijoni 1 file teksti.txt qe permban fjale 

            string libri = "fakulteti teknik"; //libri

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
