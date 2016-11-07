namespace BranchingOverBoolean
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 擴充時則繼承前類別並加以調整
    /// </summary>
    public class AddBlobLoad<T> : RegionLoad<T>
    {
        public IEnumerable<T> Blob { get; }
        
        private bool IsBlob { get; set; }
        /// <summary>
        /// override 舊的做法並增加新的邏輯
        /// </summary>
        public override IEnumerable<T> Load(Func<T,bool> predicate)
        {
            //取的舊的做法的結果
            var result = base.Load(predicate);
            //此case僅有如果前者沒資料而且IsBlob時運作
            //如果無論如何都取Blob的值就得另外做
            //且無法將Blob判斷融入Cache判斷中
            if(!result.Any() && IsBlob)
            {
                return Blob.Where(predicate);
            }
            return result;
        }
    }
}