using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookProject
{
    internal class Orders
    {
        private Database database = new Database();

        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal TotalPrice { get; set; }

        public void AddOrder()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookID", BookID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                cmd.Parameters.AddWithValue("@QuantityOrdered", QuantityOrdered);
                cmd.Parameters.AddWithValue("@TotalPrice", TotalPrice);  // Pass TotalPrice parameter
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllOrders()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
