using Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Front_End {
    public partial class Splashscreen : Form {
        public Splashscreen() {
            InitializeComponent();
            this.CenterToScreen();
        }
        Inverter invt;
        bool skippable =false, movedOn=false;
        UNILAG gui;
        private void Splashscreen_Shown(object sender, EventArgs e) {
            Thread a = new Thread(new ThreadStart(StartUp));
            a.Start();
        }
        void ChangeLabel(String Text) {
            this.Invoke(new Action(() =>lblDetails.Text = Text));
        }
        void StartUp() {
                Thread.Sleep(300);
                ChangeLabel("Loading Database");
            try {
                //throw new Exception();
                invt = Inverter.Load("Inverter.invt");
                Inverter.LogMovement("^^^^^^^^^Succesfully loaded previous Inverter: ");
            } catch (Exception ex) {
                ChangeLabel("Loading Database failed. Creating new one");
                Inverter.LogMovement("!!!!!!!!!Error loading previous Inverter: " + ex.Message);
                invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
                                  , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string> { "../../../Resources/Sherlock.txt" });
            }
            
            ChangeLabel("Loading Interface");
            this.Invoke(new Action(() => gui = new UNILAG()));
            Querier.Invt = invt;
            //gui = new UNILAG();
            ChangeLabel("Updating Database");
            skippable = true;
            //Crawler
            Thread a = new Thread(new ThreadStart(FirstInvtUpdate));
            a.Start();
        }
        void OpenGui() {
                this.Invoke(new Action(() => {
                    if (!movedOn) {
                        movedOn = true;
                        lblDetails.Text = "Done.";
                       
                        Thread.Sleep(300);
                        gui.Show();
                        Hide();
                    }
                }));
        }
        void UpdateInverterThread() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 60000) { }
            while (true) {
                sw.Restart();
                UpdateInverter();
                Inverter.LogMovement("lastSeen");
                //Wait one minute
                while (sw.ElapsedMilliseconds < 60000) { }
            }
        }
        void UpdateInverter() {
            Updater.Crawler("../../../Resources/Mock", invt);
            invt.SaveThis("Inverter.invt.temp");
            File.Delete("Inverter.invt");
            File.Move("Inverter.invt.temp", "Inverter.invt");
        }
        void FirstInvtUpdate() {
            Thread.Sleep(20000);
            UpdateInverter();
            this.Invoke(new Action(() => pictureBox2.Visible = false));
            Thread a = new Thread(new ThreadStart(UpdateInverterThread));
            a.Start();
            OpenGui();
        }

        private void Label1_Click(object sender, EventArgs e) {
            OpenGui();
        }

        private void LblDetails_SizeChanged(object sender, EventArgs e) {
            pictureBox2.Location  = new Point(lblDetails.Size.Width + lblDetails.Location.X -3,pictureBox2.Location.Y);
        }

        private void Splashscreen_DoubleClick(object sender, EventArgs e) {
            if (skippable) {
                label1.Visible = true;
            }
        }
    }
}
