using Data_hub.Models;
using Data_hub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data_hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
         [HttpPost]
        public async Task PostEmployee(Employee employee)
        {
            await employeeService.AddEmployee(employee);
        }

        [HttpGet("{name}")]
        public async Task<Employee> GetEmployee(string name)
        {
            Employee emp = await employeeService.GetEmployee(name);
            return emp;
        }

        [HttpGet("All")]
        public async Task<Dictionary<string, Employee>> GetEmployees()
        {
            Dictionary<string, Employee> emp = await employeeService.GetEmployees();
            return emp;
        }
    }
}
