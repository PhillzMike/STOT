using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
namespace Engine.Tests {
    [TestClass]
    public class SemanterTest {
        [TestMethod]
        public void Semanter() {
            Semanter samantha = new Semanter();
            String[] Actuals = { "Living","Grapes","Greens" ,"curiousity","suggestions"};
            String[] Expecteds = { "Live","Grape" ,"Green","curious","suggest"};
            for(int i=0;i<Actuals.Length;i++)
            Assert.AreEqual<String>(Expecteds[i],samantha.StemWord(Actuals[i]));
        }

    }
}
