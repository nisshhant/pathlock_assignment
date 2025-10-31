
For Assigment 1 :- 
<img width="636" height="623" alt="image" src="https://github.com/user-attachments/assets/a4fc37bc-73a0-4737-bcdf-0b7404d5b735" />

I did this at the end so css isn't styling yet. 
It had all the fucntionilties asked. 


This is my Assignment 2 for the full-stack web development module — a Mini Project Manager built using .NET 8 (C#) for the backend and React + TypeScript for the frontend.

It’s basically a small project management system where users can register, log in, create their own projects, and manage tasks inside those projects.
The focus of this assignment was to implement authentication, proper entity relationships, and a clean modular structure both on the backend and frontend.

⚙️ Tech Stack

Frontend: React, TypeScript, Axios, React Router
Backend: .NET 8 Core, Entity Framework Core
Database: SQLite / In-Memory
Authentication: JWT (JSON Web Tokens)
Styling: Tailwind CSS
Version Control: Git + GitHub

🎯 What It Does
🔐 Authentication

Users can register and log in using JWT.

After logging in, they can access only their own projects and tasks.

🗂️ Projects

Each user can create multiple projects.

A project contains:

Title (required, 3–100 chars)

Description (optional)

Creation date (auto-generated)

Projects can be created or deleted anytime.

✅ Tasks

Each project can have multiple tasks.

A task includes:

Title (required)

Due Date (optional)

Completion status (toggle)

Tasks can be added, updated, deleted, or marked complete.

🧩 API Structure
Auth Routes
Method	Endpoint	Description
POST	/api/auth/register	Register a new user
POST	/api/auth/login	Authenticate and return JWT
Project Routes
Method	Endpoint	Description
GET	/api/projects	Get all user projects
POST	/api/projects	Create a new project
DELETE	/api/projects/{id}	Delete a project
Task Routes
Method	Endpoint	Description
GET	/api/projects/{projectId}/tasks	Get all tasks for a project
POST	/api/projects/{projectId}/tasks	Add a task
PUT	/api/tasks/{id}	Update or toggle task
DELETE	/api/tasks/{id}	Delete a task


These are for the Assignment 2:- 
<img width="1357" height="632" alt="Screenshot 2025-10-31 124116" src="https://github.com/user-attachments/assets/87bf758d-ca07-4c07-9bb2-a5f3ee3215f5" />
<img width="630" height="641" alt="Screenshot 2025-10-31 123758" src="https://github.com/user-attachments/assets/600cb75a-d180-439f-91d7-f88e8b5770fe" />



🚀 How to Run the Project
🧩 Assignment 1 – Basic Task Manager
🛠 Backend Setup (TaskManagerBackend)
# Go into backend folder
cd TaskManagerBackend

# Restore dependencies
dotnet restore

# Run the backend server
dotnet run


✅ The backend will start at
https://localhost:5000
 (or whatever port your appsettings.json / launchSettings.json specifies)

💻 Frontend Setup (task-manager)
# Go into frontend folder
cd task-manager

# Install dependencies
npm install

# Start the React development server
npm run dev


✅ The frontend will run at
http://localhost:5173

Make sure both backend and frontend are running at the same time.
Your React app will automatically call the backend API via Axios.


🧩 Assignment 2 – Mini Project Manager
🛠 Backend Setup (ProjectManagerAPI)
# Navigate to the backend folder
cd ProjectManagerAPI

# Restore dependencies
dotnet restore

# Run the backend API
dotnet run


✅ The backend will start at
https://localhost:5057

💻 Frontend Setup (project-manager-frontend)
# Navigate to the frontend folder
cd project-manager-frontend

# Install dependencies
npm install

# Run the React app
npm run dev


✅ The frontend will run at
http://localhost:5173

Once logged in, JWT authentication will kick in automatically and protect routes.
Make sure your .env or API URL points to the correct backend port (e.g., 5057).
