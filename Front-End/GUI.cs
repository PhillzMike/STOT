using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using System.IO;
using Engine;
using System.Diagnostics;

namespace Front_End {
    /// <summary>
    /// Implement Crawler at intervals and other classes
    /// Author Seyi
    /// Google Looking Gui...DO IT
    /// </summary>
    /// <seealso cref="MaterialSkin.Controls.MaterialForm" />
    public partial class UNILAG:Form {
        HomePage Home ;
        SearchPage Search;
       
        public UNILAG() {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager = MaterialSkinManager.Instance;
            //skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Green300, Primary.Green400, Primary.Green400, Accent.Blue200, TextShade.WHITE);

            this.IsMdiContainer = true;
           
            LoadSearchPage();
            LoadHomePage();
            Search.BringToFront();
            Search.Homepage = Home;
            Home.Searchpage = Search;
        }
        

        public void LoadHomePage()
        {
            Home = new HomePage() {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43,
                Location = new Point(0, 0)
                
            };
            Home.Show();
        }

        public void LoadSearchPage()
        {
            Search = new SearchPage() {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43,
                 Location = new Point(0, 0)

            };
            Search.Show();
        }

        private void UNILAG_SizeChanged(object sender,EventArgs e) {
            Home.Width = this.Width - 20;
            Home.Height = this.Height -43;
            Search.Width = this.Width - 20;
            Search.Height = this.Height -43;
        }
    }
}
