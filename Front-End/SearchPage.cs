using System;
using System.Collections.Generic;
using Engine;
using System.Windows.Forms;
using System.Diagnostics;

namespace Front_End
{
    
    public partial class SearchPage : Form
    {
        public HomePage Homepage { get; set; }
        string searchTerm;
        List<List<Document>> searchresults;
        //TODO change
        int DocsPerPage=6;
        private Stopwatch sw = new Stopwatch();
        
        public SearchPage()
        {
            InitializeComponent();
        }
        public string SearchTxt { set { TxtSearch1.Text = value; TxtSearch1.SelectionStart = TxtSearch1.Text.Length; } }
        public void OpenPage(){
            if (!(TxtSearch1.Text == ""))
                Search();
        }
        
        private List<List<Document>> DivideIntoPages(List<Document> fullList)
        {
            List<List<Document>> ret = new List<List<Document>>();
            List<Document> page=new List<Document>();
            foreach (Document x in fullList)
            {
                page.Add(x);
                if (page.Count == DocsPerPage)
                {
                    ret.Add(page);
                    page = new List<Document>();
                }

            }
            if (page.Count > 0)
                ret.Add(page);
            return ret;
        }

        private void BtnClick_Click(object sender, EventArgs e) {
            Search();
            if ((TxtSearch1.Text.Equals("")) || searchresults.Count == 0)
            {
                Homepage.SearchTxt = TxtSearch1.Text;
                Homepage.BringToFront();
            }
        }
        private void Search() { 
            ResultsWindow.Controls.Clear();
            Pages.Controls.Clear();
            sw.Start();
            searchTerm = TxtSearch1.Text;
            List<Document> searchresultlist = Querier.Search(TxtSearch1.Text);
            searchresults = DivideIntoPages(searchresultlist);
             SearchTime.Text = ((double)sw.ElapsedMilliseconds / 1000) + "";
            sw.Reset();
            if (searchresults.Count > 0){
                foreach (Document x in searchresults[0]) {
                    ResultsWindow.Controls.Add(new ResultCard(x,TxtSearch1.Text));
                }
            }
            if (searchresults.Count > 1)
            {
                for(int i = 1; i <= searchresults.Count ; i++)
                {
                    Button num = new Button
                    {
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                        Size = new System.Drawing.Size(30, 30),
                        Text = i+"",
                        UseVisualStyleBackColor = true,
                        //Click += new EventHandler(PageNumber)
                    };
                    num.Click += new EventHandler(PageNumber);
                    Pages.Controls.Add(num);
                }
            }
            
          //  this.Close();
         //   (this.MdiParent as UNILAG).LoadHomePage();
        }

        private void TxtSearch1_TextChanged(object sender,EventArgs e) {
            //TODO Autocorrect
            KeyPressTimer.Stop();
            KeyPressTimer.Start();
        }

        private void SearchPage_Load(object sender,EventArgs e) {
            Location = new System.Drawing.Point(0, 0);
        }

        private void PageNumber(object sender, EventArgs e)
        {
            //Search();
            ResultsWindow.Controls.Clear();
            int.TryParse((sender as Button).Text, out int pageIndex);
            foreach (Document x in searchresults[pageIndex - 1])
            {
                ResultsWindow.Controls.Add(new ResultCard(x,searchTerm));
            }
        }
        

        private void TxtSearch1_KeyPress(object sender, KeyPressEventArgs e){
            if (e.KeyChar == 13) {
                Search();
            }
        }

        private void KeyPressTimer_Tick(object sender, EventArgs e)
        {
            Search();
            if ((TxtSearch1.Text.Equals("")) || searchresults.Count == 0)
            {
                Homepage.SearchTxt = TxtSearch1.Text;
                Homepage.BringToFront();
            }
            KeyPressTimer.Stop();
        }
    }
   
}

