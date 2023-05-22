using Data_hub.Models;
using Data_hub.Services;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Data_hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;
        private readonly IFirebaseClient _firebaseClient;
        public const string ADD = "Add"; 
        public const string DELETE = "Delete";
        Employee currentUser = null;
        Employee coworker = null;
        string exceptionMessage = null;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;

            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCMvss3zsD5GuSq\\nYiqB/1NKEXsfVaJW5KYboQyQYW5arbWPMuUga3oODdEQbqvsIKV3gWnDI1bC+pgL\\nwb947yxN/6HnzA/Njwy28PYKXrq5qwgSmX3zj9rHF0a+cMueRwJjHmVW4P2QWY3A\\nlwlA3q1mnOFGEGbds4rZSQBW3auf1M+YOpAq0X6NEWCRkJMFgrQOXTC/LsUsNQ6C\\nc80k5HAMWSwma72n3Hr92MjhdrplVJSK6KVfYjb0wCfty+OW6lTX09+THm6fbfd7\\nO9QKb22O/aW2GxwETvrSf0jnh3DAAmHKgKFU09EwHPoQA0/gMLQIAL5TSaZM7Kr2\\nwh8EiLZfAgMBAAECggEAFtOW1+1DZONUtMGJDV5gnsGpC1LzKk6ZFiAPjpcOs8w6\\nhwgFGzXzLLZW8uhQH9LHo1Ms2QYxOxwzbqy0piN8NwY/tv1kjr0lncIE1Xe5pwUz\\nIPkd8VOicum2gop+q6Puoi661tVUqoWrtNGKIag7zUmiS5+7XzeQTF9Hm88F170J\\ne91wDZf3A2Qsc8EYWtp7N9TRLupPXkzNLn9q4RIghdTr1vf59yEaoFhpbDuhDF5w\\ncV3o+/Ik5qaFOMDLxajBTDovjDg2iqellb5IgNhM7r1CCXEy25+eTZwclM5IJQqv\\n63TUJZ6NidIBGcCqSGFdftZRfu3o6fn00FbNrgy4mQKBgQDCKeu+5o/oATn3wpz5\\n1J1DlVWIKe/1PPLy/2xTMrDAh16YKaULyGywk7WKXHmhmOdmmoPjxn40y3z4/d3T\\n61QGsobJAg2s/zr0qrwm9UXl4oElMYgLPb9t/GCQdXzDejbVor5pE5v4CZEUDOHN\\nqnORGUR6nvjxKtuLfnJmuqwazQKBgQC5kbDSTTD6lkQJaMUD6cDBj0JzJQuZwKx0\\nmGDSo7uPJm8uhQ3FeblN3CfHHUb1fWpwM6FrA9M/uj6JuzUTS8/xGwd83sUDvg9o\\nUtN8c607fea49NRH1p0vHbDXAuqlGZpkDNyaONHYIUhqARCySFdIpRZ6ouqEuZCi\\nXXc+NHbt2wKBgQDBLVwzzskWl0HJU4NCvVrKRuWWV8M6R62gPqjUDfb1VrmywpxH\\nIN2iwRM52c/aC3sPBmR6Vp5ygJKSWGI/2j77etvHWZepqzZI/yW7zQQCOF0tWB52\\nsLSpBRQ+JeY/xnSQER9JBA2Ftl71h+uQ6CmbD3ymU0xzBaTxIJlJxCg/cQKBgBQm\\n4ynjYhdEbD0NWJ/VKa6bbR7t/fWDe/bpeVJGn56rENXfcyBn2JzA9LzlzAfqx71J\\njhT+BFneUt5IKzpeOEW5prWDx6dhY3Dz1a2lLkHQqVaal9b5UnaEZejjkzG8twx4\\nbRh+ZSNwcdm5JeQGgRwNeYR13rvtHuzS85kYv3WTAoGAYDyOY1hcUsEXC9ShoP3o\\nCFh0xXqIsH8BeExsVLuN+A5TQNPjsI9hbv0BOJoiMC/X3bcTnOc60zSUdZTw3BB1\\ndEBQJlVzWAc9RlE43b7JmxRiWinrv8wLe5nb+PHkNHyideocdXq6ars42GhJiqm8\\nVNFK6Ko3tlnPJvzlku9akCo=",
                BasePath = "https://gps3-669ff-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            _firebaseClient = new FireSharp.FirebaseClient(config);
        }


        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SetResponse response = await _firebaseClient.SetAsync($"users/{Guid.NewGuid()}", employee);
            Employee newUser = response.ResultAs<Employee>();

            return CreatedAtAction(nameof(PostEmployee), newUser);


            
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEntireEmployee(string unique, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FirebaseResponse response = await _firebaseClient.UpdateAsync($"users/{unique}", employee);
            Employee updatedUser = response.ResultAs<Employee>();

            return CreatedAtAction(nameof(UpdateEntireEmployee), updatedUser);
        }


        [HttpPatch("{unique}/MeetingStatus")]
        public async Task<IActionResult> UpdateEmployeeMeeting(string unique, string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dictionary<string, object> updatedStatus = new Dictionary<string, object>();
            updatedStatus.Add("MeetingStatus", status);

            FirebaseResponse response = await _firebaseClient.UpdateAsync($"users/{unique}", updatedStatus);
            Employee updatedUser = response.ResultAs<Employee>();

            return CreatedAtAction(nameof(UpdateEmployeeMeeting), updatedUser);
        }

        [HttpGet("{unique}")]
        public async Task<IActionResult> GetEmployee([FromRoute] string unique)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FirebaseResponse response = await _firebaseClient.GetAsync($"users/{unique}");
            Employee receivedUser = response.ResultAs<Employee>();

            return CreatedAtAction(nameof(GetEmployee), receivedUser);
        }

        [HttpPatch("{unique}/Favorites/Add")]
        public async Task<IActionResult> AddCoworkerToFavorite(string coworkerEmail, string unique)
        {
            if (!ModelState.IsValid || coworkerEmail == null)
            {
                return BadRequest(ModelState);
            }

            FirebaseResponse employeesResponse = await _firebaseClient.GetAsync($"users");
           
            if (employeesResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!IsCoworkerExist(employeesResponse, unique, coworkerEmail,ADD))
                {
                    return BadRequest(exceptionMessage);
                }
            }
            FavoriteCoworker? favoriteUser = null;
            if (coworker != null)
            {
                favoriteUser = new FavoriteCoworker();
                favoriteUser.UserName = coworker.Name;
                favoriteUser.Email = coworker.Email;
            }
            int favCount = 0;
            if (favoriteUser != null)
            {
                if(currentUser.Favorites?.Count > 0)
                {
                    favCount = currentUser.Favorites.Count;
                }

                Dictionary<int, object> updatedFavorite = new Dictionary<int, object>();
                updatedFavorite.Add(favCount, favoriteUser);

                FirebaseResponse response = await _firebaseClient.UpdateAsync($"users/{unique}/Favorites", updatedFavorite);
                Employee updatedUser = response.ResultAs<Employee>();

                return CreatedAtAction(nameof(AddCoworkerToFavorite), updatedUser);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPatch("{unique}/Favorites/remove")]
        public async Task<IActionResult> RemoveCoworkerFromFavorite(string coworkerEmail, string unique)
        {
            if (!ModelState.IsValid || coworkerEmail == null)
            {
                return BadRequest(ModelState);
            }
            FirebaseResponse response = await _firebaseClient.GetAsync($"users");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!IsCoworkerExist(response, unique, coworkerEmail, DELETE))
                {
                    return BadRequest(exceptionMessage);
                }
            }
            List<FavoriteCoworker> temp = currentUser.Favorites;
            int fav= currentUser.Favorites.FindIndex(x=>x.Email == coworkerEmail);

            FirebaseResponse deleteResponse = await _firebaseClient.DeleteAsync($"users/{unique}/Favorites/{fav}");
            FavoriteCoworker deletedUser = deleteResponse.ResultAs<FavoriteCoworker>();

            return CreatedAtAction(nameof(RemoveCoworkerFromFavorite), deletedUser);
        }

        private bool IsCoworkerExist(FirebaseResponse response, string unique, string coworkerEmail, string action) {

            Dictionary<string, Employee> employees = response.ResultAs<Dictionary<string, Employee>>();

            currentUser = employees.FirstOrDefault(x => x.Key ==unique).Value;

            coworker = employees.FirstOrDefault(x => x.Value.Email == coworkerEmail).Value;

            if (coworker == null || currentUser == null)
            {
                exceptionMessage = "Either the unique ID or co-worker's email doesn't exist!";
                return false;
            }
            if (action == ADD)
            {
                if (currentUser.Favorites?.Find(x => x.Email == coworkerEmail) != null)
                {
                    exceptionMessage = "Co-worker already exists in your favorites!";
                    return false;
                }
                if (currentUser.Email == coworkerEmail)
                {
                    exceptionMessage = "A logged in user cannot add him/her-self as favorites!";
                    return false;
                }
            }
            if (action == DELETE)
            {
                if (currentUser.Favorites?.Find(x => x.Email == coworkerEmail) == null)
                {
                    exceptionMessage = "The entered coworker is not the favorites list!";
                    return false;
                }
            }
            return true;
        }
    }

}

