using EmployeeManagement.API.Interfaces;
using EmployeeManagement.API.Models;
using EmployeeManagement.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Department GetDepartment(int departmentId)
        {
            return appDbContext.Departments
                .FirstOrDefault(d => d.DepartmentId == departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return appDbContext.Departments;
        }
    }
}
