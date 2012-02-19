using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Patterns;
using Moq;
using MvcArch.Core;
using MvcArch.Dal;
using MvcArch.Services;
using NUnit.Framework;

namespace MvcArch.Tests.System
{
    [TestFixture]
    public class EmployeeServiceTests
    {

        private List<Employee> _list;

        [TestFixtureSetUp]
        public void Init()
        {
            Employee e1 = new Employee{ Id = 1, FirstName  = "First", LastName = "Last", BirthDate = DateTime.Today, Login = "FMI"};
            
            _list = new List<Employee>(new[] { e1});
        }

        [Test]
        public void Login_Must_Be_Unique()
        {
            // Arrange repository
            Mock<IRepository<Employee>> repo = new Mock<IRepository<Employee>>();
            repo.Setup(rr => rr.Find(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns(
                    (Expression<Func<Employee, bool>> expr, Expression<Func<Employee, object>>[] includeProperties) =>
                    _list.Where(expr.Compile()));

            // Arrange unit of work
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();

            // Arrange service
            Mock<EmployeeService> service = new Mock<EmployeeService>(repo.Object, uow.Object) {CallBase = true};

            // Test
            MvcArch.Domain.Employee domainEmp = new MvcArch.Domain.Employee { FirstName = "New First", LastName = "New Last", BirthDate = new DateTime(1970, 1, 1), Login = "FMI", Id = -1 };
            Assert.Throws<BusinessRuleException>(() => service.Object.Add(domainEmp));
        }
    }
}
