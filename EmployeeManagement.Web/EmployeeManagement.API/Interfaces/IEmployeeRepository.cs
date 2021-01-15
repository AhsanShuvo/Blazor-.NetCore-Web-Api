using EmployeeManagement.Models;
using EmployeeManagement.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);
    }
}
