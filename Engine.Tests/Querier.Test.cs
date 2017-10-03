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
            //Assert.AreEqual<string[]>(expected,Actual);
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

    }
}
