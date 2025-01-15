using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookProject
{
    internal class Book
    {
        private readonly Database database = new Database();

      
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public DataTable GetAllBooks()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT BookID, Title FROM Books", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public decimal GetPrice(int bookID)
        {
            using (SqlConnection conn =database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT Price FROM Books WHERE BookID = @BookID", conn);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                conn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        public void AddBook()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddBooks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Author", Author);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*  public void UpdateBook()
          {
              using (SqlConnection conn = database.GetConnection())
              {
                  SqlCommand cmd = new SqlCommand("UpdateBooks", conn);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@BookID", BookID);  // BookID passed from form
                  cmd.Parameters.AddWithValue("@Title", Title);
                  cmd.Parameters.AddWithValue("@Author", Author);
                  cmd.Parameters.AddWithValue("@Category", Category);
                  cmd.Parameters.AddWithValue("@Price", Price);
                  cmd.Parameters.AddWithValue("@Quantity", Quantity);
                  conn.Open();
                  cmd.ExecuteNonQuery();
              }
          }
        */

        public void UpdateBook()
        {
            try
            {
                using (SqlConnection conn = database.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UpdateBooks", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID", BookID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Author", Author);
                    cmd.Parameters.AddWithValue("@Category", Category);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were updated. Please check if the BookID exists.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        // Method to delete a book from the database
        public void DeleteBook()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteBooks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookID", BookID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable SearchBook(string searchTerm)
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SearchBooks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

    }
}
