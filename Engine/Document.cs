using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    /// <summary>
    /// The Representation Of the available documents within the code.
    /// </summary>
    public class Document
    {
        private string _Name;
        private string _Address;
        private Format _Type;
        private DateTime _LastSeen;
        private string _Relevance;
        /// <summary>
        /// Gets or sets the name of the Document.
        /// </summary>
        /// <value>
        /// The name of the Document.
        /// </value>
        public string Name {
            get { return _Name; }
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Document Name","String is null Exception");
                _Name = value;
            }
        }
        /// <summary>
        /// Gets or sets the address or path to where the document is stored.
        /// </summary>
        /// <value>
        /// The location of the document.
        /// </value>
        public string Address {
            get { return _Address; }
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Document Path","String is null Exception");
                _Address = value;
            }
        }
        /// <summary>
        /// Gets or sets the type or file format of the document.
        /// </summary>
        /// <value>
        /// The type or file format ofthe document.
        /// </value>
        public Format Type {
            get { return _Type; }
            set {_Type = value; }
        }
        /// <summary>
        /// Gets or sets the last time the document was modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified {
            get { return _LastSeen; }
            set {
                if(value==null)
                    throw new ArgumentNullException("Last Modified","Parameter: Last Modified on "+_Name+" is not set");
                _LastSeen=value;
            }
        }
        /// <summary>
        /// The part of the document relevant to the query, To be displayed During Search
        /// </summary>
        public string Relevance {
            get { return _Relevance; }
            set {
                _Relevance=value??throw new ArgumentNullException("Relevance","Parameter: Relevenace of " + _Name + " is not set");
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="Name">The name of the Document.</param>
        /// <param name="Path">The path to where the document is stored.</param>
        /// <param name="Type">The typeor file format of the document.</param>
        /// <param name="LastModified">The last time the Document was Modified.</param>
        public Document(String Name,String Path,Format Type,DateTime LastModified) {
            this.Name = Name;
            this.Address = Path;
            this.LastModified = LastModified;
            this.Type = Type;
        }
    }
}
