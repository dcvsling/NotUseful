using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching.BlobAddins
{
    // 新的 Blob Loader
    public class BlobLoader<T> : ILoadState<T>
    {
        public BlobLoader()
        {
            Loader = Enumerable.Range(0, 2).Select(x => default(T));
        }

        public IEnumerable<T> Loader { get; }

        // Call到Blob就回傳自己
        public ILoadState<T> LoadFromBlob()
            => this;

        // 不是就回傳別人
        public Branching.ILoadState<T> LoadFromCache()
            => new CachingLoader<T>();

        // 這裡要回傳新的DbLoader 才會走到新的Db邏輯
        public Branching.ILoadState<T> LoadFromDb()
            => new DbLoader<T>();

        // 不是就回傳別人
        public Branching.ILoadState<T> LoadFromFake()
            => new FakeLoader<T>();
    }
}
