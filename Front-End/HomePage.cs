using Engine;
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
        public string SearchTxt { set { TxtSearch.Text = value; TxtSearch.SelectionStart = TxtSearch.Text.Length; } }
        public SearchPage Searchpage { get; set; }
        public HomePage()
        {
            InitializeComponent();
        }
        private void HomePage_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = tree;
            tree.ListChanged += new ListChangedEventHandler(ItemSizeChanged);
            Location = new System.Drawing.Point(0, 0);
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
            if (!OpenSearchPage()) { }
            //tree.Add("cdrghrghirtjjbrgigijg"+i++);
        }
        private bool OpenSearchPage() {
            if ((!TxtSearch.Text.Equals(""))&&Querier.Search(TxtSearch.Text).Count > 0)
            {
                Searchpage.SearchTxt = TxtSearch.Text;
                Searchpage.BringToFront();
                
                return true;
            }
            else
            {
                return false;
            }
               
        }
        private void ListBox1_ItemSelected(object sender, EventArgs e)
        {

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
            OpenSearchPage();
            /*  foreach (string it in Engine.Querier.AutoComplete(TxtSearch.Text))
                  tree.Add(it);*/
            //Below Lines will open the search page the moment Querier.Search starts returning search Results
           


        }
        
    }
}
