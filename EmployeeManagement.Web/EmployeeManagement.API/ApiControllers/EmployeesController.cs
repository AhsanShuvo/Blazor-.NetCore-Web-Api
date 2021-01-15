using EmployeeManagement.API.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;

        public EmployeesController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _empRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            try
            {
                var result = await _empRepository.GetEmployee(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost] // If there is no declaration of ApiController we might need to use [FormBody]Employee employee
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                var emp = _empRepository.GetEmployeeByEmail(employee.Email);
                if(emp != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                }

                var createdEmployee = await _empRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}")] //If there is no declaration of ApiController we might need to use[FormBody] Employee employee
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await _empRepository.GetEmployee(id);

                if (employeeToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                return await _empRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _empRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await _empRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await _empRepository.Search(name, gender);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
