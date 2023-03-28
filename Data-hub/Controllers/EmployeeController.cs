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
    }
}
