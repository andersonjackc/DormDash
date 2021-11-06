using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public class DatabaseOperations
    {


        public MySqlConnection GetMySqlConnection(String user, String database, String port, String password )
        {
            //"server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
            string connStr = "server=localhost;user="+ user+";database="+ database +";port=" +port + ";password=root";
            return new MySqlConnection(connStr);
            
        }

        public void WriteToDatabase(MySqlConnection connection)
        {

            try
            {
                connection.Open();


                connection.Close();
            }
            catch
            {
                Console.WriteLine("Error writing to database");
            }

        }

        public void CloseMySqlConnection(MySqlConnection connection)
        {
            try
            {
                connection.Close();
            }catch(Exception ex)
            {
                Console.WriteLine("There was an error closing the SQL connection!!");
            }
        }




        public void test()
        {
            Console.WriteLine("TEST");

            string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM tutorials_tbl";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
        }
    

    }

    public MySQLConnection GetConnection(String databaseName, )
    {
        string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM tutorials_tbl";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    

}
