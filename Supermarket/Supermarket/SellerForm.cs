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

namespace Supermarket
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Connection.Open();
            string query = "select * from SellerTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0)
                return;

            if (dataGridView1.SelectedCells.Count > 0)
            {
                SellerId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                SellerName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                SellerAge.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                SellerPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                SellerPassword.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Open();
                string query = "insert into SellerTable values(" + SellerId.Text + ", '" + SellerName.Text + "', '" + SellerAge.Text + "', '" + SellerPhone.Text + "', '" + SellerPassword.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category added successfully");
                Connection.Close();

                populate();

                SellerId.Text = "";
                SellerName.Text = "";
                SellerAge.Text = "";
                SellerPhone.Text = "";
                SellerPassword.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellerId.Text == "" || SellerName.Text == "" || SellerAge.Text == "" || SellerPhone.Text == "" || SellerPassword.Text == "")
                {
                    MessageBox.Show("Please select seller to edit");
                }
                else
                {
                    Connection.Open();
                    string query = "update SellerTable set SellerName='" + SellerName.Text + "', SellerAge='" + SellerAge.Text + "', SellerPhone='" + SellerPhone.Text + "', SellerPassword='" + SellerPassword.Text + "' where SellerId=" + SellerId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller updated successfully");
                    Connection.Close();

                    populate();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellerId.Text == "")
                {
                    MessageBox.Show("Select the seller to delete");
                }
                else
                {
                    Connection.Open();
                    string query = "delete from SellerTable where SellerId=" + SellerId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully");
                    Connection.Close();

                    populate();

                    SellerId.Text = "";
                    SellerName.Text = "";
                    SellerAge.Text = "";
                    SellerPhone.Text = "";
                    SellerPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
