using System;
using System.ComponentModel.DataAnnotations;
using MvcArch.Web.Models;
using NUnit.Framework;

namespace MvcArch.Tests.Views
{
    
    public class EmployeeView
    {
        [Test]
        public void First_Name_Is_Required()
        {
            Employee emp = new Employee {LastName = "Last", BirthDate = DateTime.Today, Login = "AAA"};
            ValidationContext context = new ValidationContext(emp, null, null);
            ValidationException ex = Assert.Throws<ValidationException>(() => Validator.ValidateObject(emp, context, true));
            Assert.That(ex.Message, Is.EqualTo("The Fist name field is required."));
        }
        

        [Test]
        public void Login_Must_Be_3_Char_Min()
        {
            Employee emp = new Employee {FirstName = "First", LastName = "Last", BirthDate = DateTime.Today, Login = "AA" };
            ValidationContext context = new ValidationContext(emp, null, null);
            ValidationException ex = Assert.Throws<ValidationException>(() => Validator.ValidateObject(emp, context, true));
            Assert.That(ex.Message, Is.EqualTo("The field Login must be a string with a minimum length of 3 and a maximum length of 10."));
        }
    }
}
