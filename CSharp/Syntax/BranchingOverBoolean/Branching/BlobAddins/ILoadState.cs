using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchingOverBoolean.Branching.BlobAddins
{
    // 增加功能時增加Facade 項目
    public interface ILoadState<T> : Branching.ILoadState<T>
    {
        ILoadState<T> LoadFromBlob();
    }
}
