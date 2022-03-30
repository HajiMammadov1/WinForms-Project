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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string SellerName = "";
        
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            UserNameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter the Username and Password");
            }
            else
            {
                if (RoleComboBox.SelectedIndex > -1)
                {
                    if (RoleComboBox.SelectedItem.ToString() == "Admin")
                    {
                        if (UserNameTb.Text == "Admin" && PasswordTb.Text == "Admin123")
                        {
                            ProductForm productForm = new ProductForm();
                            productForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are admin enter right username nand password");
                        }
                    }
                    else
                    {
                        Connection.Open();
                        string query = "Select count(8) from SellerTable where SellerName='" + UserNameTb.Text + "' and SellerPassword='" + PasswordTb.Text + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = UserNameTb.Text;

                            SellingForm selling = new SellingForm();
                            selling.Show();
                            this.Hide();
                            Connection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or Password");
                        }
                        Connection.Close();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Select a role");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
