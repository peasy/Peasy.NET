using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public class ExecutionResult
    {
        public bool Success { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; }
    }

    public class ExecutionResult<T> : ExecutionResult
    {
        public T Value { get; set; }
    }
}
