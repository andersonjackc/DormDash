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
        public DatabaseOperations()
        {

        }

        public static MySqlConnection GetMySqlConnection( )
        {
            string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
            return new MySqlConnection(connStr);
        }


        public static void insertUser(User user)
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



        public static bool LogUserIn(string username, string password)
        {
            string userSalt = " ";
            string userHash = " ";
            MySqlConnection conn = GetMySqlConnection();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT salt, hash from users where email ='" + username + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr[0] != DBNull.Value && rdr[1] != DBNull.Value)
                    {
                        userSalt = rdr[0].ToString();
                        userHash = rdr[1].ToString();
                        Console.Write(userSalt + "  :  " + userHash);
                    }
                    else
                    {
                        Console.WriteLine("Error retrieving hash and salt");
                        return false;
                    }

                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

            string hashedPass = User.HashPassword(password, userSalt, 10101, 70);
            if (userHash == hashedPass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
      public void insertOrder(Order order)
        {
            MySqlConnection conn = GetMySqlConnection();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();

                string sql = "INSERT INTO orders (status, userid, DESTINATION, ordered_items, total, datetime) " +
                    "VALUES (@status, @userid, @dest, @items, @total, @datetime)";

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@userid", order.userId);
                String dest = order.orderDestination.building + ":" + order.orderDestination.roomNumber;
                cmd.Parameters.AddWithValue("@dest", dest);
                String items = "";
                foreach(MenuItem item in order.orderedItems)
                {
                    items += "$";
                    items += item.Name;
                    items += ":";
                    items += item.price;
                }
                cmd.Parameters.AddWithValue("@items", items);
                cmd.Parameters.AddWithValue("@total", order.runningTotal);
                cmd.Parameters.AddWithValue("@datetime", order.orderTime);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                conn.Close();
            }

        }
    }
}


        
