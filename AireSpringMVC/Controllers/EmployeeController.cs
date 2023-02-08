using AireSpringMVC.common.models;
using AireSpringMVC.workflows.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AireSpringMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeWorkflow _employeeWorkflow;
        public EmployeeController(IEmployeeWorkflow employeeWorkflow)
        {
            //inject Employee workflow
            this._employeeWorkflow = employeeWorkflow;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employeesList = await this._employeeWorkflow.GetEmployees();
             return View(employeesList);
        }


        // GET: EmployeeController/Filter/{criteria}
        public async Task<ActionResult> Filter(string criteria)
        {
            var employeesList = await this._employeeWorkflow.FilterEmployee(criteria);
            return View(nameof(Index), employeesList);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,LastName,Phone,ZipCode,HireDate")] Employee employee)
        {
            try
            {
                this._employeeWorkflow.addEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
