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
            foreach (Format doc in Enum.GetValues(typeof(Format))) {
                pathfile= Directory.EnumerateFiles(path, "*."+doc.ToString(),SearchOption.AllDirectories).ToList<string>();
                pathfiles = pathfiles.Union<string>(pathfile).ToList<string>();
            }
            foreach (String item in files.Keys.ToArray<String>()) {
                if (!pathfiles.Contains(item)) 
                    Streamer.RemoveFile(files[item], invt);
                else if (new FileInfo(item).LastWriteTime.CompareTo(files[item].LastModified) != 0) 
                        Streamer.ModifyFile(files[item], invt);
            }
            //adding file
            //creating a document object and pass into streamer.adddfile
            foreach (String location in pathfiles) {
                if (!files.ContainsKey(location)) {
                    Document newdoc = GetDocumentFrom(location);
                    Streamer.AddFileFrom(newdoc, invt);
                }
            }
        }
        private static Document GetDocumentFrom(String path) {
            string posString = path.Substring(path.LastIndexOf("\\") + 1);
            int posOfType = posString.LastIndexOf(".");
            string ofName = posString.Substring(0, posOfType);
            string ofType = posString.Substring(posOfType + 1);
            Enum.TryParse<Format>(ofType, out Format type1);
           return new Document(ofName, path, type1, new FileInfo(path).LastWriteTime);
        }
    }
}



