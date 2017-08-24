using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.IO;
using System.Collections.Generic;

namespace Engine.Tests
{
    [TestClass]
    public class InverterTest
    {
        Inverter invt;
        [TestMethod]
        public void Inverter(){
            string query = "Depth First Search";
            //invt = new Inverter(@"C:\Users\Phillz Mike\Source\Repos\stot\Resources\stopwords.txt", @"C:\Users\Phillz Mike\Source\Repos\stot\Resources\Dictionary.txt"
            //                    , @"C:\Users\Phillz Mike\Source\Repos\stot\Resources\commonSfw.txt", new List<string>());
            invt = Engine.Inverter.Load("Tester");
            Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock",invt);
            //invt.GarbageCollector();
            //invt.SaveThis("Tester");
            //Console.WriteLine();
           // Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock", invt.Files, invt);
            List<Document> x = Querier.Search(query, invt);
            Assert.AreEqual(1,1);
        }
    }
}
