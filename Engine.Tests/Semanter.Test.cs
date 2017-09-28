using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Diagnostics;

namespace Engine.Tests {
    [TestClass]
    public class SemanterTest {
        [TestMethod]
        public void Semanter() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<long> time=new List<long>();
            Semanter Samantha = new Semanter("../../../Resources/Dictionary.txt", "../../../Resources/commonSfw.txt");
         
            String[] All = { "United","States","America","Living", "Grapes", "Greens", "curiousity", "suggestions", "Live", "Grape", "Green", "curious", "suggest" };
            time.Add(sw.ElapsedMilliseconds);
            Samantha.StemWord(All[1].ToLower().Trim());
            time.Add(sw.ElapsedMilliseconds);

            for (int i = 0; i < All.Length; i++)
            {
                time.Add(sw.ElapsedMilliseconds);
                Samantha.StemWord(All[i].ToLower().Trim());
                time.Add(sw.ElapsedMilliseconds);
            }
            time.Add(11111111111111);
            for (int i = 0; i < All.Length; i++)
            {
                time.Add(sw.ElapsedMilliseconds);
                Samantha.StemWord(All[i].ToLower().Trim());
                time.Add(sw.ElapsedMilliseconds);
            }
            String[] Actuals = { "Living", "Grapes", "Greens", "curiousity", "suggestions" };
            String[] Expecteds = { "Live","Grape" ,"Green","curious","suggest"};
            for(int i=0;i<Actuals.Length;i++)
            Assert.AreEqual<String>(Expecteds[i],Samantha.StemWord(Actuals[i]));
        }

    }
}
