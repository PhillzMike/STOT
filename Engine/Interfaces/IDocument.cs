using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Interfaces {
    public interface IDocument {
        bool Exists {
            get;
        }
        string Name {
            get;
            set;
        }
        string Type {
            get;
        }
        string Relevance {
            get;
            set;
        }
        string Address {
            get;
        }
        DateTime LastModified {
            get;
        }
    }
}
