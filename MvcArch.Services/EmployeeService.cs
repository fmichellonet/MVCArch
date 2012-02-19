using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using EntityFramework.Patterns;
using MvcArch.Core;
using MvcArch.Core.Extensions;
using MvcArch.Core.Log;
using MvcArch.IServices;
using NLog;

namespace MvcArch.Services
{
    [Tracelog]
    public class EmployeeService : IEmployeeService
    {
        private static readonly object WriteLock = new object();

        private readonly IRepository<Dal.Employee> _empRepo;
        private readonly IUnitOfWork _uow;

        static EmployeeService()
        {
            Mapper.CreateMap<Dal.Employee, Domain.Employee>();
            Mapper.CreateMap<Domain.Employee, Dal.Employee>();
        }

        public EmployeeService(IRepository<Dal.Employee> empRepo, IUnitOfWork uow)
        {
            _empRepo = empRepo;
            _uow = uow;
        }

        public IEnumerable<Domain.Employee> LoadAll()
        {
            IEnumerable<Dal.Employee> lst = _empRepo.GetAll();
            IEnumerable<Domain.Employee> domainLst = Mapper.Map<IEnumerable<Dal.Employee>, IEnumerable <Domain.Employee>> (lst);

            return domainLst;
        }

        public Domain.Employee GetById(int id)
        {
            Dal.Employee emp = _empRepo.First(e => e.Id == id);
            return Mapper.Map<Dal.Employee, Domain.Employee>(emp);
        }

        public void Add(Domain.Employee employee)
        {
            if(employee == null)
                throw new ArgumentException("employee");

            if (!employee.IsValid())
                throw new BusinessRuleException(employee.Validate(new ValidationContext(this, null, null)));
 
            Dal.Employee dalEmp = Mapper.Map<Domain.Employee, Dal.Employee>(employee);
            lock (WriteLock)
            {
                EnsureLoginIsUnique(employee);

                _empRepo.Insert(dalEmp);
                _uow.Commit();
            }
        }

        public void Update(Domain.Employee employee)
        {
            if (employee == null)
                throw new ArgumentException("employee");

            if (!employee.IsValid())
                throw new BusinessRuleException(employee.Validate(new ValidationContext(this, null, null)));

            lock (WriteLock)
            {
                EnsureLoginIsUnique(employee);

                Dal.Employee dalEmp = Mapper.Map<Domain.Employee, Dal.Employee>(employee);
                _empRepo.Update(dalEmp);
                _uow.Commit();
            }
        }

        public void Delete(int id)
        {
            lock (WriteLock)
            {
                Dal.Employee emp = _empRepo.First(e => e.Id == id);
                _empRepo.Delete(emp);
                _uow.Commit();
            }
        }

        private void EnsureLoginIsUnique(Domain.Employee employee)
        {
            if (_empRepo.Find(e => e.Login == employee.Login && e.Id != employee.Id).Any())
                throw new BusinessRuleException("A10", @"Login must be unique");
        }
    }
}