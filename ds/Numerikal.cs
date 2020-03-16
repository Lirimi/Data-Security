using System;

namespace ds
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Shenoni tekstin qe doni ta enkodoni: ");
      string teksti =Console.ReadLine();
      string enk = "";
      string n = Encode(teksti, enk);
      Console.Write("Teksti i enkoduar eshte: " + n + "\n");
      Console.Write("\n");

      Console.Write("Shkruaj kodin qe doni ta dekodoni: ");
      string dek = Console.ReadLine();
      int gjat = dek.Length;
      Decode(dek, gjat);
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

      return new string(enk);
    }

    static void Decode(String dek, int gjat)
    {

        Console.Write("Kodi i dekoduar eshte: ");
        int num = 0;
          for (int i = 0; i < gjat; i++)
          {
            num = (dek[i] - '1') + 97;
              if (num >= 97 && num <= 122)
              {
                char c = (char)num;
                Console.Write(c);
                num = 0;
              }
          }
        Console.Write("\n");
    }
  }
}
