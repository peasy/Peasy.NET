using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facile
{
    public class ReturnsIEnumerableExecutionResult<T> : ExecutionResult
    {
        public IEnumerable<T> Value { get; set; }
    }
}
