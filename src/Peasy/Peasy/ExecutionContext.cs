using System.Collections.Generic;

namespace Peasy
{
    public class ExecutionContext<T>
    {
        public T CurrentEntity { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
