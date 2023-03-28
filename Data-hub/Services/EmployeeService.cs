using Data_hub.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace Data_hub.Services
{
    public class EmployeeService
    {
        private const string FirebaseDatabaseUrl = " https://gps3-669ff-default-rtdb.europe-west1.firebasedatabase.app/";


        private readonly FirebaseClient firebaseClient;

        public EmployeeService()
        {
            firebaseClient = new FirebaseClient(FirebaseDatabaseUrl);
        }

        public async Task AddEmployee(Employee user)
        {
            await firebaseClient
              .Child("users")
              .PostAsync(user);
        }
    }
}