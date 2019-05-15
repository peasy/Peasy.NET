using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// Internal sealed class meant to be used to avoid the need for reflection.
    /// </summary>
    internal sealed class PeasyValidationResult : ValidationResult
    {
        public PeasyValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public PeasyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }
    }

}
