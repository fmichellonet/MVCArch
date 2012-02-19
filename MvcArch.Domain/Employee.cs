using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcArch.Domain
{
    public class Employee : IValidatableObject
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Fist name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [Display(Name = "Login")]
        [StringLength(10, MinimumLength = 3)]
        public string Login { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {   
            // Implementation des regles business.
            
            // Il faut avoir au moins 18 ans.
            if(BirthDate.AddYears(18) > DateTime.Today)
                yield return new ValidationResult(@"Il faut avoir au moins 18 ans", new[] {@"BirthDate"});
        }
    }

}
