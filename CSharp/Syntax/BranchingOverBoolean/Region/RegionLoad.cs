namespace BranchingOverBoolean
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 過去或一般常見的作法
    /// 利用Flag 去設定當下狀況 
    /// 再透過Boolean 判斷式一一驗證並決定如何運作
    /// </summary>
    public class RegionLoad<T> : ILoad<T>
    {
        public IEnumerable<T> Cache { get; }
        public IEnumerable<T> Db { get; }
        public IEnumerable<T> Fake { get; }

        private bool IsFromCache { get; set; }
        private bool IsFromDb { get; set; }
        private bool IsFromFake { get; set; }
        private bool IsPreview { get; set; }
        public virtual IEnumerable<T> Load(Func<T,bool> predicate)
        {
            //如果未做任何設定則初始化設定
            if(IsPreview)
            {
                IsFromDb = true;
                IsFromCache = false;
                IsFromFake = false;
            }

            //如果為測試則用Fake資料
            if(IsFromFake)
            {
                return Fake.Where(predicate);
            }
            //如果為Cache 則取用Cache資料
            else if(IsFromCache)
            {
                //如果Cache沒資料則像Db取資料
                //取得資料後放入Cache
                if(cache.Any())
                {
                    return Cache.Where(predicate);
                }
                else
                {
                    Cache = Db.Where(predicate);
                    return Cache;
                }
            }
            //如果為Db 則向Db取資料
            else if(IsFromDb)
            {
                return db.Where(predicate);
            }
            //以上皆非則回傳空列舉 盡量減少return null對程式的運作是有幫助的
            return Enumerable.Empty<T>();
        }
    }
}