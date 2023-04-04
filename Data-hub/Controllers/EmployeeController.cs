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
        //private readonly EmployeeService employeeService;

        public EmployeeController(/*EmployeeService employeeService*/)
        {
            //this.employeeService = employeeService;
        }

        // POST: api/PostEmployee
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param>Specifies the parameters</param>
        /// <response code="200">The employee was sucessfully created</response>
        /// <response code="400">An error was made with the given parameters. More context will be given in the body</response>
        [HttpPost]
        public async Task PostEmployee(/*Employee employee*/)
        {
            return;
            //await employeeService.AddEmployee(employee);
        }
    }
}
