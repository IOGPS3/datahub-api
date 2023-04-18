# ASP.NET Web API with .NET 6, FluentValidation, and Firebase Realtime Database

This project is an ASP.NET Web API built with .NET 6 that demonstrates how to perform data validation using FluentValidation and interact with Firebase Realtime Database.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- A Firebase project with Realtime Database enabled

## Getting Started

1. Clone the repository:
```
git clone https://github.com/IOGPS3/datahub-api
cd datahub-api
```

2. Install the required NuGet packages:
dotnet add package FireSharp
dotnet add package FluentValidation.AspNetCore

3. Set up your Firebase credentials:

- Go to your [Firebase Console](https://console.firebase.google.com/), navigate to your project's settings, and find your Firebase Auth Secret.
- Create an environment variable, for example `FIREBASE_AUTH_SECRET`, and set its value to your Firebase Auth Secret.
- Replace the placeholders in the `Controllers/UsersController.cs` file with your actual Firebase project details, and use the environment variable you created to securely store the Auth Secret.

4. Build and run the project:
```cmd
dotnet build
dotnet run
```

## API Endpoints

### POST /api/PostEmployee

Create a new user with the following JSON request body:

```json
{
"Name": "John Doe",
"Email": "john.doe@example.com",
"Location": "Office",
"MeetingStatus": "available",
"Favorites": ["user1", "user3"]
}
```
If the request is successful, a 201 Created response will be returned, along with the newly created user object.

## Project Structure
- Program.cs: Contains the configuration and setup for the API.
- Controllers/EmployeeController.cs: Contains the logic for handling user-related API requests.
- Models/Employee.cs: Defines the Employee model.
- Models/EmployeeValidator.cs: Defines the validation rules for the Employee model using FluentValidation.
 
## Technologies Used
- .NET 6: The framework used to build the Web API.
- ASP.NET Core: The web framework used for creating the API.
- FluentValidation: A library used to validate the User model.
- Firebase Realtime Database: A cloud-hosted NoSQL database used to store user data.
- FireSharp: A Firebase SDK for .NET used to interact with the Firebase Realtime Database.
