using System;
using MySql.Data.MySqlClient;

namespace ds
{
    public class DatabaseManipulation : GenerateHashedPassword
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


        protected void _deletefromDB(String user)
        {
            try
            {
                String query = "DELETE FROM users WHERE USER=" + "'" + user + "';";

                if (!DB.Open()) return;
                MySqlDataReader row;
                row = DB.ExecuteReader(query);
                if (row != null)
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