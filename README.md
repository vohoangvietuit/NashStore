# NashStore - E-commerce Application

A modern full-stack e-commerce platform built with **React** frontend and **.NET Core** backend, using **PostgreSQL** as the database.

## 📖 Project Description

**NashStore** is a comprehensive e-commerce management system designed for online retail operations. The platform provides a complete solution for managing products, categories, orders, and users with role-based access control.

**Demo URLs:**
- Frontend: http://localhost:3000
- Backend API: http://localhost:5083
- Database: PostgreSQL on localhost:5432

**Account for Demo:**
- **Admin:** admin@nashstore.com - Pass: Admin123!
- **Customer:** customer@nashstore.com - Pass: Customer123!

*Note: Demo accounts and sample products are automatically created when you first run the application.*

**Story:** NashStore is an e-commerce platform that enables businesses to manage their online store efficiently. Each product has comprehensive properties (name, price, description, quantity, images) and is organized by categories and brands. The system supports two types of users: Customers can browse and purchase products, while Administrators have full CRUD permissions for managing the entire platform.

## ✨ Features

### **Authentication & Authorization**
- ✅ JWT-based authentication with .NET Identity
- ✅ Role-based authorization (Admin, Customer)
- ✅ Secure password hashing with bcrypt
- ✅ User profile management (change info, avatar)

### **Product Management**
- ✅ Full CRUD operations for products (Admin only)
- ✅ Product categorization and brand management
- ✅ Image upload for products
- ✅ Inventory quantity tracking
- ✅ Product search with autocomplete
- ✅ Advanced filtering and sorting

### **Shopping Experience**
- ✅ Shopping cart functionality (localStorage + Redux)
- ✅ Checkout process with order management
- ✅ Order history for customers
- ✅ Pagination for product listings
- ✅ Responsive design for mobile/desktop

### **Admin Panel**
- ✅ Dashboard with analytics
- ✅ Manage Products, Categories, Users, Orders
- ✅ Order status management
- ✅ User role assignment
- ✅ System configuration

### **Technical Features**
- ✅ Real-time data updates
- ✅ API versioning and documentation
- ✅ Database migrations with Entity Framework
- ✅ Docker containerization
- ✅ CORS configuration for cross-origin requests
- ✅ Input validation and error handling

## 🛠️ Technology Stack

### **Frontend:**
- **React 18** (with Hooks) + **Redux** for state management
- **React Router** for navigation
- **Axios** for API communication
- **React Hook Form** for form validation
- **Material-UI / Bootstrap** for UI components
- **JWT-decode** for token handling

### **Backend:**
- **.NET 9** with **ASP.NET Core Web API**
- **Entity Framework Core** for ORM
- **PostgreSQL** database
- **JWT Bearer Authentication**
- **AutoMapper** for object mapping
- **FluentValidation** for input validation

### **DevOps & Tools:**
- **Docker & Docker Compose** for containerization
- **Git** for version control
- **Visual Studio Code** as IDE
- **Postman** for API testing
- **pgAdmin** for database management

### **Deployment:**
- **Development:** Docker Compose with hot reload
- **Production:** Multi-stage Docker builds with Nginx

## 🏗️ Architecture

```
NashStore/
├── client/          # React frontend (Port 3000)
├── server/          # .NET API backend (Port 5083)
├── docker-compose.yml      # Production Docker setup
├── docker-compose.dev.yml  # Development Docker setup
└── README.md
```

## 🚀 Quick Start Options

### Option 1: Docker Compose (Recommended)

**For Development:**
```bash
# Start all services with hot reload
docker-compose -f docker-compose.dev.yml up --build

# Access:
# - Frontend: http://localhost:3000
# - Backend API: http://localhost:5083
# - PostgreSQL: localhost:5432
```

**For Production:**
```bash
# Start optimized production build
docker-compose up --build

# Stop services
docker-compose down

# Clean up (remove volumes)
docker-compose down -v --remove-orphans
```

### Option 2: NPM Scripts (Node.js Required)

```bash
# Install root dependencies
npm install

# Install all project dependencies
npm run install:all

# Start both frontend and backend
npm run dev

# Or start individually
npm run server  # .NET API only
npm run client  # React only
```

### Option 3: Manual Setup

**Prerequisites:**
- .NET 9 SDK
- Node.js 18+
- PostgreSQL 15+

