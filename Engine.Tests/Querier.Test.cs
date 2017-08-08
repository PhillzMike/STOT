using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
namespace Engine.Tests
{
    [TestClass]
    public class QuerierTest
    {
        [TestMethod]
        public void SplitWhitespaceTest() {
            string s = "type                    :";
            Char[] space = { ' ' };
            string[] Actual = s.Split(space,StringSplitOptions.RemoveEmptyEntries);
            string[] expected = { "type",":" };
            for(int i = 0;i < expected.Length;i++)
                Assert.AreEqual<string>(expected[i],Actual[i]);
        }
        [TestMethod]
        public void SearchTest()
        {
            string s = "type                    :  pdf";
            Char[] space = { ' ' };
            string[] sarray = s.Split(space, StringSplitOptions.RemoveEmptyEntries);
            string actual = Querier.TypeChecker(sarray);
            string expected = ("pdf");
           Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void PossibleTypeTest()
        {
            string s = "p";
            Format[] sarray = { Format.pdf, Format.ppt, Format.pptx};
            List<Format> actual = Querier.PossibleType(s);
            List<Format> expected = new List<Format>(sarray);
            for (int i = 0; i < actual.Count; i++)
                Assert.AreEqual<Format>(expected[i], actual[i]);
        }
    }
}
