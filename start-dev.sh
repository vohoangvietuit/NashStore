#!/bin/bash

# NashStore Development Startup Script
echo "🚀 Starting NashStore Development Environment..."

# Check if PostgreSQL is running
if ! pg_isready -h localhost -p 5432 >/dev/null 2>&1; then
    echo "❌ PostgreSQL is not running on localhost:5432"
    echo "Please start PostgreSQL first or use Docker Compose"
    exit 1
fi

echo "✅ PostgreSQL is running"

# Start .NET API in background
echo "🔧 Starting .NET API..."
cd server
dotnet run --project Api &
API_PID=$!

# Wait a moment for API to start
sleep 5

# Start React client in background
echo "⚛️  Starting React Client..."
cd ../client
npm start &
CLIENT_PID=$!

echo "🎉 Both services are starting!"
echo "📊 API: http://localhost:5083"
echo "🌐 Client: http://localhost:3000"
echo ""
echo "Press Ctrl+C to stop both services"

# Function to kill both processes
cleanup() {
    echo "🛑 Stopping services..."
    kill $API_PID 2>/dev/null
    kill $CLIENT_PID 2>/dev/null
    echo "✅ Services stopped"
    exit 0
}

# Trap Ctrl+C
trap cleanup SIGINT

# Wait for processes
wait
