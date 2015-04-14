using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public class ReturnsBoolExecutionResult : ExecutionResult
    {
        public bool? Value { get; set; }

        public bool IsTrue
        {
            get { return Value.GetValueOrDefault(); }
        }
    }
}
