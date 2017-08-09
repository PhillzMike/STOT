using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Engine;

namespace Front_End
{
    public partial class HomePage : Form
    {
        BindingList<string> tree = new BindingList<string>();
        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = tree;
            tree.ListChanged += new ListChangedEventHandler(ItemSizeChanged);           
        }

        private void ItemSizeChanged(object sender,ListChangedEventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                listBox1.Visible = false;
                listBox1.Height = 0;

            }
            else {
                listBox1.Height = (28+(24*(listBox1.Items.Count-1)));
                listBox1.Visible = true;
            }
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            (this.MdiParent as UNILAG).LoadSearchPage(TxtSearch.Text);
            this.Close();
            //tree.Add("cdrghrghirtjjbrgigijg"+i++);
        }

        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            if(tree.Count > 0)
            tree.RemoveAt(tree.Count-1);
        }

        private void ListBox1_ItemSelected(object sender, EventArgs e)
        {
            TxtSearch.Text = listBox1.SelectedItem.ToString();
            (this.MdiParent as UNILAG).LoadSearchPage(TxtSearch.Text);
            this.Close();
        }

        private void ListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ListBox1_ItemSelected(sender, e);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {            
            tree.Clear();
            /*  foreach (string it in Engine.Querier.AutoComplete(TxtSearch.Text))
                  tree.Add(it);*/
            //Below Lines will open the search page the moment Querier.Search starts returning search Results
           /* if(Querier.Search(TxtSearch.Text).Count > 0) {
                (this.MdiParent as UNILAG).LoadSearchPage(TxtSearch.Text);
                this.Close();
                
            };*/


        }

       
    }
}