**Setup Database:**
```bash
# Update connection string in server/Api/appsettings.json
# Then run migrations
npm run migrate
```

**Start Services:**
```bash
# Terminal 1: Start .NET API
cd server
dotnet run --project Api

# Terminal 2: Start React client
cd client
npm start
```

### Option 4: Platform Scripts

**Windows:**
```cmd
start-dev.bat
```

**Linux/macOS:**
```bash
chmod +x start-dev.sh
./start-dev.sh
```

## 🔧 First-Time Setup Guide

### Prerequisites

Before running NashStore for the first time, ensure you have:

1. **PostgreSQL 15+** installed and running
   - Default port: 5432
   - Create a database named `NashStoreDb`
   - Note your username/password for connection string

2. **.NET 9 SDK** installed
   - Download from: https://dotnet.microsoft.com/download
   - Verify: `dotnet --version`

3. **Node.js 18+** and npm installed
   - Download from: https://nodejs.org/
   - Verify: `node --version` and `npm --version`

### Step-by-Step Setup

#### 1. Clone and Install Dependencies

```bash
# Clone the repository
git clone <repository-url>
cd NashStore

# Install frontend dependencies
cd client
npm install

# Install backend dependencies (restore NuGet packages)
cd ../server/Api
dotnet restore
```

#### 2. Configure Database Connection

Edit `server/Api/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=NashStoreDb;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

⚠️ **Important:** Replace `YOUR_PASSWORD` with your actual PostgreSQL password.

#### 3. Run Database Migrations

```bash
# From the server directory
cd server
dotnet ef database update --project Infrastructure --startup-project Api
```

If you encounter migration errors:
```bash
# Create migration first (if needed)
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api

# Then update database
dotnet ef database update --project Infrastructure --startup-project Api
```

#### 4. Create Default Avatar Image

The application requires a default avatar image. Create it manually:

```bash
# Create uploads directory in client
mkdir -p client/public/uploads

# Copy an existing image or create a placeholder
# The app expects: client/public/uploads/default-avatar.jpg
```

You can copy any 150x150 image and rename it to `default-avatar.jpg`, or the app will show a broken image icon as fallback.

#### 5. Start the Application

**Option A: Start Both Services (Recommended)**

```bash
# Terminal 1: Start .NET Backend (from server/Api directory)
cd server/Api
dotnet run

# Terminal 2: Start React Frontend (from client directory)
cd client
npm start
```

**Option B: Use NPM Scripts (from root directory)**

```bash
# Install root dependencies first
npm install

# Start both services
npm run dev
```

#### 6. Verify Setup

1. **Backend API:** http://localhost:5083
   - Test endpoint: http://localhost:5083/api/categories
   - Should return a JSON array of categories

2. **Frontend:** http://localhost:3000
   - Should display the NashStore homepage
   - Login with demo accounts (auto-created on first run)

### Demo Accounts (Auto-Created)

The application automatically seeds demo data on first run:

- **Admin Account:**
  - Email: `admin@nashstore.com`
  - Password: `Admin123!`

- **Customer Account:**
  - Email: `customer@nashstore.com`
  - Password: `Customer123!`

### Common First-Time Issues & Solutions

#### Issue 1: "Unknown promise rejection reason" on Login

**Cause:** Field mapping mismatch between frontend and backend
**Solution:** Already fixed - frontend now sends `username` field instead of `email`

#### Issue 2: Category Selection Shows "Electronics" Instead of ID

**Cause:** Frontend was sending category names instead of IDs
**Solution:** Already fixed - SelectListGroup component now uses `id` field properly

#### Issue 3: Search Suggestions Error "category=null"

**Cause:** Frontend was sending string "null" instead of omitting parameter
**Solution:** Already fixed - URL building now properly handles null categories

#### Issue 4: Circular Reference Error in Products API

**Cause:** JSON serialization of Entity Framework navigation properties
**Solution:** Already fixed - added projection mapping and JSON cycle handling

#### Issue 5: Default Avatar 404 Error

**Cause:** Missing default avatar image
**Solution:** Create `client/public/uploads/default-avatar.jpg` or use the SVG fallback

#### Issue 6: Database Migration Errors

**Symptoms:** 
```
Unable to create an object of type 'AppDbContext'
```

**Solution:**
```bash
# Ensure you're in the server directory
cd server

