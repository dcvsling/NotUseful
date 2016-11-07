
namespace BranchingOverBoolean.Branching
{
    using BranchingOverBoolean;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// 這裡將Loader的判斷以物件用State 加以分切
    /// 然後再以Command 把需要的作法加入
    /// </summary>
    public class Loader<T> : ILoad<T>
    {
        // 取得正確的Loader的方法
        protected ILoadState<T> loader { get; }
        public Loader()
        {
            // 可選擇任一一個new 如果有獨立Facade class 也可以
            loader = new CachingLoader<T>();
        }

        /// <summary>
        /// 這裡就坐單純的Loader Where
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Load(Func<T, bool> predicate)
        {
            var nowLoader = loader.LoadFromCaching(predicate)
                         // loader.LoadFromDb(predicate)
                         // loader.LoadFromFake(predicate)
            return nowLoader.Loader.Where(predicate);
        }
    }

    /// <summary>
    /// 如果加入新功能但仍然想保有舊的做法
    /// 這是類似Decorate的做法
    /// </summary>
    public class CombineLoader<T> : ILoad<T>
    {
        public NewLoader()
        {
            //有包括 blob的 Facade
            this.loader = new BlobAddins.BlobLoader<T>();
        }

        //仍然是單純做Loader Where
        public IEnumerable<T> Load(Func<T, bool> predicate)
        {
            var nowLoader = loader.LoadFromBlob(predicate)
                         // loader.LoadFromDb(predicate) 這會走向新的Db邏輯
            return nowLoader.Loader.Where(predicate);
        }
    }
}
