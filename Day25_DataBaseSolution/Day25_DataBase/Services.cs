using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25_DataBase
{
    internal class Services : IServices
    {
       
        SqlConnection conn = new SqlConnection("Server=LAPTOP-JIQ20LLJ\\ROOT;Integrated Security=true;Initial Catalog=northwind;TrustServerCertificate=True");

        public Services() {
            
        }
        
        public void orderSummary()
        {
            Console.WriteLine("Enter customer id");
            string customerId = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"SELECT * FROM orders where CustomerID = @customerId order by OrderDate desc", conn);
            adapter.SelectCommand.Parameters.AddWithValue("@customerId", customerId);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"OrderId: {row["OrderID"]}");
                    Console.WriteLine($"OrderDate: {row["OrderDate"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void shipperDetails()
        {
            Console.WriteLine("Enter orderId");
            string orderId = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"select s.ShipperId,s.CompanyName,s.Phone from Shippers as s join Orders as o on s.ShipperID = o.ShipVia where o.OrderID = @orderID",conn);
            adapter.SelectCommand.Parameters.AddWithValue("@orderId", orderId);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"ShipperId:\n {row["ShipperId"]}");
                    Console.WriteLine($"CompanyName:\n {row["CompanyName"]}");
                    Console.WriteLine($"Phone:\n {row["Phone"]}");
                }
            }

            catch (Exception e) {
               Console.Write(e.Message);
            
            }
        }

        public void viewOrder()
        {
            Console.WriteLine("Enter order id");
            string orderId = Console.ReadLine();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand($"select o.OrderId,c.ContactName,p.ProductName from orders as o join customers as c on c.CustomerID = o.CustomerID join [Order Details] as od on od.OrderID = o.OrderID join Products as p on p.ProductID = od.ProductID where o.OrderID = @orderID", conn);
            adapter.SelectCommand.Parameters.AddWithValue("@orderId", orderId);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                Console.WriteLine($"The current connection state is {conn.State}");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"OrderID: {row["OrderID"]}");
                    Console.WriteLine($"ContactName: {row["ContactName"]}");
                    Console.WriteLine($"ProductName: {row["ProductName"]}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
