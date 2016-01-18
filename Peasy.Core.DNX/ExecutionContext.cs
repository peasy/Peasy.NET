using System.Collections.Generic;

namespace Peasy.Core
{
    public class ExecutionContext<T>
    {
        public T CurrentEntity { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
