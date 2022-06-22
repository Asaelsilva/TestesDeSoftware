using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string msg) : base(msg) { }
        public DomainException(string msg, Exception inner) : base(msg, inner) { }
    }
}