# Add connection string to appsettings.json
# Then run migration with proper project references
dotnet ef database update --project Infrastructure --startup-project Api
```

#### Issue 7: Port Already in Use

**Symptoms:**
```
Failed to bind to address http://127.0.0.1:5083: address already in use
```

**Solution:**
```bash
# Kill existing process
# Windows:
taskkill //f //im Api.exe

# Linux/Mac:
pkill -f "dotnet.*Api"

# Or change port in Properties/launchSettings.json
```

#### Issue 8: Frontend Proxy Issues

**Symptoms:** API calls return 404 or CORS errors

**Solution:**
- Ensure backend is running on port 5083
- Check `client/package.json` has: `"proxy": "http://localhost:5083/"`
- Restart React development server after backend changes

### Verification Checklist

After setup, verify these features work:

- [ ] **Login:** Use admin@nashstore.com / Admin123!
- [ ] **Categories:** Dropdown shows 5 categories (Electronics, Clothing, etc.)
- [ ] **Products:** Home page displays 13 sample products
- [ ] **Search:** Type in search box shows suggestions
- [ ] **Category Filter:** Selecting category filters products
- [ ] **Cart:** Add products to cart functionality
- [ ] **Admin Panel:** Login as admin to access management features

### Development Tips

1. **Database Changes:** Always run migrations after pulling updates
2. **API Changes:** Restart backend server after code changes
3. **Frontend Changes:** React hot-reload handles most changes automatically
4. **CORS Issues:** Ensure backend CORS is configured for localhost:3000
5. **Authentication:** JWT tokens expire - re-login if API returns 401 errors

### Next Steps

Once setup is complete:
1. Explore the admin panel with admin account
2. Test the shopping flow with customer account
3. Review the codebase structure in `/server` and `/client`
4. Check API endpoints using tools like Postman
5. Customize the application for your needs

## 🔧 Development

### Database Migrations

```bash
# Create new migration
cd server
dotnet ef migrations add MigrationName --project Infrastructure --startup-project Api

# Apply migrations
npm run migrate
```

### Environment Variables

**Client (.env):**
```
REACT_APP_API_URL=http://localhost:5083
```

**Server (appsettings.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=NashStoreDb;Username=postgres;Password=yourpassword"
  }
}
```

## 🏗️ Building for Production

```bash
# Build everything
npm run build

# Build individually
npm run build:client
npm run build:server
```

## 🧪 Testing

```bash
# Run all tests
npm test

# Test individually
npm run test:client
npm run test:server
```

## 📊 Services

| Service | Development | Production | Description |
|---------|-------------|------------|-------------|
| React Frontend | http://localhost:3000 | http://localhost:3000 | User interface |
| .NET API | http://localhost:5083 | http://localhost:5083 | Backend API |
| PostgreSQL | localhost:5432 | localhost:5432 | Database |

## 🐳 Docker Commands Reference

```bash
# Development with hot reload
docker-compose -f docker-compose.dev.yml up --build

# Production optimized
docker-compose up --build

# Run in background
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Remove everything including volumes
docker-compose down -v --remove-orphans

# Rebuild specific service
docker-compose build api
docker-compose build client
```

## 🚨 Troubleshooting

### Database Connection Issues
1. Ensure PostgreSQL is running
2. Check connection string in `appsettings.json`
3. Run migrations: `npm run migrate`

### Port Conflicts
- API (5083): Check if port is in use
- Client (3000): React will auto-assign available port
- PostgreSQL (5432): Change port in connection string if needed

### Docker Issues
```bash
# Clean Docker system
docker system prune -a
docker volume prune

# Rebuild without cache
docker-compose build --no-cache
```

## 🛠️ Tech Stack

**Frontend:**
- React 18
- React Router
- Axios for API calls
- Material-UI/Bootstrap

**Backend:**
- .NET 9
- Entity Framework Core
- JWT Authentication
- PostgreSQL

**DevOps:**
- Docker & Docker Compose
- Nginx (production)
- npm scripts

## 📝 API Endpoints

- `POST /api/auth/login` - User authentication
- `GET /api/products` - Get products
- `GET /api/categories` - Get categories
- `POST /api/orders` - Create order
- `GET /api/users` - Get users (admin)

## 🤝 Contributing

1. Fork the repository
2. Create feature branch: `git checkout -b feature-name`
3. Commit changes: `git commit -am 'Add feature'`
4. Push to branch: `git push origin feature-name`
5. Submit pull request

## 📄 License

MIT License - see LICENSE file for details
