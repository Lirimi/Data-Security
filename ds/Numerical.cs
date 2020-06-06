using System;

namespace ds
{
    public class Numerical
    {
        public string Encode(string plain)
        {
            string cipher = "";
            foreach (char c in plain)
            {
                if (c >= 'a' && c <= 'z')
                {
                    cipher += (c - 'a' + 1).ToString();
                    cipher += ' ';
                }
                else
                {
                    cipher += '0';
                    cipher += ' ';
                }
            }

            return cipher.Trim();
        }

        public string Decode(string cipher)
        {
            string[] chars = cipher.Split(" ");
            string plain = "";
            foreach (string s in chars)
            {
                int numri = Int32.Parse(s);
                if (numri >= 0 && numri < 27)
                    if (numri >= 1 && numri <= 26)
                        plain += (char) (Int16.Parse(s) + 'a' - 1);
                    else
                        plain += " ";
                else
                    throw new Exception("Numri nuk guxon te jete me i madh se 26!");
            }

            return plain;
        }
    }
}