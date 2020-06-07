using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ds
{
    public class DatabaseConnection
    {
        MySqlConnection conn;
        static string host = "localhost";
        static string database = "DSusers";
        static string userDB = "root";
        static string password = "password";
  
        private static string strProvider = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password +";CharSet=utf8";

        public bool Open()
        {
            try
            {
                
                conn = new MySqlConnection(strProvider);
                conn.Open();
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Connection Error ! " + er.Message);
            }

        }

        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet DataSet(string sql)
        {
            DataSet DS = new DataSet();
            var SqlCommnad = new MySqlCommand(sql, conn);
            var DA = new MySqlDataAdapter(SqlCommnad);
            try
            {
                DA.Fill(DS);
                return DS;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Close()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}