using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine {
    public static class Updater {
        private static List<String> pathfiles;
        public static void Crawler(String path, Inverter invt) {
            Dictionary<String, Document> files = invt.Files;
            pathfiles = new List<string>();
            List<String> pathfile = new List<string>();
            //run a for loop on this for all types in format
            foreach (string doc in invt.Formats[""]) {
                pathfile= Directory.EnumerateFiles(path, "*."+doc,SearchOption.AllDirectories).ToList<string>();
                pathfiles = pathfiles.Union<string>(pathfile).ToList<string>();
            }
            //bool RemovedFile = false;
            foreach (String item in files.Keys.ToArray<String>()) {
                if (!pathfiles.Contains(item))
                {
                    Streamer.RemoveFile(files[item], invt);
                   // RemovedFile = true;
                }
                else if (new FileInfo(item).LastWriteTime.CompareTo(files[item].LastModified) != 0)
                {
                    Streamer.ModifyFile(files[item], invt);
                    //RemovedFile = true;
                }
            }
            //if (RemovedFile)
            //    invt.GarbageCollector();
            Dictionary<string,Exception> ErrorList = new Dictionary<string,Exception>();
            //adding file
            //creating a document object and pass into streamer.adddfile
            foreach(String location in pathfiles) {
                if(!files.ContainsKey(location)) {
                    try {
                        Document newdoc = GetDocumentFrom(location);
                        Streamer.AddFileFrom(newdoc,invt);
                    } catch(Exception ex) {
                        ErrorList.Add(location,ex);
                    }
                } else {
                    ErrorList.Add(location,new Exception("The File Type is Currently Not supported by this App"));
                }
            }
            if(ErrorList.Count > 0) { }
            //Todo create new Exception
            //throw new Exception("Couldn't withdraw some Files");
        }
        private static Document GetDocumentFrom(String path) {
            string posString = path.Substring(path.LastIndexOf("\\") + 1);
            int posOfType = posString.LastIndexOf(".");
            string ofName = posString.Substring(0, posOfType);
            string ofType = posString.Substring(posOfType + 1);
           return new Document(ofName, path, ofType, new FileInfo(path).LastWriteTime);
        }
        
    }
}



