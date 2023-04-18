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

        [HttpPatch]
        public async Task UpdateEmployee(string name, Employee employee)
        {
            await employeeService.AddEmployee(employee);
        }

        [HttpGet("{name}")]
        [HttpGet("update/{name}")]
        public async Task<Employee> GetEmployee([FromRoute]string name, Employee employee)
        {
            Employee emp = await employeeService.GetEmployee(name);
            return emp;
        }

        [HttpGet("search/{email}")]
        public async Task<Employee> GetEmployeeOnEmail(string email)
        {
            Employee emp = await employeeService.GetEmployeeOnEmail(email);
            return emp;
        }

        [HttpGet("All")]
        public async Task<Dictionary<string, Employee>> GetEmployees()
        {
            Dictionary<string, Employee> emp = await employeeService.GetEmployees();
            return emp;
        }

        [HttpPatch("{name}/meetingStatus={status}")]
        public async Task<Employee> postMeetingDataForSpecificUser([FromRoute]string name, [FromRoute] string status)
        {
            var updatedData = await employeeService.updateMeetingData(name, status);
            return updatedData;
        }
    }
}
