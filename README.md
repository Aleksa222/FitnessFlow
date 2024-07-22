# FitnessFlow
FitnessFlow is a web application designed to help users track and analyze their training activities. Created by Aleksa Vučković as an assignment for WeDoSoftware
# Features 
User Authentication: Secure login and user management. <br />
Training Log: Record details such as training type, duration, calories burned, intensity, and fatigue. <br />
Statistics : View summary statistics for selected periods. <br />
Responsive Design: Mobile-friendly interface for seamless use on various devices. <br />
# Technologies
Frontend: Angular <br />
Backend: ASP.NET Core <br />
Database: SQL Server <br />
ORM: Entity Framework Core <br />
UI Components: Angular Material, Tailwind CSS <br />
# Installation
**Prerequisites** <br />
.NET 6 or later <br />
Node.js and npm <br />
SQL Server or compatible database <br />
# Backend Setup
After cloning the repository and navigating to the backend directory first you need to restore the dependencies. <br />
Then you should update the connection string in appsettings.json to match your SQL Server configuration. <br />
Apply migrations to create the database schema with **dotnet ef database update**. <br />
# Frontend Setup
Navigate to the frontend directory and install the dependencies <br />
After that you are **ready to go!**
