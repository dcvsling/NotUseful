using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching
{
    public class DbLoader<T> : ILoadState<T>
    {
        public IEnumerable<T> Loader { get; protected set; }
        private Action<IEnumerable<T>> cache { get; }
        
        // 一般狀況去Db取資料
        public DbLoader()
        {
            Loader = Enumerable.Range(0, 3).Select(x => default(T));
        }
        // 有要求資料需要做額外處理時使用 ex : cache
        public DbLoader(Action<IEnumerable<T>> cache) : this()
        {
            this.cache = cache;
            cache(Loader);
        }
        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromCache()
            => new CachingLoader<T>();
        // 呼叫到自己的時候 把自己回傳
        public virtual ILoadState<T> LoadFromDb()
            => this;
        // 不是的時候就回傳別人
        public ILoadState<T> LoadFromFake()
            => new FakeLoader<T>();
    }
}
