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
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }
        private void LoadStaffData()
        {

            Staff staff = new Staff();
            DataTable dt = staff.SearchStaffs("");
            dataGridViewStaff.DataSource = dt;

        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            LoadStaffData();
        }
        private void ClearFields()
        {
            txtStaffName.Clear();
            txtAddress.Clear();
            txtStaffPhone.Clear();
            txtStaffEmail.Clear();
            txtStaffUsername.Clear();
            txtAge.Clear();
            txtSearchStaff.Clear();
        }
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            try
            {
                Staff staff = new Staff
                {
                    StaffName = txtStaffName.Text,
                    Address = txtAddress.Text,
                    Phone = txtStaffPhone.Text,
                    Email = txtStaffEmail.Text,
                    Username = txtStaffUsername.Text,
                    Age = int.Parse(txtAge.Text)
                };

                staff.AddStaff();
                LoadStaffData();
                ClearFields();
                MessageBox.Show("Staff added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the Staff ID textbox is empty
                if (string.IsNullOrWhiteSpace(txtStaffID.Text))
                {
                    MessageBox.Show("Please select a staff to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a Staff object with the values from the textboxes
                Staff staff = new Staff
                {
                    StaffID = int.Parse(txtStaffID.Text),
                    StaffName = txtStaffName.Text,
                    Address = txtAddress.Text,
                    Phone = txtStaffPhone.Text,
                    Email = txtStaffEmail.Text,
                    Username = txtStaffUsername.Text,
                    Age = int.Parse(txtAge.Text)
                };

                staff.UpdateStaff();  // Update the staff
                LoadStaffData();      // Reload the staff data
                MessageBox.Show("Staff updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();        // Clear the form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the Staff ID textbox is empty
                if (string.IsNullOrWhiteSpace(txtStaffID.Text))
                {
                    MessageBox.Show("Please select a staff to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a Staff object with the ID from the textbox
                Staff staff = new Staff
                {
                    StaffID = int.Parse(txtStaffID.Text)
                };

                staff.DeleteStaff();  // Delete the staff
                LoadStaffData();      // Reload the staff data
                MessageBox.Show("Staff deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();        // Clear the form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            try
            {
                Staff staff = new Staff();
                string searchTerm = txtSearchStaff.Text;
                DataTable dt = staff.SearchStaffs(searchTerm);
                dataGridViewStaff.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a row is clicked
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewStaff.Rows[e.RowIndex];
                // Populate the textboxes with the values from the selected row
                txtStaffID.Text = row.Cells["StaffID"].Value.ToString();
                txtStaffName.Text = row.Cells["StaffName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtStaffPhone.Text = row.Cells["Phone"].Value.ToString();
                txtStaffEmail.Text = row.Cells["Email"].Value.ToString();
                txtStaffUsername.Text = row.Cells["Username"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
            }
        }

    }
}
