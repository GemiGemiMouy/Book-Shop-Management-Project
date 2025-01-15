using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookProject
{
    public partial class BookForm : Form
    {

        private readonly Database database;
        public BookForm()
        {
            InitializeComponent();   
            database= new Database();

        }
        private void BookForm_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadBook();
            DisplayTotalBook();
        }

        public void LoadBook()
        {
            Book book = new Book();
            DataTable dt = book.SearchBook("");
            dataGridViewBooks.DataSource = dt;


        }

        private void DisplayTotalBook()
        {
            try
            {
                Book book = new Book();
                DataTable dt = book.SearchBook(""); // You can filter if needed

                int totalStock = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalStock += Convert.ToInt32(row["Quantity"]);
                }

                lblTotalStock.Text = $"Total Books: {totalStock}"; // Display total stock in a label
                LoadBook();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while calculating total stock: " + ex.Message);
            }
            /* try
             {
                 using (SqlConnection conn = database.GetConnection())
                 {
                     string query = "SELECT ISNULL(SUM(QuantityAvailable), 0) AS TotalStock FROM Stock";
                     SqlCommand cmd = new SqlCommand(query, conn);
                     conn.Open();
                     object result = cmd.ExecuteScalar();
                     int totalStock = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                     lblTotalStock.Text = "Total Stock: " + totalStock.ToString();
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error calculating total stock: " + ex.Message);
             }*/
        }
        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cboCategory.Items.Add("Programming");
            cboCategory.Items.Add("Fiction");
            cboCategory.Items.Add("Non-Fiction");
            cboCategory.Items.Add("Science");
            cboCategory.Items.Add("Technology");
            cboCategory.Items.Add("History");
            cboCategory.Items.Add("Song");
            cboCategory.Items.Add("Art");
            cboCategory.Items.Add("Drama");
            cboCategory.Items.Add("Classic");
            cboCategory.Items.Add("Fantasy");
            cboCategory.Items.Add("Adventure");

            // Set a default value if needed
            cboCategory.SelectedIndex = 0;
        }

        private void ClearFields()
        {
            txtBookId.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            cboCategory.SelectedIndex = 0;
            txtPrice.Clear();
            txtQuantity.Clear();
            txtSearchbook.Clear();
        }
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = new Book
                {
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    Category = cboCategory.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text)
                };
                book.AddBook();
                MessageBox.Show("Book added successfully.");
                LoadBook();
                ClearFields();
                DisplayTotalBook();
            }catch(Exception ex)
            {
                MessageBox.Show("Error"+ex.Message);
            }
        }

      


        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBookId.Text))
                {
                    MessageBox.Show("Please select a book to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtBookId.Text, out int bookID))
                {
                    MessageBox.Show("BookID must be a valid number.");
                    return;
                }

                Book book = new Book
                {
                    BookID = bookID,
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    Category = cboCategory.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text)
                };

                book.UpdateBook();
                MessageBox.Show("Book updated successfully.");
                LoadBook(); // Refresh the DataGridView
                ClearFields(); // Clear input fields
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            /*try
            {
                int bookID;

                // Determine the BookID source: from textbox or selected row
                if (!string.IsNullOrWhiteSpace(txtBookId.Text))
                {
                    // Parse BookID from the textbox
                    if (!int.TryParse(txtBookId.Text, out bookID))
                    {
                        MessageBox.Show("BookID must be a valid number.");
                        return;
                    }
                }
                else if (dataGridViewBooks.CurrentRow != null)
                {
                    // Get BookID from the selected row in the DataGridView
                    bookID = Convert.ToInt32(dataGridViewBooks.CurrentRow.Cells["BookID"].Value);
                }
                else
                {
                    MessageBox.Show("Please enter a BookID in the textbox or select a row in the table.");
                    return;
                }

                // Validate that the rest of the fields are filled
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAuthor.Text) ||
                    string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtQuantity.Text))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }

                // Create a Book object with values from the form
                Book book = new Book
                {
                    BookID = bookID, // Use the BookID determined above
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    Category = cboCategory.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text)
                };

                // Call the UpdateBook method
                book.UpdateBook();

                MessageBox.Show("Book updated successfully.");

                // Refresh the DataGridView with updated data
                LoadBook();

                // Clear form fields
                ClearFields();
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific errors
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                // General error handling
                MessageBox.Show("Error while updating book: " + ex.Message);
            }*/
        }

        
       

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewBooks.Rows[e.RowIndex];
                txtBookId.Text= row.Cells["BookID"].Value.ToString();
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtAuthor.Text = row.Cells["Author"].Value.ToString();
                cboCategory.Text = row.Cells["Category"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            try
            {
                int bookID;

                // Determine the BookID source: from textbox or selected row
                if (!string.IsNullOrWhiteSpace(txtBookId.Text))
                {
                    // Parse BookID from the textbox
                    if (!int.TryParse(txtBookId.Text, out bookID))
                    {
                        MessageBox.Show("BookID must be a valid number.");
                        return;
                    }
                }
                else if (dataGridViewBooks.CurrentRow != null)
                {
                    // Get BookID from the selected row in the DataGridView
                    bookID = Convert.ToInt32(dataGridViewBooks.CurrentRow.Cells["BookID"].Value);
                }
                else
                {
                    MessageBox.Show("Please enter a BookID in the textbox or select a row in the table.");
                    return;
                }

                // Confirm deletion
                DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this book?",
                                                             "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Create the Book object and assign the BookID
                    Book book = new Book { BookID = bookID };

                    // Call the DeleteBook method
                    book.DeleteBook();

                    MessageBox.Show("Book deleted successfully.");

                    // Refresh the DataGridView to show updated book list
                    LoadBook();

                    // Clear the input fields
                    ClearFields();
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific errors
                if (sqlEx.Number == 547) // Foreign Key constraint violation
                {
                    MessageBox.Show("Cannot delete this book because it has associated orders.");
                }
                else
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
            }
            catch (Exception ex)
            {
                // General error handling
                MessageBox.Show("Error while deleting book: " + ex.Message);
            }
        }
    

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = new Book();
                DataTable dt = book.SearchBook(txtSearchbook.Text);
                dataGridViewBooks.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
