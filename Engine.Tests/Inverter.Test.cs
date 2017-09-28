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
            Double t1 = sw.ElapsedMilliseconds; 
           // invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
             //            , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
            invt = Engine.Inverter.Load("Tester");
            
            Double t2 = sw.ElapsedMilliseconds;
            Updater.Crawler("../../../Resources/Mock", invt);
            Double t3 = sw.ElapsedMilliseconds;
            // invt.SaveThis("Tester");
            Double t4 = sw.ElapsedMilliseconds;
            //TODO figure out how and why first run slower
            invt.Samantha.StemWord("United");
            Double t5 = sw.ElapsedMilliseconds;

            
            List<Document> x = Querier.Search(" United    abstract   State's    type:t   of     America type :p", invt);
            Double Firsttime = sw.ElapsedMilliseconds-t5;
            sw.Restart();
            string[] queries = {" United    abstract   State's    type:t   of     America type :p",
                "Timilehin Fasip","Where is Jon snow","Asia is it a continent?","marriage village girls timi jedidiah",
            "somebody thinks his dad is joking","Please i need to remember to remove numbers","How do you spell daenerys",
            "Primary colors are blue yellow red","Olamide is a fish","I love food don't mock me","Deji is a pseudo programmer even at rural weddings"};
            List<List<Document>> results = new List<List<Document>>();
            List<long> time = new List<long>();
            for (int i = 0; i < 1000; i++)
            {
                foreach (string query in queries)
                    results.Add(Querier.Search(query, invt));
                time.Add(sw.ElapsedMilliseconds);
            }
            double endTime = sw.ElapsedMilliseconds;
            double proveSWisOn;
            try { throw new Exception(); }
            catch { proveSWisOn = sw.ElapsedMilliseconds; }
            Assert.AreEqual(1,1);
        }
    }
}
