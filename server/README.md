# NashStore Server

NashStore Server is a Clean Architecture-based .NET 9 Web API project that uses PostgreSQL for data storage, Entity Framework Core (EF Core) for data access, JWT for authentication, and BCrypt for password hashing.

---

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
- [EF Core Migrations](#ef-core-migrations)
- [Running the API](#running-the-api)
- [Configuration](#configuration)
- [Troubleshooting](#troubleshooting)

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- EF Core CLI tools (install via: `dotnet tool install --global dotnet-ef`)

---

## Project Structure

The solution is organized into multiple projects following Clean Architecture:

```
MyApi (Solution Folder)
├── MyApi.sln
├── Api              # Presentation Layer (Web API)
│   ├── Api.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   └── Controllers
│       ├── AuthController.cs
│       └── UsersController.cs
├── Application      # Business Logic Layer
│   ├── Application.csproj
│   ├── DTOs
│   │   └── UserDto.cs
│   ├── Interfaces
│   │   ├── IAuthService.cs
│   │   └── IUserService.cs
│   └── Services
│       ├── AuthService.cs
│       └── UserService.cs
├── Domain           # Core Entities & Business Rules
│   ├── Domain.csproj
│   └── Entities
│       └── User.cs
└── Infrastructure   # Data Access & External Services
    ├── Infrastructure.csproj
    ├── Data
    │   └── AppDbContext.cs
    ├── Repositories
    │   ├── IUserRepository.cs
    │   └── UserRepository.cs
    └── DependencyInjection.cs
```

---

## Setup Instructions

1. **Clone the Repository and Navigate to the Solution Root**

   ```sh
   git clone <repository-url>
   cd NashStore/server
   ```

2. **Create the Solution and Projects**

   ```sh
   dotnet new sln -n MyApi
   dotnet new webapi -n Api
   dotnet new classlib -n Application
   dotnet new classlib -n Domain
   dotnet new classlib -n Infrastructure
   dotnet sln add Api/Api.csproj Application/Application.csproj Domain/Domain.csproj Infrastructure/Infrastructure.csproj
   ```

3. **Add Project References**

   ```sh
   dotnet add Api/Api.csproj reference Application/Application.csproj
   dotnet add Application/Application.csproj reference Domain/Domain.csproj Infrastructure/Infrastructure.csproj
   dotnet add Infrastructure/Infrastructure.csproj reference Domain/Domain.csproj
   ```

4. **Install Required NuGet Packages**

   ```sh
   dotnet add Infrastructure/Infrastructure.csproj package Microsoft.EntityFrameworkCore Microsoft.EntityFrameworkCore.Design Npgsql.EntityFrameworkCore.PostgreSQL
   dotnet add Api/Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer Microsoft.EntityFrameworkCore.Design
   dotnet add Application/Application.csproj package BCrypt.Net-Next
   ```

5. **Configure Database in `appsettings.json`**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=MyApiDb;Username=postgres;Password=yourpassword"
     },
     "Jwt": {
       "Key": "MySuperSecretKey12345",
       "Issuer": "MyApi",
       "Audience": "MyApiUsers"
     }
   }
   ```

6. **Add Design-Time DbContext Factory** (Infrastructure/Data/AppDbContextFactory.cs)

   ```csharp
   using System.IO;
   using Microsoft.EntityFrameworkCore;
   using Microsoft.EntityFrameworkCore.Design;
   using Microsoft.Extensions.Configuration;
   using Infrastructure.Data;

   namespace Infrastructure
   {
       public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
       {
           public AppDbContext CreateDbContext(string[] args)
           {
               IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

               var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
               optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

               return new AppDbContext(optionsBuilder.Options);
           }
       }
   }
   ```

---

## EF Core Migrations

### Creating a Migration

```sh
   dotnet ef migrations add InitialCreate --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
```

### Updating the Database

```sh
   dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project Api/Api.csproj
```

---

## Running the API

```sh
   dotnet run --project Api/Api.csproj
```

---

## Troubleshooting

- **Ensure `.csproj` files use SDK-style:**
  ```xml
  <Project Sdk="Microsoft.NET.Sdk">
  ```

- **Check Dependencies:** Run `dotnet list package` in each project.

- **Verify DbContext Configuration:** Ensure `AppDbContextFactory` is correctly implemented.

Happy coding! 🚀

