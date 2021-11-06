using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

using WebApplication1;

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
      
        //returns the ID of the order inserted on success
      public int insertOrder(Order order)
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
                    items += item.Name;
                    items += ":";
                    items += item.price;
                    items += "$";
                }
                cmd.Parameters.AddWithValue("@items", items);
                cmd.Parameters.AddWithValue("@total", order.runningTotal);
                cmd.Parameters.AddWithValue("@datetime", order.orderTime);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                sql = "SELECT MAX(order_id) from orders;";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                int maxID;
                    if (rdr[0] != DBNull.Value)
                    {
                        maxID = Int32.Parse(rdr[0].ToString());
                        
                    }
                    else
                    {
                        maxID = -1;
                    }
                
                rdr.Close();
                conn.Close();
                return maxID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                conn.Close();
                return -1;
            }

        }

        public void insertMenuItem(MenuItem menuItem)
        {
            MySqlConnection conn = GetMySqlConnection();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();

                string sql = "INSERT INTO sparts_menu (itemname, itemdescription, price) " +
                    "VALUES (@name, @descript, @price)";

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name", menuItem.Name);
                cmd.Parameters.AddWithValue("@descript", menuItem.description);
                cmd.Parameters.AddWithValue("@price", menuItem.price);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                conn.Close();
            }

        }

        public void updateOrder(Order order)
        {
            MySqlConnection conn = GetMySqlConnection();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();

                string sql = "UPDATE orders SET status=@status , userid=@userid , DESTINATION=@dest , ordered_items=@items , total=@total , datetime=@datetime " +
                    "WHERE order_id = @id";

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@userid", order.userId);
                String dest = order.orderDestination.building + ":" + order.orderDestination.roomNumber;
                cmd.Parameters.AddWithValue("@dest", dest);
                String items = "";
                foreach (MenuItem item in order.orderedItems)
                {
                    items += "$";
                    items += item.Name;
                    items += ":";
                    items += item.price;
                }
                cmd.Parameters.AddWithValue("@items", items);
                cmd.Parameters.AddWithValue("@total", order.runningTotal);
                cmd.Parameters.AddWithValue("@datetime", order.orderTime);
                cmd.Parameters.AddWithValue("@id", order.id);
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


        public static List<MenuItem> selectMenuItems()
        {
            
            MySqlConnection conn = GetMySqlConnection();
            try
            {
                conn.Open();
                List<MenuItem> menuItems = new List<MenuItem>();

                string sql = "SELECT * from sparts_menu;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr[0] != DBNull.Value && rdr[1] != DBNull.Value && rdr[2] != DBNull.Value && rdr[3] != DBNull.Value && rdr[4] != DBNull.Value && rdr[5] != DBNull.Value)
                    {
                        MenuItem tempItem = new MenuItem((int)rdr[0], rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), (double)(decimal)rdr[4], ((int)rdr[5]) == 1 ? true : false);
                        menuItems.Add(tempItem);
                    }
                    

                }
                rdr.Close();
                return menuItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                conn.Close();
                return null;
            }
        }

        public static List<Order> selectOrdersByUserID(int userID)
        {

            MySqlConnection conn = GetMySqlConnection();
            try
            {
                conn.Open();
                List<Order> orders = new List<Order>();

                string sql = "SELECT * from orders where userid = @id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", userID);

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr[0] != DBNull.Value && rdr[1] != DBNull.Value && rdr[2] != DBNull.Value && rdr[3] != DBNull.Value && rdr[4] != DBNull.Value && rdr[5] != DBNull.Value && rdr[6] != DBNull.Value)
                    {

                        String[] destinationStringArray = rdr[4].ToString().Split(':');
                        Destination dest = new Destination((building)Int32.Parse(destinationStringArray[0]), Int32.Parse(destinationStringArray[1]));

                        List <MenuItem> tempMenuItemList = new List<MenuItem>();
                        String[] menuItemsStringArray = rdr[5].ToString().Split('$');
                        foreach(String item in menuItemsStringArray)
                        {
                           String[] menuItemVals = item.Split(':');
                           MenuItem tempMenuItem = new MenuItem(menuItemVals[0], Double.Parse(menuItemVals[1]));
                           tempMenuItemList.Add(tempMenuItem);
                        }

                        Order tempOrder = new Order((int)rdr[0], (int)rdr[1], DateTime.Parse(rdr[2].ToString()), (double)rdr[3], dest, tempMenuItemList);
                        orders.Add(tempOrder);
                    }


                }
                rdr.Close();
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                conn.Close();
                return null;
            }
        }

    }
}


        
