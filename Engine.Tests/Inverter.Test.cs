using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Engine.Tests
{
    [TestClass]
    public class InverterTest
    {
        Inverter invt;
        [TestMethod]
        public void Inverter() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string query = " United       States  of     America";
            Double t1 = sw.ElapsedMilliseconds; 
            invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
                         , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
           // invt = Engine.Inverter.Load("Tester");
            
            Double t2 = sw.ElapsedMilliseconds;
            Updater.Crawler("../../../Resources/Mock", invt);
            Double t25 = sw.ElapsedMilliseconds;
            invt.SaveThis("Tester");
            //Console.WriteLine();
            // Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock", invt.Files, invt);
            Double t3 = sw.ElapsedMilliseconds;
            //TODO figure out how to get first run slower
            //invt.Samantha.StemWord("United");
            Double t4 = sw.ElapsedMilliseconds;
            List<Document> x = Querier.Search(query, invt);
            Double time = sw.ElapsedMilliseconds;
            Assert.AreEqual(1,1);
        }
    }
}
