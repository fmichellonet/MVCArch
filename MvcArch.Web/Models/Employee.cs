using System;
using System.ComponentModel.DataAnnotations;

namespace MvcArch.Web.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fist name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        [Editable(false)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Login")]
        [StringLength(10, MinimumLength = 3)]
        public string Login { get; set; }

    }
}