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


        public MySqlConnection GetMySqlConnection( )
        {
            
            string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
            return new MySqlConnection(connStr);
            
        }


        public void insertUser(User user)
        {
            string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand();
                String SQL = "INSERT INTO users (id, user_type, dining_balance, flex_balance, email, salt, hash)" +
                    "VALUES (@id, @user_type, @dining_balance, @flex_balance, @email, @salt, @hash)";
                command.CommandText = SQL;
                
                command.Parameters.AddWithValue("@id", user.id );
                command.Parameters.AddWithValue("@user_type", (int) user.userType);
                command.Parameters.AddWithValue("@dining_balance", user.diningBalance);
                command.Parameters.AddWithValue("@flex_balance", user.flexBalance);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@salt", user.salt);
                command.Parameters.AddWithValue("@hash", user.hashPWD);
                string test = command.CommandText;
                command.Connection = conn;
                Console.WriteLine("command " + test);
                command.ExecuteNonQuery();
                conn.Close();

            }catch(Exception ex)
            {
                Console.WriteLine("Error inserting user" +ex);
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


}
