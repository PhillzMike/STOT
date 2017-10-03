using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;

namespace Front_End {
    /// <summary>
    /// MDI Parent, Holds HomePage and SearchPage
    /// </summary>
    /// <seealso cref="MaterialSkin.Controls.MaterialForm" />
    public partial class UNILAG : Form {
        HomePage Home;
        SearchPage Search;
        public UNILAG() {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager = MaterialSkinManager.Instance;
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Green300, Primary.Green400, Primary.Green400, Accent.Blue200, TextShade.WHITE);

            this.IsMdiContainer = true;

            LoadSearchPage();
            LoadHomePage();
            Search.BringToFront();
            Search.Homepage = Home;
            Home.Searchpage = Search;
        }
        void LoadHomePage() {
            Home = new HomePage() {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43,
                Location = new Point(0, 0)
            };
            Home.Show();
        }
        void LoadSearchPage() {
            Search = new SearchPage() {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43,
                Location = new Point(0, 0)

            };
            Search.Show();
        }
        void UNILAG_SizeChanged(object sender, EventArgs e) {
            Home.Width = this.Width - 20;
            Home.Height = this.Height - 43;
            Search.Width = this.Width - 20;
            Search.Height = this.Height - 43;
        }
    }
}
