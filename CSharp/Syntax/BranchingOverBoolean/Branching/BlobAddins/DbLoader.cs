using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching.BlobAddins
{
    //繼承原本的Db做法並加以調整 繼承的目的是原本的Db引導邏輯必須照舊Db
    //此處亦可以用組合代替繼承 but 我不想寫那麼多code XD
    public class DbLoader<T> : Branching.DbLoader<T>, ILoadState<T>
    {
        private Action<IEnumerable<T>> cache { get; }
        
        // 新的Db邏輯 做出Blob 分流
        public DbLoader() : base() 
        {
            this.Loader = Environment.MachineName.StartsWith("NoDb") ? null : Enumerable.Range(0, 3).Select(x => default(T));
        }

        // 分流到Blob
        public ILoadState<T> LoadFromBlob()
            => new BlobLoader<T>();
        
        // 特定狀況下交由Blob來提供資料
        public override Branching.ILoadState<T> LoadFromDb()
            => Loader == null ? new BlobLoader<T>() : this as ILoadState<T>;
    }
}
