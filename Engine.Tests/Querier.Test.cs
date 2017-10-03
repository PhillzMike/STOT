using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Linq;

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
            string pathString = System.IO.Path.Combine(@"C:\Users\Teniola\Documents\Visual Studio 2017\Projects\Stot\Resources", "Testing Folder");
            System.IO.Directory.CreateDirectory(pathString);
            Inverter invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
                    , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
            Engine.Updater.Crawler(pathString, invt);
            Querier.Invt = invt;
            string s = "type:pdf";
            Char[] space = { ' ' };
            string[] sarray = s.Split(space, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> actual = Querier.TypeChecker(sarray.ToList());
           string expected = "pdf";
           Assert.AreEqual<String>(expected, actual.ToArray()[0]);
        }

    }
}
