using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.IO;
namespace Engine.Tests
{
    [TestClass]
    public class InverterTest
    {
        Inverter invt;
        [TestMethod]
        public void Inverter()
        {
            invt = new Inverter();
            foreach(String a in Directory.EnumerateFiles("../../../Engine/Mock","*.*",SearchOption.AllDirectories)) {
                try {
                    DateTime i = new DateTime();
                    FileInfo fi = new FileInfo(a);
                    Enum.TryParse<Format>(fi.Extension,out Format typ);
                    Document doc = new Document(fi.Name,a,typ,i);
                    Streamer.AddFileFrom(doc,invt);
                } catch(Exception ex) {
                    Assert.Fail("Someone Failed Here " + ex.Message + ex.Data,invt.Table);
                }
            }
            Assert.AreEqual(1,1);
        }
    }
}
