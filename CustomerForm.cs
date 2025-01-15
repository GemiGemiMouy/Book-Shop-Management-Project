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
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtSearchCustomer.Clear();
        }
        private void LoadCustomerData()
        {
            Customer customer = new Customer();
            DataTable dt = customer.SearchCustomer("");
            dataGridViewCustomers.DataSource = dt;
        }

        private void btnAddCustomer_Click_1(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer
                {
                    CustomerName = txtCustomerName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };
                customer.AddCustomer();
                MessageBox.Show("Customer added successfully.");
                LoadCustomerData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure all fields are filled
                if (!string.IsNullOrWhiteSpace(txtCustomerName.Text) &&
                    !string.IsNullOrWhiteSpace(txtPhone.Text) &&
                    !string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    int customerId;

                    // Get CustomerID from TextBox or DataGridView
                    if (!string.IsNullOrWhiteSpace(txtCustomerId.Text) && int.TryParse(txtCustomerId.Text, out customerId))
                    {
                        // Use CustomerID from TextBox
                    }
                    else if (dataGridViewCustomers.CurrentRow != null &&
                             int.TryParse(dataGridViewCustomers.CurrentRow.Cells["CustomerID"].Value.ToString(), out customerId))
                    {
                        // Use CustomerID from DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid Customer ID or select a row in the table.");
                        return;
                    }

                    // Create a Customer object with updated details
                    Customer customer = new Customer
                    {
                        CustomerID = customerId,
                        CustomerName = txtCustomerName.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text
                    };

                    // Call the UpdateCustomer method
                    customer.UpdateCustomer();

                    // Show success message
                    MessageBox.Show("Customer updated successfully.");

                    // Refresh the DataGridView with the updated data
                    LoadCustomerData();

                    // Clear the input fields
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("All fields are required.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId;

                // Get CustomerID from TextBox or DataGridView
                if (!string.IsNullOrWhiteSpace(txtCustomerId.Text) && int.TryParse(txtCustomerId.Text, out customerId))
                {
                    // Use CustomerID from TextBox
                }
                else if (dataGridViewCustomers.CurrentRow != null &&
                         int.TryParse(dataGridViewCustomers.CurrentRow.Cells["CustomerID"].Value.ToString(), out customerId))
                {
                    // Use CustomerID from DataGridView
                }
                else
                {
                    MessageBox.Show("Please enter a valid Customer ID or select a row in the table.");
                    return;
                }

                // Confirm before deleting
                DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this customer?",
                                                             "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Create a Customer object and set the ID
                    Customer customer = new Customer { CustomerID = customerId };

                    // Call the DeleteCustomer method
                    customer.DeleteCustomer();

                    // Show success message
                    MessageBox.Show("Customer deleted successfully.");

                    // Refresh the DataGridView with the updated data
                    LoadCustomerData();

                    // Clear the input fields
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dataGridViewCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewCustomers.Rows[e.RowIndex];
                txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer();
                DataTable dt = customer.SearchCustomer(txtSearchCustomer.Text);
                dataGridViewCustomers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

       
    }
}
