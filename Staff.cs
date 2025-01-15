using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookProject
{
    internal class Staff
    {
        private readonly Database database = new Database();

        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }

        public void AddStaff()
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddStaff", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffName", StaffName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Age", Age);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStaff()
        {
            using (SqlConnection connection = database.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateStaff", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StaffID", StaffID);
                    command.Parameters.AddWithValue("@StaffName", StaffName);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Age", Age);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Failed to update staff.", ex);
                }
            }
        }

        // Delete Staff
        public void DeleteStaff()
        {
            using (SqlConnection connection = database.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteStaffs", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StaffID", StaffID);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Failed to delete staff.", ex);
                }
            }
        }

            public DataTable SearchStaffs(string searchTerm)
        {
            using (SqlConnection conn = database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SearchStaffs", conn);
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


