using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine {
    /// <summary>
    /// 
    /// </summary>
    public static class Updater {
        private static List<String> pathfiles;
        /// <summary>
        /// It checks all the documents in path and updates them. It is responsible for deleting document,
        /// adding documents and modifying documents.
        /// </summary>
        /// <param name="path">The path which contains the documents.</param>
        /// <param name="invt">An object of the inverter Class.</param>
        public static void Crawler(String path, Inverter invt)
        {
            Dictionary<String, Document> files = invt.Files;
            pathfiles = new List<string>();
            List<String> pathfile = new List<string>();
            Streamer.Invt = invt;
            //run a for loop on this for all types in format
            foreach (string type in invt.Formats[""])
            {
                pathfile = Directory.EnumerateFiles(path, "*." + type, SearchOption.AllDirectories).Select(Path.GetFullPath).ToList<string>();
                pathfiles = pathfiles.Union<string>(pathfile).ToList<string>();
            }
            foreach (String item in files.Keys.ToArray<String>())
            {
                if (!pathfiles.Contains(item))
                {
                    Streamer.RemoveFile(files[item]);
                }
                else if (new FileInfo(item).LastWriteTime.CompareTo(files[item].LastModified) != 0)
                {
                    try
                    {
                        Streamer.ModifyFile(files[item]);
                    }
                    catch (Exception ex)
                    {
                        Inverter.LogMovement("!!!!!!!!!Error withdrawing from " + files[item].Address + " while modifying. ERROR MESSAGE:" + ex.Message);
                    }
                }
            }

            //adding file
            //creating a document object and pass into streamer.adddfile
            foreach (String location in pathfiles)
            {
                if (!files.ContainsKey(location))
                {
                    try
                    {
                        Document newdoc = GetDocumentFrom(location);
                        Streamer.AddFileFrom(newdoc);
                    }
                    catch (Exception ex)
                    {
                        Inverter.LogMovement("!!!!!!!!!Error withdrawing from " + location + ". ERROR MESSAGE:" + ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Gets the details of the document which includes: name, path, time and time it was last modified.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>  it returns an object of document with the necessary information.  </returns>
        private static Document GetDocumentFrom(String path) {
            string posString = path.Substring(path.LastIndexOf("\\") + 1);
            int posOfType = posString.LastIndexOf(".");
            string ofName = posString.Substring(0, posOfType);
            string ofType = posString.Substring(posOfType + 1);
           return new Document(ofName, path, ofType, new FileInfo(path).LastWriteTime);
        }
        
    }
}



