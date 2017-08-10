using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    public static class Updater
    {
        public static List<Document> Crawler(String path, Dictionary<String,Document> files) {
            //run a for loop on this for all types in format
            IEnumerable<String> pathfiles = Directory.EnumerateFiles(path, "*.pdf", SearchOption.AllDirectories);
            List<String> filesdocname = new List<String>();
            //Comparing to check if file was modified``
            //.CompareTo((new FileInfo(a)).LastWriteTime)!= 0, time of modification

         
            //deleting file
           foreach (String item in files.Keys.ToArray<String>())
           {
                if (!pathfiles.Contains(item))
                {
                    Streamer.RemoveFile(files[item]);
                }
                else {
                    FileInfo thisguyzinfo = new FileInfo(item);
                    DateTime lastModified = thisguyzinfo.LastWriteTime;
                    if (lastModified.CompareTo(files[item].LastModified) != 0) {
                        Streamer.ModifyFile(files[item]);
                    }
                }
                
            }
           //adding file
           //creating a document object and pass into streamer.adddfile
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

