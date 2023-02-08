using AireSpringMVC.common.models;

namespace AireSpringMVC.data.Interfaces
{
    public interface IEmployeeData
    {
        void addEmployee(Employee employeeInputModel);
        Task<List<Employee>> GetEmployees();
    }
}
