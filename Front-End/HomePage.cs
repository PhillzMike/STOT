using Engine;
using System;
using System.ComponentModel;
using System.Windows.Forms;
//using Engine;

namespace Front_End {
    public partial class HomePage : Form {
        public HomePage() {
            InitializeComponent();
        }
        BindingList<string> SuggestionsList = new BindingList<string>();
        public string SearchTxt { set { TxtSearch.Text = value; TxtSearch.SelectionStart = TxtSearch.Text.Length; } }
        public SearchPage Searchpage { get; set; }

        private void HomePage_Load(object sender, EventArgs e) {
            listBox1.DataSource = SuggestionsList;
            SuggestionsList.ListChanged += new ListChangedEventHandler(ItemSizeChanged);
            Location = new System.Drawing.Point(0, 0);
        }
        private void ItemSizeChanged(object sender, EventArgs e) {
            if (listBox1.Items.Count == 0) {
                listBox1.Visible = false;
                listBox1.Height = 0;
            } else {
                listBox1.Height = (28 + (24 * (listBox1.Items.Count - 1)));
                listBox1.Visible = true;
            }
        }
        private void MaterialRaisedButton1_Click(object sender, EventArgs e) {
            if (!OpenSearchPage()) { }
        }
        private bool OpenSearchPage() {
            if ((!TxtSearch.Text.Equals("")) && Querier.Search(TxtSearch.Text).Count > 0) {
                Searchpage.SearchTxt = TxtSearch.Text;
                Searchpage.BringToFront();

                return true;
            } else {
                return false;
            }

        }
        private void ListBox1_ItemSelected(object sender, EventArgs e) {
            TxtSearch.Text = listBox1.SelectedItem.ToString();
        }
        private void ListBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                ListBox1_ItemSelected(sender, e);
            }
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e) {
            SuggestionsList.Clear();
            foreach (string s in Querier.AutoComplete(TxtSearch.Text, 4))
                SuggestionsList.Add(s);
            OpenSearchPage();

        }

    }
}
