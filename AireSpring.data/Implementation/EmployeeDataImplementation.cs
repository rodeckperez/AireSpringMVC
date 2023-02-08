using AireSpringMVC.common.models;
using AireSpringMVC.data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace AireSpringMVC.data.Implementation
{
    public class EmployeeDataImplementation : IEmployeeData
    {
        //private properties
        private readonly IConfiguration _configuration;
        private List<SqlParameter> sqlParameters = new List<SqlParameter>();
        private string sqlSpName = string.Empty;
        private Conection<Employee> conection;

        public EmployeeDataImplementation(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.conection = new Conection<Employee>(configuration);
        }
        public async void addEmployee(Employee employeeInputModel)
        {
            this.sqlSpName = "sp_createEmployee";
            this.sqlParameters.Add(new SqlParameter("@name", employeeInputModel.Name));
            this.sqlParameters.Add(new SqlParameter("@lastName", employeeInputModel.LastName));
            this.sqlParameters.Add(new SqlParameter("@phone", employeeInputModel.Phone));
            this.sqlParameters.Add(new SqlParameter("@zipCode", employeeInputModel.ZipCode));
            this.sqlParameters.Add(new SqlParameter("@hiringDate", employeeInputModel.HireDate));

            this.conection.setCommand(this.sqlSpName, this.sqlParameters);
            await this.conection.executeNonQuery();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            this.sqlSpName = "sp_getEmployees";
            this.sqlParameters = new List<SqlParameter>();
            this.conection.setCommand(this.sqlSpName, this.sqlParameters);
            var employeesList = await this.conection.getReader();
            return employeesList;
        }
    }
}
