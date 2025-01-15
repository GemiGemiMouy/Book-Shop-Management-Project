using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookProject
{
    internal class User
    {
    
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        private readonly Database database;

        public User()
        {
            database = new Database();
        }

        public void AddUser()
        {
            using (SqlConnection conn = database.GetConnection())
            {                
                    SqlCommand cmd = new SqlCommand("AddUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Role", Role);

                    conn.Open();
                    cmd.ExecuteNonQuery();                                 
            }
        }
        public void UpdateUser()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Role", Role);

                // Execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Delete a user
        public void DeleteUser()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter for UserID
                cmd.Parameters.AddWithValue("@UserID", UserID);

                // Execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Search for users
        public DataTable SearchUsers(string searchTerm)
        {
            using (SqlConnection conn = database.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SearchUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch
                {
                    return null;
                }
            }
        }

       
    }
}
