using System.Collections.Generic;

namespace Engine {
    /// <summary>
    /// Override the Comprator of Documents, Compares Name and Address insted of HashCode
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{Engine.Document}" />
    class DocumentComparer : IEqualityComparer<Document> {

        public bool Equals(Document x, Document y) {
            return (x.Exists == y.Exists) && (x.Address == y.Address)
                && (x.Name == y.Name);
        }
        public int GetHashCode(Document obj) {
            return base.GetHashCode();
        }
    }
}
