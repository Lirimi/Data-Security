using System;

namespace ds
{
    public class Beale
    {
        public void BealeEncrypt(string plainteksti)
        {
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
                }
            }
        }


        public void BealeDecrypt(string[] ciphertekst)
        {
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