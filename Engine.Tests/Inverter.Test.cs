using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.IO;

namespace Engine.Tests
{
    [TestClass]
    public class InverterTest
    {
        [TestMethod]
        public void Inverter()
        {
            foreach(String a in Directory.EnumerateFiles("../../Mock","*.",SearchOption.AllDirectories)) {
                try {
                    DateTime i = new DateTime();
                    Document doc = new Document("File 1",a,Format.docx,i);
                    Streamer.AddFileFrom(doc);
                } catch{}
            }
            //TODOint i = Engine.Inverter.boy;
        }
    }
}
