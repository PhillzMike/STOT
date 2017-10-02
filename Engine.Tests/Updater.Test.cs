using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.IO;
using System.Linq;

namespace Engine.Tests {
    [TestClass]
    public class UpdaterTest {
        [TestMethod]
        public void Updater() {
            string pathString = System.IO.Path.Combine(@"C:\Users\Teniola\Documents\Visual Studio 2017\Projects\Stot\Resources", "Testing Folder");
            System.IO.Directory.CreateDirectory(pathString);
            Inverter invt = new Inverter("../../../Resources/stopwords.txt", "../../../Resources/Dictionary.txt"
                    , "../../../Resources/commonSfw.txt", "../../../Resources/Formats.txt", new List<string>());
            Engine.Updater.Crawler(pathString, invt);
            File.Create(pathString +"//tenifolder.txt");
            File.Create(pathString + "//myfolder.pdf");
            Engine.Updater.Crawler(pathString, invt);
            Assert.AreEqual(2, invt.Files.Count);
            Assert.IsTrue(invt.AllWordsInTable.Contains(invt.Samantha.StemWord("tenifolder")));
            Assert.AreEqual(2, invt.DocumentCount);
          
            File.Delete(pathString + "//tenifolder.txt");
            File.Delete(pathString + "//myfolder.pdf");
            Engine.Updater.Crawler(pathString, invt);
            System.IO.Directory.Delete(pathString);
            //File.AppendAllText((pathString + "tenifolder.txt") , "i am so hungry i feel like fainting and i havent slept..arrrrghhhhh");
            //Engine.Updater.Crawler(pathString, invt);

        }

    }
}
