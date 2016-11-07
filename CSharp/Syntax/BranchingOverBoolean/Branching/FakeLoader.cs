using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching
{
    public class FakeLoader<T> : ILoadState<T>
    {
        public FakeLoader()
        {
            Loader = Enumerable.Range(0, 1).Select(x => default(T));
        }

        public IEnumerable<T> Loader { get; }
        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromCache()
            => new CachingLoader<T>();

        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromDb()
            => new DbLoader<T>();

        // 呼叫到自己的時候 把自己回傳
        public virtual ILoadState<T> LoadFromFake()
            => this;
    }
}
