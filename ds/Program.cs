using System;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            string plaintekst = "gi";
            string teksti = "gentrit ibishi";
            string[] TekstiArray = teksti.Split(" ");
            char[] ch = plaintekst.ToCharArray();
            if (!(TekstiArray[0].StartsWith(ch[0])))
            {
                Console.WriteLine("Nuk Starton");
            }
            else
            {
                Console.WriteLine(ch[0]);
            }


        }
    }
}
