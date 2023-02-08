using AireSpringMVC.common.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireSpringMVC.workflows.Interfaces
{
    public interface IEmployeeWorkflow
    {
        void addEmployee(Employee employeeInputModel);
        Task<List<Employee>> GetEmployees();

        Task<List<Employee>> FilterEmployee(string criteria);
    }
}
