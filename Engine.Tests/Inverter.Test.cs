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
            
            invt = new Inverter();
            Updater.Crawler("C:\\Users\\Phillz Mike\\Source\\Repos\\stot\\Engine\\Mock", (new Dictionary<string, Document>()),invt);
            invt.SaveThis();
            Assert.AreEqual(1,1);
        }
    }
}
