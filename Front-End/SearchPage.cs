using System;
using System.Collections.Generic;
using Engine;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

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
    public class ResultCard : TableLayoutPanel
    {
        public ResultCard(Document x,String query)
        {
            LinkLabel title = new LinkLabel
            {
                AutoSize = true,
                Text = "[" + x.Type + "] " + x.Name,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            };
            title.Click += (o, i) =>
             {
                 try
                 {
                     Process.Start(x.Address);
                 }
                 catch
                 {

                 }
             };
            Label adress = new Label
            {
                AutoSize=true,
                Text = x.LastModified.ToString() + " " + x.Address,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            };
            LblBoldWords Relevance = new LblBoldWords(query)
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = "Document Relevance At present it is not too much to say that it is of such weight it may have an influence upon European history. I promise,said Holmes. And I.You will excuse this mask,continued our strange visitor. The august person who employs me wishes his agent to be unknown to you,and I may confess at once that the title by which I have just called myself is not exactly my own.I was aware of it,said Holmes dryly.You are sure that she has not sent it yet?I love and am loved by a better man than he.The King may do what he will without hindrance from one whom he has cruelly wronged.I keep it only to safeguard myself, and to preserve a weapon which will always secure me from any steps which he might take in the future. I leave a photograph which he might care to possess; and I remain, dear Mr. Sherlock Holmes,Very truly yours,IRENE NORTON, nee ADLER.hat a woman--oh, what a woman! cried the King of Bohemia, when we had all three read this epistle. Did I not tell you how quick and resolute she was? Would she not have made an admirable queen? Is it not a pity that she was not on my level?From what I have seen of the lady, she seems, indeed, to be on a very different level to your Majesty, said Holmes coldly. I am sorry that I have not been able to bring your Majesty's business to a more successful conclusion.On the contrary, my dear sir, cried the King; nothing could be more successful. I know that her"
                };
            ColumnCount = 1;
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Controls.Add(title, 0, 0);
            Controls.Add(adress, 0, 1);
            Controls.Add(Relevance, 0, 2);
            Name = x.Address;
            Padding = new System.Windows.Forms.Padding(2);
            RowCount = 3;
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.36364F));
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.63636F));
            RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            Size = new System.Drawing.Size(1534, 107);
        }
    }
    public class LblBoldWords : Label {
        HashSet<String> bolded;
        public LblBoldWords(String bold) {
            this.bolded = new HashSet<string>();
            bold = bold.ToLower();
            String[] bolded = bold.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            this.bolded.UnionWith(bolded);
        }
        protected override void OnPaint(PaintEventArgs e) {
            Point drawPoint = new Point(0, 0);
            String[] relevantWords = Text.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            //Fonts
            Font normalFont = this.Font;
            Font boldFont = new Font(normalFont, FontStyle.Bold);
            foreach(String word in relevantWords) {
                Size boldSize = TextRenderer.MeasureText(word, boldFont);
                Size normalSize = TextRenderer.MeasureText(word, normalFont);
                if (drawPoint.X + boldSize.Width > Width) {
                    drawPoint = new Point(0, drawPoint.Y + boldSize.Height);
                }
                if (bolded.Contains(word.ToLower())) {
                    Rectangle boldRect = new Rectangle(drawPoint, boldSize);
                    drawPoint = new Point(drawPoint.X + boldRect.Width, drawPoint.Y);
                    TextRenderer.DrawText(e.Graphics, word, boldFont, boldRect, ForeColor);
                } else {
                    
                    Rectangle normalRect = new Rectangle(drawPoint,normalSize);
                    drawPoint = new Point(drawPoint.X + normalSize.Width, drawPoint.Y);
                    TextRenderer.DrawText(e.Graphics, word, normalFont, normalRect, ForeColor);
                }
                
            }
        }
    }
}
