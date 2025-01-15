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
    public partial class OrderForm : Form
    {
        private Customer customer = new Customer();
        private Book book = new Book();
        public OrderForm()
        {
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadBookList();
            LoadCustomerList();
            LoadOrderData();
        }
        private void LoadCustomerList()
        {
            cboCustomer.DataSource = customer.GetAllCustomers();
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "CustomerID";
        }

        private void LoadBookList()
        {
            cboBook.DataSource = book.GetAllBooks();
            cboBook.DisplayMember = "Title";
            cboBook.ValueMember = "BookID";
        }

        private void LoadOrderData()
        {
            Orders order = new Orders();
            dataGridViewaOrders.DataSource = order.GetAllOrders();
        }

        private void CalculateTotalPrice()
        {
            if (cboBook.SelectedValue != null && int.TryParse(txtQuantityOrders.Text, out int quantity))
            {
                decimal price = book.GetPrice(Convert.ToInt32(cboBook.SelectedValue));
                txtTotalPrice.Text = (price * quantity).ToString("0.00");
            }
        }

        private void ClearFields()
        {
            cboCustomer.SelectedIndex = -1;
            cboBook.SelectedIndex = -1;
            txtQuantityOrders.Clear();
            txtTotalPrice.Clear();
        }

        private void btnAddOrders_Click(object sender, EventArgs e)
        {
            try
            {
                Orders order = new Orders
                {
                    CustomerID = Convert.ToInt32(cboCustomer.SelectedValue),
                    BookID = Convert.ToInt32(cboBook.SelectedValue),
                    OrderDate = dateTimeOrder.Value,
                    QuantityOrdered = int.Parse(txtQuantityOrders.Text),
                    TotalPrice = decimal.Parse(txtTotalPrice.Text),  // Ensure TotalPrice is calculated
                };

                order.AddOrder();
                MessageBox.Show("Order added successfully.");
                LoadOrderData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtQuantityOrders_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }
    }
}
