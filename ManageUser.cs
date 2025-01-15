using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookProject
{
    public partial class ManageUser : Form
    {
        public ManageUser()
        {
            InitializeComponent();
        }
        private void LoadUsers()
        {
            User user = new User();
            DataTable dt = user.SearchUsers("");
            dataGridViewUser.DataSource = dt;
        }
        private void ManageUser_Load(object sender, EventArgs e)
        {
            LoadRole();
            LoadUsers();
        }
        private void LoadRole()
        {
            cboUserRole.Items.Clear();
            cboUserRole.Items.Add("Admin");
            cboUserRole.Items.Add("Staff");

            cboUserRole.SelectedIndex = 0;
        }
        private void ClearFields()
        {
            txtManageUsername.Clear();
            txtSearchUser.Clear();
            txtUserPassword.Clear();
            cboUserRole.SelectedIndex = -1;

        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User
                {
                    Username = txtManageUsername.Text,
                    Password = txtUserPassword.Text,
                    Role = cboUserRole.Text
                };
                user.AddUser();
                MessageBox.Show("Added user Successfully!");
                LoadUsers();
                ClearFields();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
             try
             {
                 // Make sure that txtUserID contains a valid UserID
                 int userId = int.Parse(txtUserID.Text);
                 if (userId <= 0)
                 {
                     MessageBox.Show("Please select a valid user to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }

                 // Check that the required fields (Username, Password, Role) are not empty
                 if (string.IsNullOrEmpty(txtManageUsername.Text) || string.IsNullOrEmpty(txtUserPassword.Text) || cboUserRole.SelectedIndex == -1)
                 {
                     MessageBox.Show("Please fill in all the fields before updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }

                 // Create the user object and populate it with updated information
                 User user = new User
                 {
                     UserID = userId,
                     Username = txtManageUsername.Text,
                     Password = txtUserPassword.Text,
                     Role = cboUserRole.Text
                 };

                 // Call the UpdateUser method from the User class to update the user's data in the database
                 user.UpdateUser();

                 // Show a success message
                 MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 // Refresh the user grid and clear the input fields
                 LoadUsers();
                 ClearFields();
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(txtUserID.Text);  // Assuming txtUserID contains the UserID to delete

                User user = new User
                {
                    UserID = userId
                };

                user.DeleteUser();  // Delete the user from the database

                MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh grid and clear fields
                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            try
            {


                User user = new User();
                DataTable dt = user.SearchUsers(txtSearchUser.Text);
                dataGridViewUser.DataSource = dt;
            }catch(Exception ex)
            {
                MessageBox.Show("Error!" + ex);
            }
        }

        private void dataGridViewUser_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Ensure a row is selected
            {
                // Get the selected row
                DataGridViewRow row = dataGridViewUser.Rows[e.RowIndex];

                // Populate the form fields with the data from the selected row
                txtUserID.Text = row.Cells["UserID"].Value.ToString();
                txtManageUsername.Text = row.Cells["Username"].Value.ToString();
                txtUserPassword.Text = row.Cells["Password"].Value.ToString();
                cboUserRole.SelectedItem = row.Cells["Role"].Value.ToString();
            }
        }
    }
}
