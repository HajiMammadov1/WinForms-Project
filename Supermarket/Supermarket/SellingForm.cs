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
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Connection.Open();
            string query = "select ProductName, ProductPrice from ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void populateBills()
        {
            Connection.Open();
            string query = "select * from BillTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBills();
            fillComboBox();
            SellerNameLabel.Text = Form1.SellerName;
        }
        
        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProductDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductDGV1.SelectedRows == null || ProductDGV1.SelectedRows.Count == 0)
                return;

            if (ProductDGV1.SelectedCells.Count > 0)
            {
                ProdName.Text = ProductDGV1.SelectedRows[0].Cells[1].Value.ToString();
                ProdPrice.Text = ProductDGV1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelabel.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (BillId.Text == "")
            {
                MessageBox.Show("Missing Bill id");
            }
            else
            {
                try
                {
                    Connection.Open();
                    string query = "insert into BillTable values(" + BillId.Text + ", '" + SellerNameLabel.Text + "', '" + Datelabel.Text + "', " + AmountLabel.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order added successfully");
                    Connection.Close();

                    populateBills();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        int grandTotal = 0, n = 0;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("SuperMarket", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID: " + BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
            e.Graphics.DrawString("Seller Name: " + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Date: " + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount: " + BillsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("Thanks for choosing us", new Font("Century Gothic", 20, FontStyle.Italic), Brushes.Red, new Point(270, 230));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoryComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "select ProductName, ProductPrice from ProductTable where ProductCat='" + CategoryComboBox2.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void fillComboBox()
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

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(OrderDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = ProdQty.Text;
                newRow.Cells[4].Value = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);

                n++;

                OrderDGV.Rows.Add(newRow);

                grandTotal = grandTotal + total;
                AmountLabel.Text = ""+grandTotal;
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void CategoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
