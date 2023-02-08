using AireSpringMVC.common.models;
using AireSpringMVC.data.Interfaces;
using AireSpringMVC.workflows.Interfaces;
using System.Collections.Generic;

namespace AireSpringMVC.workflows.Implementation
{
    public class EmployeeWorkflow : IEmployeeWorkflow
    {
        public readonly IEmployeeData _employeeData;
        public EmployeeWorkflow(IEmployeeData employeeData)
        {
            this._employeeData = employeeData ?? throw new ArgumentNullException(nameof(employeeData));
        }

        #region Public Methods
        public void addEmployee(Employee employeeInputModel)
        {
            _employeeData.addEmployee(employeeInputModel);
        }

        public async Task<List<Employee>> FilterEmployee(string criteria)
        {
            List<Employee> employees = new List<Employee>();
            var employeesList = await this._employeeData.GetEmployees();
            if (criteria == String.Empty || criteria == null)
            {
                return employeesList;
            }

            return employeesList.Where(x => x.Name.Contains(criteria) || x.Phone.Contains(criteria)).ToList();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employeesList = await _employeeData.GetEmployees();
            return employeesList;
        }
        #endregion
    }
}
