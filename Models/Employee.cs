using Microsoft.AspNetCore.Authorization;

namespace SPANTECH.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
}
