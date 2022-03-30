using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Supermarket
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void fillComboBox()
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("select CategoryName from CategoryTable", Connection);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Load(rdr);
            CategoryComboBox.ValueMember = "CategoryName";
            CategoryComboBox.DataSource = dt;
            Connection.Close();
        }

        private void fillComboBox2()
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("select CategoryName from CategoryTable", Connection);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Load(rdr);
            CategoryComboBox2.ValueMember = "CategoryName";
            CategoryComboBox2.DataSource = dt;
            Connection.Close();
        }

        private void populate()
        {
            Connection.Open();
            string query = "select * from ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillComboBox();
            fillComboBox2();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductId.Text == "" || ProductName.Text == "" || ProductQty.Text == "" || ProductPrice.Text == "")
                {
                    MessageBox.Show("Please select product to edit");
                }
                else
                {
                    Connection.Open();
                    string query = "update ProductTable set ProductName='" + ProductName.Text + "', ProductQty='" + ProductQty.Text + "' where ProductId=" + ProductId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated successfully");
                    Connection.Close();

                    populate();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Open();
                string query = "insert into ProductTable values(" + ProductId.Text + ", '" + ProductName.Text + "', '" + ProductQty.Text + "', '" + ProductPrice.Text + "', '" + CategoryComboBox.SelectedValue.ToString() + "' )";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product added successfully");
                Connection.Close();

                populate();

                ProductId.Text = "";
                ProductName.Text = "";
                ProductQty.Text = "";
                ProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if (ProductDGV.SelectedRows == null || ProductDGV.SelectedRows.Count == 0)
                return;

            if (ProductDGV.SelectedCells.Count > 0)
            {
                ProductId.Text = ProductDGV.SelectedRows[0].Cells[0].Value.ToString();
                ProductName.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
                ProductQty.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
                ProductPrice.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductId.Text == "")
                {
                    MessageBox.Show("Select the product to delete");
                }
                else
                {
                    Connection.Open();
                    string query = "delete from ProductTable where ProductId=" + ProductId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully");
                    Connection.Close();

                    populate();

                    ProductId.Text = "";
                    ProductName.Text = "";
                    ProductQty.Text = "";
                    ProductPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "select * from ProductTable where ProductCat='" + CategoryComboBox2.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
