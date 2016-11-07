using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching
{
    // 用此做法來取得正確的Loader
    // 概念是以Facade展示所有做法 並一一指向正確的方向
    public interface ILoadState<T>
    {
        ILoadState<T> LoadFromCache();
        ILoadState<T> LoadFromDb();
        ILoadState<T> LoadFromFake();
        // return value
        IEnumerable<T> Loader { get; }
    }
}
