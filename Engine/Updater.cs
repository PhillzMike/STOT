using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    public static class Updater
    {
        public static List<Document> Crawler(String path, List<Document> files) {
            
            IEnumerable<String> pathfiles = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
            List<String> filesdocname = new List<String>();
            //Comparing to check if file was modified``
            //.CompareTo((new FileInfo(a)).LastWriteTime)!= 0, time of modification

            //deleting file
           foreach (var item in files)
           { filesdocname.Add(item.Name);
                if (!pathfiles.Contains(item.Name))
                {
                    Streamer.RemoveFile(item);
                }
            }
           //adding file
            foreach (var name in pathfiles)
            {
                if (!filesdocname.Contains(name)) {
                    foreach (var doc in files)
                    {
                        if (doc.Name.Equals(name))
                            Streamer.AddFileFrom(doc);
                    }
                }
            }
            //modify file
             
            return null;
        }
            } 
}

