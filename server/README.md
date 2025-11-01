# NashStore Backend (.NET)

ASP.NET Core 9 Web API backend for the NashStore e-commerce platform, built with Clean Architecture principles, Entity Framework Core, PostgreSQL, and JWT authentication.

## 🚀 Quick Start

### Prerequisites

- **.NET 9 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **PostgreSQL 15+** - [Download here](https://www.postgresql.org/download/)
- **EF Core CLI Tools** - Install with: `dotnet tool install --global dotnet-ef`

### Development Setup

```bash
# 1. Navigate to server directory
cd server

# 2. Restore dependencies
dotnet restore

# 3. Update database connection string in Api/appsettings.json
# 4. Run database migrations
dotnet ef database update --project Infrastructure --startup-project Api

# 5. Start the API
dotnet run --project Api

# API will be available at: https://localhost:5083
```

### Docker Setup

```bash
# From server directory
docker build -t nashstore-api .
docker run -p 5083:5083 nashstore-api

# Or use docker-compose from root
docker-compose up api
```

## 📁 Project Architecture

The backend follows **Clean Architecture** principles with clear separation of concerns:

```
server/
├── 📁 Api/                   # 🌐 Presentation Layer
│   ├── Controllers/          # REST API endpoints
│   │   ├── AuthController.cs
│   │   ├── ProductController.cs
│   │   ├── CategoriesController.cs
│   │   ├── OrdersController.cs
│   │   └── UserController.cs
│   ├── Program.cs           # Application startup & configuration
│   ├── appsettings.json     # Configuration settings
│   └── Api.csproj
│
├── 📁 Application/           # 🧠 Business Logic Layer
│   ├── DTOs/                # Data Transfer Objects
│   │   ├── ProductDto.cs
│   │   └── UserDto.cs
│   ├── Interfaces/          # Service contracts
│   │   ├── IAuthService.cs
│   │   ├── IProductService.cs
│   │   ├── ICategoryService.cs
│   │   ├── IOrderService.cs
│   │   └── IUserService.cs
│   ├── Services/            # Business logic implementation
│   │   ├── AuthService.cs
│   │   ├── ProductService.cs
│   │   ├── CategoryService.cs
│   │   ├── OrderService.cs
│   │   └── UserService.cs
│   └── Application.csproj
│
├── 📁 Domain/                # 🏛️ Core Domain Layer
│   ├── Entities/            # Domain entities
│   │   ├── BaseEntity.cs
│   │   ├── User.cs
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Order.cs
│   │   └── OrderDetail.cs
│   ├── Interfaces/          # Repository contracts
│   │   ├── IUserRepository.cs
│   │   ├── IProductRepository.cs
│   │   ├── ICategoryRepository.cs
│   │   └── IOrderRepository.cs
│   └── Domain.csproj
│
├── 📁 Infrastructure/        # 🔧 Data Access Layer
│   ├── Data/                # Database context
│   │   └── AppDbContext.cs
│   ├── Migrations/          # EF Core migrations
│   ├── Repositories/        # Repository implementations
│   │   ├── UserRepository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── OrderRepository.cs
│   ├── DependencyInjection.cs # Service registration
│   └── Infrastructure.csproj
│
└── server.sln               # Solution file
```

## 🔧 Configuration

### Database Connection

Update the connection string in `Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=nashstore_db;Username=postgres;Password=yourpassword"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyForJWTTokenGeneration123456789",
    "Issuer": "NashStore",
    "Audience": "NashStoreUsers",
    "TokenLifetimeMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Environment Variables

For production, use environment variables:

```bash
export ConnectionStrings__DefaultConnection="Host=prod-host;Port=5432;Database=nashstore_db;Username=prod_user;Password=prod_password"
export JwtSettings__SecretKey="your-production-secret-key"
```

## 📊 Database Schema

### Core Entities

**Users Table:**
- Id (Primary Key)
- Email (Unique)
- PasswordHash
- FirstName, LastName
- Role (Admin, Customer)
- Created/Updated timestamps

**Products Table:**
- Id (Primary Key)
- Name, Description
- Price, Quantity
- CategoryId (Foreign Key)
- ImageUrl
- Created/Updated timestamps

**Categories Table:**
- Id (Primary Key)
- Name, Description
- Created/Updated timestamps

**Orders Table:**
- Id (Primary Key)
- UserId (Foreign Key)
- TotalAmount
- Status (Pending, Processing, Shipped, Delivered)
- Created/Updated timestamps

**OrderDetails Table:**
- Id (Primary Key)
- OrderId (Foreign Key)
- ProductId (Foreign Key)
- Quantity, UnitPrice

## 🔌 API Endpoints

### Authentication
```
POST   /api/auth/login          # User login
POST   /api/auth/register       # User registration
```

### Users
```
GET    /api/users/current       # Get current user profile
PUT    /api/users/profile       # Update user profile
POST   /api/users/change-password # Change password
GET    /api/users               # Get all users (Admin only)
```

### Products
```
GET    /api/products             # Get products (with pagination/filtering)
GET    /api/products/{id}        # Get single product
POST   /api/products             # Create product (Admin only)
PUT    /api/products/{id}        # Update product (Admin only)
DELETE /api/products/{id}        # Delete product (Admin only)
```

### Categories
```
GET    /api/categories           # Get all categories
POST   /api/categories           # Create category (Admin only)
PUT    /api/categories/{id}      # Update category (Admin only)
DELETE /api/categories/{id}      # Delete category (Admin only)
```

### Orders
```
GET    /api/orders               # Get user orders
POST   /api/orders               # Create new order
GET    /api/orders/{id}          # Get order details
PUT    /api/orders/{id}/status   # Update order status (Admin only)
```

## 🗃️ Database Migrations

### Create New Migration

```bash
# Navigate to server directory
cd server

# Create migration
dotnet ef migrations add MigrationName --project Infrastructure --startup-project Api

# Apply migration to database
dotnet ef database update --project Infrastructure --startup-project Api
```

### Data Seeding

The application automatically seeds the database with sample data on first run:

**Demo Users:**
- **Admin:** admin@nashstore.com / Admin123!
- **Customer:** customer@nashstore.com / Customer123!
- **Customer 2:** jane.smith@example.com / Password123!

**Sample Data:**
- **5 Categories:** Electronics, Clothing, Books, Sports, Home
- **15+ Products:** iPhone, MacBook, Nike shoes, books, fitness equipment, etc.
- **3 Sample Orders:** With realistic order details and customer information

*Note: Seeding only occurs when tables are empty, so it's safe to run multiple times.*

### Useful Migration Commands

```bash
# List migrations
dotnet ef migrations list --project Infrastructure --startup-project Api

# Remove last migration (if not applied)
dotnet ef migrations remove --project Infrastructure --startup-project Api

# Generate SQL script
dotnet ef migrations script --project Infrastructure --startup-project Api

# Drop database (development only)
dotnet ef database drop --project Infrastructure --startup-project Api
```

## 🛠️ Development Commands

### Building

```bash
# Build solution
dotnet build

# Build specific project
dotnet build Api/Api.csproj

# Build for production
dotnet build --configuration Release
```

### Running

```bash
# Run API in development
dotnet run --project Api

# Run with specific environment
dotnet run --project Api --environment Development

# Watch for changes (hot reload)
dotnet watch run --project Api
```

### Testing

```bash
# Run tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test Tests/Api.Tests
```

## 🔒 Security Features

### JWT Authentication
- Secure token-based authentication
- Configurable token expiration
- Role-based authorization
- Automatic token validation

### Password Security
- BCrypt password hashing
- Password strength validation
- Secure password reset flow

### API Security
- CORS configuration for frontend
- Input validation and sanitization
- SQL injection prevention via EF Core
- Request rate limiting (configurable)

## 🚀 Deployment

### Production Build

```bash
# Build for production
dotnet publish Api/Api.csproj -c Release -o publish/

# Run published app
cd publish && dotnet Api.dll
```

### Docker Deployment

```bash
# Build Docker image
docker build -t nashstore-api .

# Run container
docker run -p 5083:5083 \
  -e ConnectionStrings__DefaultConnection="your-connection-string" \
  -e JwtSettings__SecretKey="your-secret-key" \
  nashstore-api
```

### Environment-Specific Settings

**Development:** `appsettings.Development.json`
**Production:** `appsettings.Production.json`

## 🚨 Troubleshooting

### Common Issues

**1. Database Connection Failed**
```bash
# Check PostgreSQL is running
sudo service postgresql status

# Test connection
psql -h localhost -U postgres -d nashstore_db

# Verify connection string format
"Host=localhost;Port=5432;Database=nashstore_db;Username=postgres;Password=yourpassword"
```

**2. Migration Errors**
```bash
# Reset migrations (development only)
dotnet ef database drop --project Infrastructure --startup-project Api
dotnet ef migrations remove --project Infrastructure --startup-project Api
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api
dotnet ef database update --project Infrastructure --startup-project Api
```

**3. JWT Token Issues**
```bash
# Verify JWT settings in appsettings.json
# Ensure SecretKey is at least 32 characters
# Check token expiration settings
```

**4. CORS Errors**
```bash
# Verify CORS policy in Program.cs
# Check allowed origins match frontend URL
# Ensure credentials are allowed if needed
```

### Logging

Enable detailed logging in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

## 📦 Package Dependencies

### Core Packages
- `Microsoft.AspNetCore.App` - ASP.NET Core framework
- `Microsoft.EntityFrameworkCore` - ORM framework
- `Npgsql.EntityFrameworkCore.PostgreSQL` - PostgreSQL provider
- `Microsoft.AspNetCore.Authentication.JwtBearer` - JWT authentication
- `BCrypt.Net-Next` - Password hashing

### Development Packages
- `Microsoft.EntityFrameworkCore.Design` - Design-time EF tools
- `Microsoft.EntityFrameworkCore.Tools` - EF migration tools

## 🎯 Best Practices

### Code Organization
- Follow Clean Architecture principles
- Use dependency injection for loose coupling
- Implement repository pattern for data access
- Separate DTOs from domain entities

### Security
- Always hash passwords with BCrypt
- Use JWT tokens for stateless authentication
- Implement proper authorization checks
- Validate all inputs and sanitize outputs

### Performance
- Use async/await for database operations
- Implement pagination for large datasets
- Use appropriate EF Core tracking behavior
- Consider caching for frequently accessed data

### Testing
- Write unit tests for business logic
- Create integration tests for API endpoints
- Use in-memory database for testing
- Mock external dependencies

