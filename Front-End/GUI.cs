using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using System.IO;
using Engine;
using System.Threading;
using System.Diagnostics;

namespace Front_End {
    /// <summary>
    /// Implement Crawler at intervals and other classes
    /// Author Seyi
    /// Google Looking Gui...DO IT
    /// </summary>
    /// <seealso cref="MaterialSkin.Controls.MaterialForm" />
    public partial class UNILAG:Form {
        private Inverter invt;
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
            try {
                invt = Inverter.Load("Inverter.invt");
                Inverter.LogMovement("^^^^^^^^^Succesfully loaded previous Inverter: ");
            } catch(Exception ex)  {
                Inverter.LogMovement("!!!!!!!!!Error loading previous Inverter: " + ex.Message);
                invt = new Inverter("../../../Resources/stopwords.txt","../../../Resources/Dictionary.txt"
                              ,"../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
                Updater.Crawler("../../../Resources/Mock", invt);
                   invt.SaveThis("Tester");
            }
            //Set Querier
            Querier.Invt = invt;
            LoadSearchPage();
            // Search.Hide();


            //Crawler
            Thread a = new Thread(new ThreadStart(UpdateInverter));
            a.Start();

            LoadHomePage();
            Search.BringToFront();
            Search.Homepage = Home;
            Home.Searchpage = Search;
        }
        private void UpdateInverter() {
            Stopwatch sw = new Stopwatch();
            while (true) {
                sw.Restart();
                Updater.Crawler("../../../Resources/Mock", invt);
                invt.SaveThis("Inverter.invt.temp");
                File.Delete("Inverter.invt");
                File.Move("Inverter.invt.temp", "Inverter.invt");
                Inverter.LogMovement("lastSeen");
                //TODO remove last lastseen
                //Wait one minute
                while (sw.ElapsedMilliseconds < 60000) { }
            }
        }
        private void UNILAG_Load(object sender, EventArgs e)
        {

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
            Search.invt = invt;
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
