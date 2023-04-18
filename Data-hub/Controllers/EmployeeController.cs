using Data_hub.Models;
using Data_hub.Services;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data_hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;
        private readonly IFirebaseClient _firebaseClient;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;

            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCMvss3zsD5GuSq\\nYiqB/1NKEXsfVaJW5KYboQyQYW5arbWPMuUga3oODdEQbqvsIKV3gWnDI1bC+pgL\\nwb947yxN/6HnzA/Njwy28PYKXrq5qwgSmX3zj9rHF0a+cMueRwJjHmVW4P2QWY3A\\nlwlA3q1mnOFGEGbds4rZSQBW3auf1M+YOpAq0X6NEWCRkJMFgrQOXTC/LsUsNQ6C\\nc80k5HAMWSwma72n3Hr92MjhdrplVJSK6KVfYjb0wCfty+OW6lTX09+THm6fbfd7\\nO9QKb22O/aW2GxwETvrSf0jnh3DAAmHKgKFU09EwHPoQA0/gMLQIAL5TSaZM7Kr2\\nwh8EiLZfAgMBAAECggEAFtOW1+1DZONUtMGJDV5gnsGpC1LzKk6ZFiAPjpcOs8w6\\nhwgFGzXzLLZW8uhQH9LHo1Ms2QYxOxwzbqy0piN8NwY/tv1kjr0lncIE1Xe5pwUz\\nIPkd8VOicum2gop+q6Puoi661tVUqoWrtNGKIag7zUmiS5+7XzeQTF9Hm88F170J\\ne91wDZf3A2Qsc8EYWtp7N9TRLupPXkzNLn9q4RIghdTr1vf59yEaoFhpbDuhDF5w\\ncV3o+/Ik5qaFOMDLxajBTDovjDg2iqellb5IgNhM7r1CCXEy25+eTZwclM5IJQqv\\n63TUJZ6NidIBGcCqSGFdftZRfu3o6fn00FbNrgy4mQKBgQDCKeu+5o/oATn3wpz5\\n1J1DlVWIKe/1PPLy/2xTMrDAh16YKaULyGywk7WKXHmhmOdmmoPjxn40y3z4/d3T\\n61QGsobJAg2s/zr0qrwm9UXl4oElMYgLPb9t/GCQdXzDejbVor5pE5v4CZEUDOHN\\nqnORGUR6nvjxKtuLfnJmuqwazQKBgQC5kbDSTTD6lkQJaMUD6cDBj0JzJQuZwKx0\\nmGDSo7uPJm8uhQ3FeblN3CfHHUb1fWpwM6FrA9M/uj6JuzUTS8/xGwd83sUDvg9o\\nUtN8c607fea49NRH1p0vHbDXAuqlGZpkDNyaONHYIUhqARCySFdIpRZ6ouqEuZCi\\nXXc+NHbt2wKBgQDBLVwzzskWl0HJU4NCvVrKRuWWV8M6R62gPqjUDfb1VrmywpxH\\nIN2iwRM52c/aC3sPBmR6Vp5ygJKSWGI/2j77etvHWZepqzZI/yW7zQQCOF0tWB52\\nsLSpBRQ+JeY/xnSQER9JBA2Ftl71h+uQ6CmbD3ymU0xzBaTxIJlJxCg/cQKBgBQm\\n4ynjYhdEbD0NWJ/VKa6bbR7t/fWDe/bpeVJGn56rENXfcyBn2JzA9LzlzAfqx71J\\njhT+BFneUt5IKzpeOEW5prWDx6dhY3Dz1a2lLkHQqVaal9b5UnaEZejjkzG8twx4\\nbRh+ZSNwcdm5JeQGgRwNeYR13rvtHuzS85kYv3WTAoGAYDyOY1hcUsEXC9ShoP3o\\nCFh0xXqIsH8BeExsVLuN+A5TQNPjsI9hbv0BOJoiMC/X3bcTnOc60zSUdZTw3BB1\\ndEBQJlVzWAc9RlE43b7JmxRiWinrv8wLe5nb+PHkNHyideocdXq6ars42GhJiqm8\\nVNFK6Ko3tlnPJvzlku9akCo=",
                BasePath = "https://console.firebase.google.com/project/gps3-669ff/database/gps3-669ff-default-rtdb/data/~2F"
            };

            _firebaseClient = new FireSharp.FirebaseClient(config);
        }



        // POST: api/PostEmployee
        /// <summary>
        /// Creates a new employee in the Firebase Realtime database.
        /// </summary>
        /// <remarks>
        /// This should only be used when an employee does not exist in the database.
        /// </remarks>
        /// <param>Specifies the parameters</param>
        /// <response code="200">The employee was successfully created</response>
        /// <response code="400">An error was made with the given parameters. More context will be given in the body</response>
        [HttpPost]
        public async Task <IActionResult>PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SetResponse response = await _firebaseClient.SetAsync($"users/{Guid.NewGuid()}", employee);
            Employee newUser = response.ResultAs<Employee>();

            return CreatedAtAction(nameof(PostEmployee), newUser);


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
