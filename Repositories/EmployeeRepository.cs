using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using SPANTECH.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPANTECH.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns> The List of all employees./// </returns>
        /// <exception cref="Exception"> Thrown when an error occurs while fetching employees. </exception>
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger service if available)
                throw new Exception("An error occurred while fetching employees.", ex);
            }
        }

        /// <summary>
        /// Searching for a specific employee by Id.
        /// </summary>
        /// <returns> Returns the EmployeeDetails based on Id./// </returns>
        /// <param name="id">The employee name</param>
        /// <exception cref="Exception"> Thrown when an error occurs while fetching employees. </exception>
        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {id} was not found.");
                }
                return employee;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while fetching the employee with ID {id}.", ex);
            }
        }

        /// <summary> 
        /// Adding Employee Information.
        /// </summary>
        /// <param name="employee">An instance of <see cref="Employee"/> containing the details of the employee to be added. </param>
        /// <returns> Returns the details of the added employee. </returns>
        /// <exception cref="Exception"> Thrown when an error occurs while adding the employee.</exception>

        public async Task AddEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee data cannot be null.");
                }
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the employee.", ex);
            }
        }


        /// <summary>
        /// Updating Employee Information.
        /// </summary>
        /// <returns>Update the  Employee Details./// </returns>
        /// <param name="employee">An instance of <see cref="Employee"/> containing the details of the employee to be added. </param>
        /// <exception cref="Exception"> Thrown when an error occurs while fetching employees. </exception>
        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee data cannot be null.");
                }

                var existingEmployee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == employee.Id);
                if (existingEmployee == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {employee.Id} was not found.");
                }

                // Attach the employee and mark it as modified
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here if a logging mechanism is implemented
                throw new Exception($"An error occurred while updating the employee with ID {employee.Id}.", ex);
            }
        }


        /// <summary>
        /// Deleting the employee by Id.
        /// </summary>
        /// <returns> Delete the EmployeeDetails based on Id./// </returns>
        /// <param name="id">The Employee Id.</param>
        /// <exception cref="Exception"> Thrown when an error occurs while fetching employees. </exception>
        public async Task DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {id} was not found.");
                }
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while deleting the employee with ID {id}.", ex);
            }
        }
    }
}
