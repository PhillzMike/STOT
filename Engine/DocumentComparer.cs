using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
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
