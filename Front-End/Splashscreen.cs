using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Front_End {
    public partial class Splashscreen : Form {
        public Splashscreen() {
            InitializeComponent();
            this.CenterToScreen();
        }
        Inverter invt;
        UNILAG gui;
        private void Splashscreen_Shown(object sender, EventArgs e) {
            Thread a = new Thread(new ThreadStart(StartUp));
            a.Start();
        }
        void ChangeLabel(String Text) {
            lblDetails.Text = Text;
        }
        void StartUp() {
            if (InvokeRequired) {
                this.Invoke(new Action(() => ChangeLabel("Loading Database...")));
            }
            try {
                invt = Inverter.Load("Inverter.invt");
                Inverter.LogMovement("^^^^^^^^^Succesfully loaded previous Inverter: ");
            } catch (Exception ex) {
                if (InvokeRequired) {
                    this.Invoke(new Action(() => ChangeLabel("Loading Database failed. Creating new one...")));
                }
                Inverter.LogMovement("!!!!!!!!!Error loading previous Inverter: " + ex.Message);
                invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
                                  , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string> { "../../../Resources/Sherlock.txt" });
            }
            if (InvokeRequired) {
                this.Invoke(new Action(() => ChangeLabel("Loading GUI...")));
                this.Invoke(new Action(() => gui = new UNILAG()));
            }
            Querier.Invt = invt;
            //gui = new UNILAG();
            if (InvokeRequired) {
                this.Invoke(new Action(() => ChangeLabel("Updating Database...")));
            }
            //Crawler
            Thread a = new Thread(new ThreadStart(FirstInvtUpdate));
            a.Start();
        }
        void OpenGui() {
            lblDetails.Text = "Done.";
            Thread a = new Thread(new ThreadStart(UpdateInverterThread));
            a.Start();
            gui.Show();
            Hide();
        }
        void UpdateInverterThread() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 60000) { }
            while (true) {
                sw.Restart();
                UpdateInverter();
                Inverter.LogMovement("lastSeen");
                //TODO remove last lastseen
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
            UpdateInverter();
            if (InvokeRequired) {
                this.Invoke(new Action(() => OpenGui()));
            }
        }
    }
}
