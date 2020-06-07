using System;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace ds
{
    public class Deleteuser
    {
        DatabaseConnection DB = new DatabaseConnection();

        public void DeleteRsaKey(string privateKeyPath, string publicKeyPath, int size)
        {
            File.Delete(privateKeyPath);
            File.Delete(publicKeyPath);
        }

        public void DeleteRsaKey(string publicKeyPath, int size)
        {
            File.Delete(publicKeyPath);
        }

        public void DeletefromDB(string user)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                String query = "DELETE FROM users WHERE USER=" + "'" + user + "';";

                DB.Open();
                MySqlDataReader row;
                row = DB.ExecuteReader(query);
                Console.WriteLine("Eshte fshire shfrytezuesi " + user);
                DB.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}