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
            string query = "United States of America";
            invt = new Inverter(@"C:\Users\Phillz Mike\Source\Repos\stot\Engine\stopwords.txt", @"C:\Users\Phillz Mike\Source\Repos\stot\Dictionary.txt"
                                , @"C:\Users\Phillz Mike\Source\Repos\stot\commonSfw.txt",new List<string>());
            var y = Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock", (new Dictionary<string, Document>()),invt);
            //invt.SaveThis();
            //Console.WriteLine();
            //var z = Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock", y, invt);
            List<Document> x = Querier.Search(query, invt);
            Assert.AreEqual(1,1);
        }
    }
}
