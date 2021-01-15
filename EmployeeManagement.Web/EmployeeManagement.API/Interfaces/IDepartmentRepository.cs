using EmployeeManagement.Models.Models;
using System.Collections.Generic;

namespace EmployeeManagement.API.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int departmentId);
    }
}
