using Data_hub.Models;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Data_hub.Services
{
    public  class EmployeeService
    {
        private const string FirebaseDatabaseUrl = " https://gps3-669ff-default-rtdb.europe-west1.firebasedatabase.app/";


       
        static readonly HttpClient client = new HttpClient();

        public  async Task<Employee> AddEmployee(Employee employee)
        {
            string employeeJsonString = JsonSerializer.Serialize(employee);

            var payload = new StringContent(employeeJsonString, Encoding.UTF8, "application/json");
            string url = $"{FirebaseDatabaseUrl}" +
                        $"users.json";


            var httpResponseMessage = await client.PostAsync(url, payload);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Employee>(contentStream);
                return result;
            }

            return null;
        }

        public async Task<Employee> UpdateEmployee(string uniqueKey, Employee employee)
        {
            string employeeJsonString = JsonSerializer.Serialize(employee);

            var payload = new StringContent(employeeJsonString, Encoding.UTF8, "application/json");
            string url = $"{FirebaseDatabaseUrl}" +
                        $"users/" +
                        $"" + uniqueKey + ".json";


            var httpResponseMessage = await client.PatchAsync(url, payload);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Employee>(contentStream);
                return result;
            }

            return null;
        }

        public async Task<Employee> GetEmployee(string name)
        {
            string url = $"{FirebaseDatabaseUrl}" +
                        $"users/" +
                        $"" + name + ".json";


            var httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Employee>(contentStream);
                return result;
            }

            return null;
        }

        public async Task<Employee> GetEmployeeOnEmail(string email)
        {
            Dictionary<string, Employee> employees = await GetEmployees();

            //search within the list on employee with according email
            foreach (Employee employee in employees.Values)
            {
                if (employee.email.Equals(email))
                {
                    return employee;
                }
            }

            return null;
        }

        public async Task<Dictionary<string, Employee>> GetEmployees()
        {
            string url = $"{FirebaseDatabaseUrl}" +
                        $"users.json";


            var httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Dictionary<string, Employee>>(contentStream);
                return result;
            }

            return null;
        }

        public async Task<Employee> updateMeetingData(string user, string status)
        {
            string jsonString = JsonSerializer.Serialize(status);
            StringContent payload = new StringContent(jsonString, Encoding.UTF8, "application/json-patch+json");
            string url = $"{FirebaseDatabaseUrl}" +
            $"users/" +
            $"" + user + ".json";


            var httpResponseMessage = await client.PatchAsync(url, payload);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Employee>(contentStream);
                return result;
            }

            return null;
        }
    }

}