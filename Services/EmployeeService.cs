using SPANTECH.Models;
using SPANTECH.Repositories;

namespace SPANTECH.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _repository.GetAllEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _repository.GetEmployeeById(id);
        }

        public async Task AddEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _repository.UpdateEmployee(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            await _repository.DeleteEmployee(id);
        }
    }
}
