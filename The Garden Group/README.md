# NoSQL Incident Management System

This is a NoSQL-based incident/ticket management system built for the **[course name]** project.  
The backend is built with **ASP.NET Core MVC** and **MongoDB**.

## 1. Project Overview

The application allows:
- Regular **employees** to log in, create tickets, and view only their own tickets.
- **Service desk employees** to manage tickets for all employees and manage employees.
- Both roles to see a **dashboard** with ticket status statistics.

The system uses **MongoDB** as the NoSQL database and follows a **repository pattern** with MVC.

---

## 2. Tech Stack

- **Backend**: ASP.NET Core MVC
- **Database**: MongoDB
- **Data access**: MongoDB C# driver, repository pattern
- **Language**: C#

---

## 3. Architecture

- `Data/MongoDbContext.cs`  
  - Manages the MongoDB connection.  
  - Exposes collections: `Employees`, `Tickets`.

- `Models/Employee.cs`  
  - Employee with `Id`, `Name`, `Email`, `Department`, `Role` (`employee` or `servicedesk`).

- `Models/Ticket.cs` + `TicketStatus` enum  
  - Ticket with `Id`, `EmployeeId`, `Subject`, `Description`, `Priority`, `Status`, timestamps, tags, status history.

- `Repositories/EmployeeRepository.cs`  
  - CRUD operations for `Employee`, plus `GetByEmailAsync` used for login.

- `Repositories/TicketRepository.cs`  
  - CRUD operations for `Ticket`.  
  - `GetByEmployeeIdAsync` to filter tickets for logged-in employees.  
  - `GetAllForDashboardAsync` to load data for the dashboard.

- `Controllers/LoginController.cs`  
  - Handles login and logout.  
  - Login by **email only** (no password for this project).  
  - Saves `EmployeeId`, `Role`, and `EmployeeName` in session.

- `Controllers/TicketsController.cs`  
  - Shows ticket list based on role:
    - `servicedesk` ? all tickets.
    - `employee` ? only tickets with their `EmployeeId`.
  - Allows creating, editing, deleting tickets.
  - `Dashboard` action calculates percentages of Open / Resolved / Closed tickets.

- `Controllers/EmployeeController.cs`  
  - CRUD for employees.  
  - Only accessible for role `servicedesk` (checked using session).

- `Services/TicketSorter.cs`  
  - **Individual functionality**: sorts the ticket list by priority.  
  - Custom order: `Urgent` ? `High` ? `Medium` ? `Low`.  
  - Used in `TicketsController.Index()`.

---

## 4. User Roles & Rights

### Employee
- Logs in with their email.
- Can create tickets.
- Can view **only their own** tickets.
- Can view their own dashboard with ticket status percentages.

### Service Desk Employee
- Logs in with their email.
- Can view **all tickets**.
- Can manage employees (create, update, delete).
- Can view a global dashboard for all tickets.

Role is stored in the `role` field in the `employees` MongoDB collection.

---

## 5. How to Run

### Prerequisites

- .NET SDK installed
- MongoDB instance (local or cloud)
- Connection string in environment variable: `MONGODB_URI`

Example `.env` (if using DotNetEnv):

```env
MONGODB_URI=mongodb://localhost:27017
