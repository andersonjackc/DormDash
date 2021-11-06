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


        public MySqlConnection GetMySqlConnection()
        {
            string connStr = "server=localhost;user=root;database=ycp_dormdash;port=3306;password=root";
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

        public void insertOrder(Order order)
        {
            MySqlConnection conn = GetMySqlConnection();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();

                string sql = "INSERT INTO 'orders' ('order_id', 'status', 'userid', 'DESTINATION', 'ordered_items', 'total', 'datetime') " +
                    "VALUES (@orderid, @status, @userid, @dest, @items, @total, @datetime)";

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@orderid", order.id);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@userid", order.userId);
                cmd.Parameters.AddWithValue("@dest", order.orderDestination);
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
