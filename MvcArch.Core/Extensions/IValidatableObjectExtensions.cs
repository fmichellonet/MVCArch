using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcArch.Core.Extensions
{
    public static class ValidatableObjectExtensions
    {
        public static bool IsValid(this IValidatableObject obj)
        {
            return !obj.Validate(new ValidationContext(obj, null, null)).Any();
        }
    }
}
