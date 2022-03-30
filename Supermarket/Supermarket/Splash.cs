﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        int startPoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 1;
            progressBar.Value = startPoint;

            if (progressBar.Value == 100)
            {
                progressBar.Value = 0;
                timer1.Stop();

                Form1 log = new Form1();
                this.Hide();
                log.Show();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
    }
}
