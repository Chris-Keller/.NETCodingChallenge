namespace orders
{
    using System;
    using System.Collections.Generic;
    using MySql.Data.MySqlClient;

    internal class Repository
    {
        public static MySqlConnection ConnectToDB(string Password)
        {
            try
            {
                // Connect to the MySQL database
                string connstring = "server=127.0.0.1;uid=root;pwd="+Password+";database=models";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                return con;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Connection failed!");
                return null;
            }
        }
        public static void AddOrder(MySqlConnection con)
        {
            try
            {
                Console.WriteLine("Enter Order ID: ");
                if (!int.TryParse(Console.ReadLine(), out int Id))
                {
                    Console.WriteLine("Invalid Order ID. Please enter a valid integer.");
                    return; // Exit the function due to invalid input
                }

                Console.WriteLine("Enter Order Type (Standard, Sale Order, Purchase Order, Transfer Order, Return Order): ");
                string Type = Console.ReadLine();

                // Check if the entered type is not one of the allowed types
                string[] allowedTypes = { "Standard", "Sale Order", "Purchase Order", "Transfer Order", "Return Order" };
                if (!allowedTypes.Contains(Type))
                {
                    Console.WriteLine("Invalid Order Type. Please enter one of the allowed types.");
                    return; // Exit the function due to invalid input
                }

                Console.WriteLine("Enter Order customer's name: ");
                string Cname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Cname))
                {
                    Console.WriteLine("Customer name cannot be empty or whitespace.");
                    return; // Exit the function due to invalid input
                }

                string Date = DateTime.Now.ToString();
                Console.WriteLine("Enter name of who is creating the Order: ");
                string Crby = Console.ReadLine();

                Order NOrder = new Order(Id, Type, Cname, Date, Crby);

                string sql = "INSERT INTO models.orderstable (ID, Type, CustomerName, Date, Createdbyusername) VALUES (@Id, @Type, @Cname, @Date, @Crby);";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Cname", Cname);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Crby", Crby);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }



        public static void ReadOrder(MySqlConnection con)
        {
            Console.WriteLine("Enter ID of the order you would like displayed: ");
            string id = Console.ReadLine();
            string sql = "SELECT * FROM models.orderstable WHERE ID = " + id + ";";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Order> R = new List<Order>();

            while (reader.Read())
            {
                Order O = new Order(int.Parse(reader["id"].ToString()), reader["type"].ToString(), reader["CustomerName"].ToString(), reader["Date"].ToString(), reader["Createdbyusername"].ToString());
                R.Add(O);
            }
            foreach (Order rPart in R)
            {
                Console.WriteLine("ID: " + rPart.GetId() + ", Type: " + rPart.GetType() + ", Customer Name: " + rPart.GetCustomerName() + ", Date: " + rPart.GetDate() + ", Created by Username: " + rPart.GetCreatedBy());
            }
            con.Close();
        }

        public static void UpdateOrder(MySqlConnection con)
        {
            try
            {
                Console.WriteLine("Enter ID of the order you would like to update: ");
                string id = Console.ReadLine();

                // Check if the entered ID is a valid integer
                if (!int.TryParse(id, out int orderId))
                {
                    Console.WriteLine("Invalid Order ID. Please enter a valid integer.");
                    return; // Exit the function due to invalid input
                }

                Console.WriteLine("Enter the field to be changed (Type, CustomerName, Date, Createdbyusername): ");
                string fieldToUpdate = Console.ReadLine();

                // Check if the entered field is one of the allowed fields
                string[] allowedFields = { "Type", "CustomerName", "Date", "Createdbyusername" };
                if (!allowedFields.Contains(fieldToUpdate))
                {
                    Console.WriteLine("Invalid field name. Please enter one of the allowed fields.");
                    return; // Exit the function due to invalid input
                }

                Console.WriteLine($"Enter the new value for the {fieldToUpdate}: ");
                string newValue = Console.ReadLine();

                string sql = $"UPDATE models.orderstable SET {fieldToUpdate} = @NewValue WHERE ID = @OrderId;";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@NewValue", newValue);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Order ID {orderId} updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Order ID {orderId} not found.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        public static void DeleteOrder(MySqlConnection con)
        {
            Console.WriteLine("Enter ID of the order you would like to delete: ");
            string id = Console.ReadLine();
            string sql = "DELETE FROM models.orderstable WHERE ID = " + id + ";";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void ShowOrders(MySqlConnection con)
        {
            string sql = "SELECT * FROM models.orderstable";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Order> R = new List<Order>();

            while (reader.Read())
            {
                Order O = new Order(int.Parse(reader["id"].ToString()), reader["type"].ToString(), reader["CustomerName"].ToString(), reader["Date"].ToString(), reader["Createdbyusername"].ToString());
                R.Add(O);
            }
            foreach (Order rPart in R)
            {
                Console.WriteLine("ID: " + rPart.GetId() + ", Type: " + rPart.GetType() + ", Customer Name: " + rPart.GetCustomerName() + ", Date: " + rPart.GetDate() + ", Created by Username: " + rPart.GetCreatedBy());
            }
            con.Close();
        }
    }
}
