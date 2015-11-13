using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace Orders.com.Web.Api
{
    public static class ModelStateDictionaryExtensions
    {
        public static ModelStateDictionary ClearFirst(this ModelStateDictionary modelState)
        {
            modelState.Clear();
            return modelState;
        }

        public static ModelStateDictionary ThenAddRange(this ModelStateDictionary modelState, IEnumerable<ValidationResult> validationResults)
        {
            foreach (var error in validationResults)
            {
                if (error.MemberNames.Any())
                    modelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                else
                    modelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            return modelState;
        }
    }
}