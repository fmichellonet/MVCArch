using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcArch.Core.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static ModelStateDictionary MergeWithValidatableObject(this ModelStateDictionary modelState, IValidatableObject obj)
        {
            IEnumerable<ValidationResult> errors = obj.Validate(new ValidationContext(obj, null, null));
            foreach (ValidationResult error in errors)
                foreach (string memberName in error.MemberNames)
                {
                    if (modelState.ContainsKey(memberName))
                        modelState.AddModelError(memberName, error.ErrorMessage);
                    else
                        modelState.AddModelError(memberName, error.ErrorMessage);
                }
            return modelState;
        }

        public static ModelStateDictionary MergeWithBusinessException(this ModelStateDictionary modelState, BusinessRuleException ex)
        {
            modelState.AddModelError(string.Empty, ex.Message);
            return modelState;
        }
    }
}
