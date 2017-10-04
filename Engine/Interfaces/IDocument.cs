using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Interfaces {
    public interface IDocument {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IDocument"/> is exists.
        /// </summary>
        /// <value>
        ///  <c>true</c> if exists; otherwise, <c>false</c>.
        /// </value>
        bool Exists {
            get;
        }
        /// <summary>
        /// Gets or sets the name of a document
        /// </summary>
        /// <value> The name. </value>
        string Name {
            get;
            set;
        }
        /// <summary>
        /// Gets the type of a document.
        /// </summary>
        /// <value> The type. </value>
        string Type {
            get;
        }
        /// <summary>
        /// Gets or sets the relevance of the document.
        /// </summary>
        /// <value> The relevance. </value>
        string Relevance {
            get;
            set;
        }
        /// <summary>
        /// Gets the address of the document.
        /// </summary>
        /// <value> The address. </value>
/        string Address {
            get;
        }
        /// <summary>
        /// Gets the last modified dat
        /// </summary>
        /// <value> The last modified. </value>
        DateTime LastModified
        {
            get;
        }
    }
}
