using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace MvcArch.Tests.Domain
{
    public class EmployeeDomainTests
    {
        [Test]
        public void Employee_Must_Be_18()
        {
            // Test
            MvcArch.Domain.Employee emp = new MvcArch.Domain.Employee { FirstName = "First", BirthDate = DateTime.Today.AddYears(-5)};
            
            List<ValidationResult> results = emp.Validate(new ValidationContext(emp, null, null)).ToList();
            Assert.That(results, Has.Count.EqualTo(1));
            Assert.That(results.First(), Has.Property("ErrorMessage").StringContaining("18 ans"));

            emp.BirthDate = emp.BirthDate.AddYears(-13);
            results = emp.Validate(new ValidationContext(emp, null, null)).ToList();
            Assert.That(results, Has.Count.EqualTo(0));
        }
    }
}
