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
            Streamer.Invt = invt;
            //run a for loop on this for all types in format
            foreach (string type in invt.Formats[""]) {
                pathfile= Directory.EnumerateFiles(path, "*."+type,SearchOption.AllDirectories).ToList<string>();
                pathfiles = pathfiles.Union<string>(pathfile).ToList<string>();
            }
            bool RemovedFile = false;
            foreach (String item in files.Keys.ToArray<String>()) {
                if (!pathfiles.Contains(item))
                {
                    Streamer.RemoveFile(files[item]);
                    RemovedFile = true;
                }
                else if (new FileInfo(item).LastWriteTime.CompareTo(files[item].LastModified) != 0)
                {
                    try {
                        Streamer.ModifyFile(files[item]);
                        RemovedFile = true;
                    } catch (Exception ex) {
                        Inverter.LogMovement("!!!!!!!!!Error withdrawing from " + files[item].Address + " while modifying. ERROR MESSAGE:" + ex.Message);
                    }
                }
            }
            
            //adding file
            //creating a document object and pass into streamer.adddfile
            foreach(String location in pathfiles) {
                if(!files.ContainsKey(location)) {
                    try {
                        Document newdoc = GetDocumentFrom(location);
                        Streamer.AddFileFrom(newdoc);
                    } catch(Exception ex) {
                        Inverter.LogMovement("!!!!!!!!!Error withdrawing from " + location + ". ERROR MESSAGE:" + ex.Message);
                    }
                }
            }
            if (RemovedFile)
                invt.GC();
           
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



