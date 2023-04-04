using Data_hub.Models;
using System.Text;
using System.Text.Json;

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
                        $"users/" +
                        $"user5.json";


            var httpResponseMessage = await client.PutAsync(url, payload);

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