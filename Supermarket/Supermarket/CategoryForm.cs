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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Open();
                string query = "insert into CategoryTable values(" + CategoryIdTb.Text + ", '" + CategoryNameTb.Text + "', '" + CategoryDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category added successfully");
                Connection.Close();

                populate();

                CategoryIdTb.Text = "";
                CategoryNameTb.Text = "";
                CategoryDescTb.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populate()
        {
            Connection.Open();
            string query = "select * from CategoryTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoryDataGridView.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CategoryDataGridView.SelectedRows == null || CategoryDataGridView.SelectedRows.Count == 0)
                return;

            if (CategoryDataGridView.SelectedCells.Count > 0)
            {
                CategoryIdTb.Text = CategoryDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                CategoryNameTb.Text = CategoryDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                CategoryDescTb.Text = CategoryDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            }

            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = this.CategoryDataGridView.Rows[e.RowIndex];

            //    CategoryIdTb.Text = row.Cells["CategoryId"].Value.ToString();
            //    CategoryNameTb.Text = row.Cells["CategoryName"].Value.ToString();
            //    CategoryDescTb.Text = row.Cells["CategoryDesc"].Value.ToString();
            //    //CategoryIdTb.Text = CategoryDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            //    //CategoryNameTb.Text = CategoryDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            //    //CategoryDescTb.Text = CategoryDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryIdTb.Text == "")
                {
                    MessageBox.Show("Select the category to delete");
                }
                else
                {
                    Connection.Open();
                    string query = "delete from CategoryTable where CategoryId=" + CategoryIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted successfully");
                    Connection.Close();

                    populate();

                    CategoryIdTb.Text = "";
                    CategoryNameTb.Text = "";
                    CategoryDescTb.Text = "";
                }
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
                if (CategoryIdTb.Text == "" || CategoryNameTb.Text == "" || CategoryDescTb.Text == "")
                {
                    MessageBox.Show("Please select category to edit");
                }
                else
                {
                    Connection.Open();
                    string query = "update CategoryTable set CategoryName='" + CategoryNameTb.Text + "', CategoryDesc='" + CategoryDescTb.Text + "' where CategoryId=" + CategoryIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category updated successfully");
                    Connection.Close();

                    populate();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
            this.Hide();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
