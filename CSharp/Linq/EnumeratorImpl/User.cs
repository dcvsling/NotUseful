namespace NotUseful.CSharp.Linq.EnumeratorImpl
{
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    /// <summary>
    /// 繼承Entity的 User並且對Friends加工實作我們所需要的列舉
    /// 這裡透過將預設產生的Enumerator 紀錄在物件中
    /// 並透過幾個操作Enumerator的Method改為操作物件中的iterator來運作
    /// 即可以達到同樣的一個物件在IEnumerable的運作下達到內部Member的列舉運算
    /// 而所謂的Iterator Design Pattern 也就是Linq實際實作的Pattern
    /// </summary>
    public class User : Model.User, IEnumerable<User>,IEnumerator,IEnumerator<User>
    {
        /// <summary>
        /// 此處用建構子 為了不讓Friends == null
        /// </summary>
        public User()
        {
            this.Friends = this.Friends ?? new List<Model.User>();
        }

        /// <summary>
        /// Iterator實際運作的物件
        /// </summary>
        private IEnumerator<User> iterator = Enumerable.Empty<User>().GetEnumerator();

        /// <summary>
        /// Iterator實際的Current
        /// </summary>
        public User Friend => iterator.Current as User;

        /// <summary>
        /// 我們希望在IEnumerable[T]上運作的Type
        /// </summary>
        public object Current => this;

        /// <summary>
        /// 與this.Current目的一樣 只是多了型別
        /// </summary>
        User IEnumerator<User>.Current => this.Current as User;
                
        /// <summary>
        /// Dispose iterator 如果此類別需要被Dispose 可由此加工
        /// </summary>
        public void Dispose() => iterator.Dispose();

        /// <summary>
        /// 取得IEnumerator[T]
        /// 這裡我們建立Friends的 Enumerator後放在iterator上
        /// 然後回傳this 每次當列舉進行的時候就讓iterator前進 然後繼續回傳自己
        /// </summary>
        /// <returns>回傳物件本身 因為物件本身已經實作IEnumerator[T]了</returns>
        public IEnumerator<User> GetEnumerator() 
        {
            iterator = this.Friends.OfType<User>().GetEnumerator();
            return this;
        }
        /// <summary>
        /// 與this.GetEnumerator相同
        /// </summary>
        /// <returns>回傳this.GetEnumerator的結果</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// 讓iterator執行MoveNext並回傳執行結果
        /// </summary>
        /// <returns>Result of iterator.MoveNext</returns>
        public bool MoveNext() => iterator.MoveNext();

        /// <summary>
        /// 重置iterator
        /// </summary>
        public void Reset() => iterator.Reset();
    }
}
