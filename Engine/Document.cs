using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    //Author ??
    public class Document
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Summary { get; set; }
        public DateTime LastModified { get; set; }
        /// <summary>
        /// The part of the document relevant to the query
        /// </summary>
        public string Relevance { get; set; }
        public Document(String Name,String Path,String Summary) { }
    }
}
