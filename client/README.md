# NashStore Frontend (React)

React-based frontend application for the NashStore e-commerce platform.

## ��� Quick Start

### Development Mode
```bash
# Install dependencies
npm install

# Start development server
npm start

# Access application at http://localhost:3000
```

### Production Build
```bash
# Build for production
npm run build
```

## ��� Project Structure
```text
src/
├── components/          # Reusable UI components
├── pages/              # Page components
│   ├── Auth/           # Login, Register, Profile pages
│   ├── Home/           # Product listing and shopping
│   ├── Cart/           # Shopping cart
│   └── AdminManage/    # Admin management pages
├── store/              # Redux store configuration
│   ├── actions/        # Redux actions
│   ├── reducers/       # Redux reducers
│   └── store.js        # Store configuration
├── shared/             # Shared utilities and helpers
│   ├── components/     # Common components
│   └── helpers/        # Utility functions
└── App.js              # Main application component
```

## ��� Configuration

### Environment Variables
```bash
REACT_APP_API_BASE_URL=http://localhost:5083/api
REACT_APP_NODE_ENV=development
```

### API Proxy Configuration
The development server proxies API requests to the backend:
- API calls to `/api/*` are forwarded to `http://localhost:5083`

## ��� Available Scripts
- `npm start` - Start development server with hot reload
- `npm run build` - Build for production
- `npm test` - Run test suite
- `npm run eject` - Eject from Create React App (irreversible)

## ��� UI Components

### User Interface
- Login form with validation
- Registration with role selection
- Password change functionality
- User avatar upload

### Product Catalog
- Product grid with pagination
- Search and filtering
- Category navigation
- Product detail views

### Shopping Cart
- Add/remove items
- Quantity management
- Checkout process
- Order history

### Admin Panel
- Product management (CRUD)
- Category management
- User management
- Order management

## ��� API Integration

### Authentication
```javascript
// Login
POST /api/auth/login

// Register
POST /api/auth/register

// Get current user
GET /api/users/current
```

### Products
```javascript
// Get products with pagination
GET /api/products?page=1&pageSize=10&searchTerm=keyword&categoryId=1

// Get single product
GET /api/products/:id
```

### Categories
```javascript
// Get all categories
GET /api/categories
```

## ���️ Technologies Used
- **React 18** with Hooks
- **Redux** for state management  
- **React Router** for navigation
- **Axios** for HTTP requests
- **React Hook Form** for form validation
- **JWT-decode** for token handling
- **Moment.js** for date formatting
- **React Toastify** for notifications

## ��� Docker Support

### Development
```bash
# Build and run with hot reload
docker-compose -f ../docker-compose.dev.yml up client-dev
```

### Production
```bash
# Build optimized container
docker build -t nashstore-client .
docker run -p 3000:3000 nashstore-client
```

## ��� Features

### User Features
- Browse products by category
- Search products with autocomplete
- Add items to shopping cart
- Secure checkout process
- View order history
- Manage user profile

### Admin Features
- Dashboard with analytics
- Complete product management
- Category and brand management
- User account management
- Order processing and status updates

## ��� Authentication Flow
1. User enters credentials
2. Frontend sends request to `/api/auth/login`
3. Backend validates and returns JWT token
4. Token stored in localStorage
5. Token sent in Authorization header for protected routes
6. Auto-logout on token expiration

## ��� Error Handling
- Global error boundaries for React components
- API error interceptors with user-friendly messages
- Form validation with real-time feedback
- Network error handling with retry options

## ��� State Management

### Redux Store Structure
```javascript
{
  auth: {
    user: {},
    isAuthenticated: boolean,
    loading: boolean
  },
  products: {
    items: [],
    loading: boolean,
    pagination: {}
  },
  cart: {
    items: [],
    total: number
  },
  categories: {
    items: []
  }
}
```

## ��� Testing
```bash
# Run all tests
npm test

# Run tests in watch mode
npm test -- --watch

# Generate coverage report
npm test -- --coverage
```

## ��� Best Practices
- Component-based architecture
- Proper state management with Redux
- Form validation with React Hook Form
- Responsive design principles
- Accessibility considerations
- Performance optimization with React.memo
- Code splitting for better loading times
