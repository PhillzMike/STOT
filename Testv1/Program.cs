using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Testv1 {
    class Program {
        static void Main(string[] args) {
            IEnumerable<String> boy = Directory.EnumerateFiles("../../../","*.cs",SearchOption.AllDirectories);
            
            boi boi=new boi(new Dictionary<string, DateTime>());

          


            using(FileStream fsin = new FileStream("../../../DeleteThis.File",FileMode.Open,FileAccess.Read,FileShare.None)) {
                boi = (boi)(new BinaryFormatter().Deserialize(fsin));
            }
            foreach(String a in boy) {
                if(boi.boii[a].CompareTo((new FileInfo(a)).LastWriteTime)!= 0){
                    Console.WriteLine(a + " was changed at " + (new FileInfo(a)).LastWriteTime);
                }
            }
            boi.boii.Clear();
            foreach(String a in boy) {
                //Console.Write(a + " " + (new FileInfo(a)).LastWriteTime); 
                boi.boii.Add(a,(new FileInfo(a)).LastWriteTime);
                Console.WriteLine(); 
            }
            
            using(FileStream fsout = new FileStream("../../../DeleteThis.File",FileMode.Create,FileAccess.Write,FileShare.None)) {
                new BinaryFormatter().Serialize(fsout,boi);
            }
            Console.Read();
        }

    }
    [Serializable]
    class boi  {
        public Dictionary<String,DateTime> boii;
        public boi(Dictionary<String,DateTime> boii) {
            this.boii = boii;
        }
    }
}
