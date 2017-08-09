﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Front_End
{
    public partial class SearchPage : Form
    {
        public SearchPage(string ray)
        {
            InitializeComponent();
            TxtSearch1.Text = ray;
        }

        private void BtnClick_Click(object sender,EventArgs e) {
            this.Close();
            (this.MdiParent as UNILAG).LoadHomePage();
        }
    }
}