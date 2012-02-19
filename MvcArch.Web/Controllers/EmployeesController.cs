using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using MvcArch.Core;
using MvcArch.Core.Extensions;
using MvcArch.Core.Log;
using MvcArch.IServices;
using MvcArch.Web.Models;

namespace MvcArch.Web.Controllers
{
    [Tracelog]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _empService;

        public EmployeesController(IEmployeeService empService)
        {
            _empService = empService;
        }

        //
        // GET: /Employees/

        public ViewResult Index()
        {
            IEnumerable<Domain.Employee> domainEmps = _empService.LoadAll();
            IEnumerable<Employee> viewLst = Mapper.Map<IEnumerable<Domain.Employee>, IEnumerable<Employee>>(domainEmps);
            return View(viewLst);
        }

        //
        // GET: /Employees/Details/5

        public ViewResult Details(int id)
        {
            Domain.Employee domainEmp = _empService.GetById(id);
            Employee viewEmp = Mapper.Map<Domain.Employee, Employee>(domainEmp);
            return View(viewEmp);
        }

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employees/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Domain.Employee domainEmp = Mapper.Map<Employee, Domain.Employee>(employee);

                if (domainEmp.IsValid())
                {
                    try
                    {
                        _empService.Add(domainEmp);
                        return RedirectToAction("Index");
                    }
                    catch (BusinessRuleException bex)
                    {
                        ModelState.MergeWithBusinessException(bex);
                    }
                }

                ModelState.MergeWithValidatableObject(domainEmp);
            }
            return View(employee);
        }

        //
        // GET: /Employees/Edit/5

        public ActionResult Edit(int id)
        {
            Domain.Employee domainEmp = _empService.GetById(id);
            Employee viewEmp = Mapper.Map<Domain.Employee, Employee>(domainEmp);

            return View(viewEmp);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Domain.Employee domainEmp = Mapper.Map<Employee, Domain.Employee>(employee);
                if (domainEmp.IsValid())
                {
                    try
                    {
                        _empService.Update(domainEmp);
                        return RedirectToAction("Index");
                    }
                    catch (BusinessRuleException bex)
                    {
                        ModelState.MergeWithBusinessException(bex);
                    }
                }
                ModelState.MergeWithValidatableObject(domainEmp);
            }
            return View(employee);
        }

        //
        // GET: /Employees/Delete/5

        public ActionResult Delete(int id)
        {
            Domain.Employee domainEmp = _empService.GetById(id);
            Employee viewEmp = Mapper.Map<Domain.Employee, Employee>(domainEmp);

            return View(viewEmp);
        }

        //
        // POST: /Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _empService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}