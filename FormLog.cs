using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookProject
{
    public partial class FormLog : Form
    {
        public FormLog()
        {
            InitializeComponent();
        }
        private readonly Database database = new Database();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Authenticate the staff user
            string role = AuthenticateUser(username, password);

            if (role == "Staff")
            {
                // Show staff dashboard
                MainFormStaff staffDashboard = new MainFormStaff();
                staffDashboard.Show();
                this.Hide();
            }
            else if (role == "Admin")
            {
                // Redirect to admin dashboard
                MainDashbordForm adminDashboard = new MainDashbordForm();
                adminDashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password, or you do not have access rights.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /* string username = txtUsername.Text.Trim();
             string password = txtPassword.Text.Trim();

             if (AuthenticateUser(username, password))
             {
                 //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 MainDashbordForm mainDashbordForm = new MainDashbordForm();
                 mainDashbordForm.Show();
                 this.Hide();
             }
             else
             {
                 MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/
        }

        private string AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = database.GetConnection())
            {
                string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString(); // Return the role (e.g., "Staff", "Admin")
                }

                return null; // User not found
            }
        }
        /* using (SqlConnection connection = database.GetConnection())
         {
             string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@Username", username);
             cmd.Parameters.AddWithValue("@Password", password);

             connection.Open();
             int result = (int)cmd.ExecuteScalar();
             return result > 0;
         }*/
    }
    
}
