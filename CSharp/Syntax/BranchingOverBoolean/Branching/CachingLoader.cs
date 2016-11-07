using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching
{
    public class CachingLoader<T> : ILoadState<T>
    {
        private IList<T> list;
        public CachingLoader()
        {
            list = new List<T>();
        }

        public IEnumerable<T> Loader { get; private set; }

        // 呼叫到自己的時候 把自己回傳
        // Cache發現自己沒有資料則回傳Db 並且告訴Db如何將Data塞入 Cache list
        public virtual ILoadState<T> LoadFromCache()
            => list.Any() ? this : new DbLoader<T>(x => Loader = x) as ILoadState<T>;
        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromDb()
            => new DbLoader<T>();
        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromFake()
            => new FakeLoader<T>();
    }
}
