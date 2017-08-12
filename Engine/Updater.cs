using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    public static class Updater
    {
        private static IEnumerable<String> pathfiles;
        public static List<Document> Crawler(String path, Dictionary<String, Document> files) {
            //run a for loop on this for all types in format
            foreach (Format doc in Enum.GetValues(typeof(Format)))
            {
                IEnumerable<String> pathfiles = Directory.EnumerateFiles(path, "*.{0}", SearchOption.AllDirectories);

            }
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
            foreach (String name in pathfiles)
            {
                if (!files.ContainsKey(name))
             //add name from path and etc;       Document doc = new Document();
                     //   Streamer.AddFileFrom(doc);
                }
            return null;
        }
         }
  }



