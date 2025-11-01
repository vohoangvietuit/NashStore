@echo off
echo 🚀 Starting NashStore Development Environment...

REM Check if PostgreSQL is running (simple check)
netstat -an | findstr ":5432" >nul
if errorlevel 1 (
    echo ❌ PostgreSQL is not running on port 5432
    echo Please start PostgreSQL first or use Docker Compose
    pause
    exit /b 1
)

echo ✅ PostgreSQL is running

REM Start .NET API
echo 🔧 Starting .NET API...
start "NashStore API" cmd /k "cd server && dotnet run --project Api"

REM Wait a moment for API to start
timeout /t 5 /nobreak >nul

REM Start React client
echo ⚛️  Starting React Client...
start "NashStore Client" cmd /k "cd client && npm start"

echo 🎉 Both services are starting!
echo 📊 API: http://localhost:5083
echo 🌐 Client: http://localhost:3000
echo.
echo Press any key to exit...
pause >nul
