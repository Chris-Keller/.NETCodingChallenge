using MySql.Data.MySqlClient;

namespace orders
{
    using System;
    public class Ordertest
    {
        
        public static void Main()
        {

            bool i = true;
            
            Console.WriteLine("Please provide password to access the database: ");
            string Password = Console.ReadLine();

            while (i)
            {
                Console.WriteLine("..................");
                Console.WriteLine("Enter one of the following options to continue: ");
                Console.WriteLine("Add Order, Read Order, Update Order, Delete Order, Show All Orders, End Program:");
                MySqlConnection c1 = Repository.ConnectToDB(Password);
                string Option = Console.ReadLine();
                if (Option == "Add Order")
                {
                    Repository.AddOrder(c1);
                }if (Option == "Read Order")
                {
                    Repository.ReadOrder(c1);
                }if (Option == "Update Order")
                {
                    Repository.UpdateOrder(c1);
                }if (Option == "Delete Order")
                {
                    Repository.DeleteOrder(c1);
                }if (Option == "Show All Orders")
                {
                    Repository.ShowOrders(c1);
                }if (Option == "End Program")
                {
                    i = false;
                }else { continue; }

                c1.Close();

            }

        }
    }
}
