using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
namespace Engine.Tests
{
    [TestClass]
    public class InverterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
          
        }
        [TestMethod]
        public void SplitWhitespaceTest() {
            string s = "type                    :";
            Char[] space = { ' ' };
            string[] Actual = s.Split(space,StringSplitOptions.RemoveEmptyEntries);
            string[] expected = { "type",":" };
            for(int i = 0;i < expected.Length;i++)
                Assert.AreEqual<string>(expected[i],Actual[i]);
        }
    }
}
