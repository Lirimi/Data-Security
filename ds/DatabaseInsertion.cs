using System;
using MySql.Data.MySqlClient;

namespace ds
{
    public class DatabaseInsertion : GenerateHashedPassword
    {
        protected DatabaseConnection DB = new DatabaseConnection();

        protected void _insertIntoDB(String user)
        {
            
            String saltBytes = Convert.ToBase64String(Salt());
            try
            {
                // if we want added security is recommended to use prepared statements
                String query = "INSERT INTO users VALUES" + "('" + user + "','" + GetPassword() + "','" + saltBytes + "')";

                if (!DB.Open()) return;
                MySqlDataReader row;
                row = DB.ExecuteReader(query);
                if (row != null)
                    Console.WriteLine("Eshte krijuar shfrytezuesi " + user);
                DB.Close();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}