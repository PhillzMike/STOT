using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using System.IO;
using Engine;

namespace Front_End {
    /// <summary>
    /// Implement Crawler at intervals and other classes
    /// Author Seyi
    /// Google Looking Gui...DO IT
    /// </summary>
    /// <seealso cref="MaterialSkin.Controls.MaterialForm" />
    public partial class UNILAG:Form {
        public Inverter invt;
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
            //LoadHomePage();
            // invt = new Inverter("../../../Resources/stopwords.txt","../../../Resources/Dictionary.txt"
            //                  ,"../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
            //  invt = Engine.Inverter.Load("Tester");
            //   Updater.Crawler("../../../Resources/Mock",invt);
            //   invt.SaveThis("Tester");
            LoadSearchPage("");
            LoadHomePage();
            Home.Close();
        }

        private void UNILAG_Load(object sender, EventArgs e)
        {

        }

        public void LoadHomePage()
        {
            Home = new HomePage() {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43
            };
            Home.Show();
        }

        public void LoadSearchPage(string jello)
        {
            Search = new SearchPage(jello) {
                MdiParent = this,
                Width = this.Width - 20,
                Height = this.Height - 43
            };
            Search.Show();
        }

        private void UNILAG_SizeChanged(object sender,EventArgs e) {
            Home.Width = this.Width - 20;
            Home.Height = this.Height - 43;
            Search.Width = this.Width - 20;
            Search.Height = this.Height - 43;
        }
    }
}
