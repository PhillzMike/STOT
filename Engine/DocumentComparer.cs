using System.Collections.Generic;
using Engine;
namespace Engine {
    /// <summary>
    /// Override the Comprator of Documents, Compares Name and Address insted of HashCode
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{IDocument}" />
    class DocumentComparer : IEqualityComparer<IDocument> {

        public bool Equals(IDocument x, IDocument y) {
            return (x.Exists == y.Exists) && (x.Address == y.Address)
                && (x.Name == y.Name);
        }
        public int GetHashCode(IDocument obj) {
            return base.GetHashCode();
        }
    }
}
