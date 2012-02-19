using System.Collections.Generic;
using MvcArch.Domain;

namespace MvcArch.IServices
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> LoadAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
    }
}