using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Engine
{
    [Serializable]
    /// <summary>
    /// The Representation Of the available documents within the code.
    /// </summary>
    public class Document
    {
        private string _Name;
        private string _Address;
        private string _Type;
        private DateTime _LastSeen;
        private string _Relevance;
        private bool _Available;

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="Name">The name of the Document.</param>
        /// <param name="Path">The path to where the document is stored.</param>
        /// <param name="LastModified">The last time the Document was Modified.</param>
        public Document(String Name, String Path, String Type, DateTime LastModified) {
            this.Name = Name;
            Address = Path;
            this.LastModified = LastModified;
            this.Type = Type;
            _Available = true;
            this.Relevance = "";
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class from an already existing Document.
        /// </summary>
        /// <param name="doc">The document that ws modified.</param>
        /// <param name="LastModified">The last modified.</param>
        public Document(Document doc, DateTime LastModified) : this(doc.Name, doc.Address, doc.Type, LastModified) { }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance. By giving the pth
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return Address;
        }
        /// <summary>
        /// Deletes this Document.
        /// All references to this document Should no longer work
        /// </summary>
        public void Delete() {
            _Available = false;
            _Name = "";
            _Relevance = "";
        }
        [BsonElement("Available")]
        /// <summary>
        /// Gets a value indicating whether this <see cref="Document"/> exists.
        /// <para>All references to this Instance must ensure this is true before proceeding, to avoid null pointer Exceptions</para>
        /// </summary>
        /// <value>
        ///   <c>true</c> if exists; otherwise, <c>false</c>.
        /// </value>
        public bool Exists {
            get { return _Available;}
            private set {
                _Available = value;
            }
        }
        [BsonElement("name")]
        /// <summary>
        /// Gets or sets the name of the Document.
        /// </summary>
        /// <value>
        /// The name of the Document.
        /// </value>
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        [BsonId]
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
        [BsonElement("type")]
        /// <summary>
        /// Gets or sets the type or file format of the document.
        /// </summary>
        /// <value>
        /// The type or file format ofthe document.
        /// </value>
        public string Type {
            get { return _Type; }
            set {_Type = value; }
        }
        [BsonDateTimeOptions]
        /// <summary>
        /// Gets or sets the last time the document was modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified {
            get { return _LastSeen; }
            set {_LastSeen=value; }
        }
        [BsonElement("Relevance")]
        /// <summary>
        /// The part of the document relevant to the query, To be displayed During Search
        /// </summary>
        public string Relevance {
            get { return _Relevance; }
            set {
                _Relevance=value??throw new ArgumentNullException("Relevance","Parameter: Relevenace of " + _Address + " is not set");
            }
        }

    }

}
