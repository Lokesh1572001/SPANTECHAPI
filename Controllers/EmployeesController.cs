using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPANTECH.Models;
using SPANTECH.Services;
using Microsoft.Extensions.Logging;

namespace SPANTECH.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<EmployeesController>
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service, ILogger<EmployeesController> logger)
            : base(logger)
        {
            _service = service;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            LogInformation("Fetching all employees.");

            var employees = await _service.GetAllEmployees();

            if (employees == null || !employees.Any())
            {
                LogWarning("No employees found.");
                return NotFound("No employees found.");
            }

            LogInformation("Employees fetched successfully.");
            return Ok(employees);
        }

        // Get employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            LogInformation($"Fetching employee with ID: {id}");

            var employee = await _service.GetEmployeeById(id);

            if (employee == null)
            {
                LogWarning($"Employee with ID: {id} not found.");
                return NotFound($"Employee with ID: {id} not found.");
            }

            LogInformation($"Employee with ID: {id} fetched successfully.");
            return Ok(employee);
        }

        // Add a new employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            LogInformation("Adding new employee.");

            if (employee == null)
            {
                LogWarning("Employee data is null.");
                return BadRequest("Employee data is null.");
            }

            await _service.AddEmployee(employee);

            LogInformation($"Employee with ID: {employee.Id} added successfully.");
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // Update employee details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            LogInformation($"Updating employee with ID: {id}");

            if (id != employee.Id)
            {
                LogWarning($"Employee ID mismatch. Given ID: {id}, Employee ID: {employee.Id}");
                return BadRequest("Employee ID mismatch.");
            }

            await _service.UpdateEmployee(employee);

            LogInformation($"Employee with ID: {id} updated successfully.");
            return NoContent();
        }

        // Delete employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            LogInformation($"Deleting employee with ID: {id}");

            var employee = await _service.GetEmployeeById(id);
            if (employee == null)
            {
                LogWarning($"Employee with ID: {id} not found for deletion.");
                return NotFound($"Employee with ID: {id} not found.");
            }

            await _service.DeleteEmployee(id);

            LogInformation($"Employee with ID: {id} deleted successfully.");
            return NoContent();
        }
    }
}
