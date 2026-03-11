# Alza_CaseStudy

This repository contains a simple case study application with a Web API, persistence layer, and domain models. It demonstrates basic CRUD operations for products and includes unit and integration tests.

## 📋 Prerequisites

Before running the project, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A C#-compatible IDE or editor (e.g., Visual Studio, VS Code)

> **Note:** The solution was built and tested on Windows but should work on other platforms supported by .NET 8.

## 🚀 Running the Application

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Alza_CaseStudy
   ```

2. **Navigate to the Web API project**
   ```bash
   cd src/CaseStudy.WebApi
   ```

3. **Restore dependencies and build**
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

   The API will start on `https://localhost:5001` (or a port indicated in `launchSettings.json`).

5. **Use the HTTP request file**
   - An example `.http` file (`CaseStudyRequests.http`) lives in the WebApi project. You can open it in VS Code with the REST Client extension to execute sample requests.

## 🧪 Running Tests

The solution includes both unit and integration tests located in `tests/CaseStudy.Test`.

To execute all tests, run the following from the repository root:

```bash
cd tests/CaseStudy.Test
dotnet test
```

The tests are grouped into `UnitTests` and `IntegrationTests` folders. You can run them individually by specifying the project or filtering by test names.

---

For more details or contributions, refer to the source code and project folders.
