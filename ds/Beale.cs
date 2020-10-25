using System;
using System.IO;
using System.Text;

namespace ds
{
    public class Beale : Encryption_Standard 
    {
        private static readonly String keyPath = "Beale/KeyFile";

        private String content()
        {
            if (File.Exists(keyPath))
                return File.ReadAllText(keyPath);
            throw new Exception("File does not exist!");
        }

        public String Encrypt(String plaintext)
        {
            return Translate(true, plaintext);
        }

        public String Decrypt(String ciphertext)
        {
            return Translate(false, ciphertext);
        }

        private String Translate(bool Encrypt, String Text)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < Text.Length; i++)
            for (int j = 0; j < content().Length; j++)
            {
                if (Encrypt)
                {
                    if (content()[j] == Text[i])
                    {
                        sb.Append(j).Append(" ");
                        break;
                    }
                }
                else if (i<Text.Split(' ').Length && Convert.ToInt32(Text.Split(' ')[i]) == j)
                { 
                    sb.Append(content()[j]).Append(""); 
                }
            }

            return sb.ToString();
        }
    }
}