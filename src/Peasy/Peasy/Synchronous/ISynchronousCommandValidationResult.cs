using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peasy.Synchronous
{
    /// <summary>
    /// </summary>
    public interface ISynchronousCommandValidationResult<T>
    {
        ///
        IEnumerable<ValidationResult> Results { get; }

        ///
        bool CanContinue { get; }

        ///
        Func<T> CompleteCommandExecution { get; }
    }
}