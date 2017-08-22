using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Windows.Forms;

namespace Front_End
{
    public partial class SearchPage : Form
    {
        Inverter invt;
        public SearchPage(string ray)
        {
            
            InitializeComponent();
           
            TxtSearch1.Text = ray;
        }

        private void BtnClick_Click(object sender,EventArgs e) {
            listView1.Items.Clear();
            foreach(Document x in Querier.Search(TxtSearch1.Text,invt)) {
                listView1.Items.Add(new ListViewItem().Text = x.Name);
            }
          //  this.Close();
            (this.MdiParent as UNILAG).LoadHomePage();
        }

        private void TxtSearch1_TextChanged(object sender,EventArgs e) {
          
        }

        private void SearchPage_Load(object sender,EventArgs e) {
            invt = (this.MdiParent as UNILAG).invt;
        }
    }
}
