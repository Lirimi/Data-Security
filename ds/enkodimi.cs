/*-------Programi qe mundeson enkriptimin e tekstit ne numer psh A - 1 , B - 2, pa hapesira----------*/





using System;

namespace ds
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.Write("Shenoni tekstin qe doni ta enkriptoni\n");
      string teksti =Console.ReadLine();
      string enk = "";
     /*for (int i =0; i < teksti.Length; ++i)
      {
        char ch = teksti[i];
        if (!string.IsNullOrEmpty(enk))
        {
          enk += " ";
        }

        int n = (int)ch - (int)'a' + 1;
        enk += Convert.ToInt32(n);

       }
       Console.WriteLine(enk);*/
      string n = Encode(teksti, enk);
      Console.Write("Your encoded text is:\t" + n + "\n");


    }

    static string Encode(string teksti, string enk)
    {
      for (int i = 0; i < teksti.Length; ++i)
      {
      char ch = teksti[i];
      if (!string.IsNullOrEmpty(enk))
      {
        enk += " ";
      }
      int n = (int)ch- (int)'a' + 1;
      enk += Convert.ToInt32(n);
      }
      //Console.WriteLine(enk);
      return new string(enk);
    }

  }


}
