using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace Engine.Tests {
    [TestClass]
    public class RankerTest {
        [TestMethod]
        public void Ranker() {

        }
        [TestMethod]
        public void GetVectorTest() {
            List<string> query = new List<string>{ "God", "is", "good","is","god","is","good","fare" };
            double[] x = Engine.Ranker.GetVector(query);
            for (int i = 0; i < x.Length; i++) {
                x[i] = Engine.Ranker.TfWeight(x[i]);
            }
            Assert.AreEqual(2, x[0]);
            Assert.AreEqual(3, x[1]);
            Assert.AreEqual(2, x[2]);
            Assert.AreEqual(1, x[3]);
            //Assert.AreEqual(3, x[3]);
            //Console.Read();
        }
    }
}
