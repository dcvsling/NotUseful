using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean
{
    public interface ILoad<T>
    {
        IEnumerable<T> Load(Func<T,bool> predicate);
    }
}
