using System;
using System.Text;

namespace ds
{
    public class Numerical_Encoding : Encoding_Standard
    {
        
        private StringBuilder _stringBuilder = new StringBuilder();
        public string Encode(string plain)
        {
            foreach (char c in plain)
                if (c >= 'a' && c <= 'z')
                    _stringBuilder.Append((c - 'a' + 1).ToString()).Append(' ');
                else
                    _stringBuilder.Append('0').Append(' ');
            return _stringBuilder.ToString().Trim();
        }
        public string Decode(string cipher)
        {
            string[] chars = cipher.Split(" ");
           
            foreach (string s in chars)
            {
                int numri = Int32.Parse(s);
                if (numri >= 0 && numri < 27)
                    if (numri >= 1 && numri <= 26)
                        _stringBuilder.Append((char)(Int32.Parse(s) + 'a' - 1));
                    else
                        _stringBuilder.Append(" ");
                else
                    throw new Exception("Numri nuk guxon te jete me i madh se 26!");
            }
            
            return _stringBuilder.ToString();
        }
    }
}