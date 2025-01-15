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
    public partial class MainDashbordForm : Form
    {
        public MainDashbordForm()
        {
            InitializeComponent();
        }
        private void LoadFormInPanel(Form form)
        {
            pnlContainer.Controls.Clear();

            // Prepare the form for embedding
            form.TopLevel = false; // Make it a child control
            form.Dock = DockStyle.Fill; // Fill the panel
            pnlContainer.Controls.Add(form); // Add to the panel
            form.Show(); // Display the form
        }
        private void button1_Click(object sender, EventArgs e)
        {
           Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            BookForm bookForm = new BookForm();
            LoadFormInPanel(bookForm);
            
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            LoadFormInPanel(customerForm);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            LoadFormInPanel(orderForm);
        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainDashbordForm_Load(object sender, EventArgs e)
        {

        }

        private void btnManageUser_Click(object sender, EventArgs e)
        {
            ManageUser manageUsersForm = new ManageUser();
            LoadFormInPanel(manageUsersForm);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            StaffForm staffForm = new StaffForm();
            LoadFormInPanel(staffForm);
        }

      
    }
}
