/*-------Programi qe mundeson enkriptimin e tekstit ne numer psh A - 1 , B - 2, pa hapesira----------*/





using System;

namespace ds
{
  class Program
  {
    static void Main(string[] args)
    {

      Console.Write("Shenoni tekstin qe doni ta enkriptoni\n");
      string s =Console.ReadLine();
      string t = "";
      for (int i =0; i < s.Length; ++i)
      {
        char ch = s[i];
        if (!string.IsNullOrEmpty(t))
        {
          t += " ";
        }

        int n = (int)ch - (int)'a' + 1;
        t += Convert.ToInt32(n);

       }

      Console.WriteLine(t);

    }


  }


}
