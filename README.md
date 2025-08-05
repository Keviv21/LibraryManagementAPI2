# 📚 LibraryManagement2 API

A robust, production-ready .NET 8 Web API for managing library resources, built with clean N-Tier architecture and enterprise-grade practices.

---

## 🚀 Introduction

LibraryManagement2 is an extensible, secure, and well-architected Web API built using ASP.NET Core 8. It enables managing books in a library with support for authentication, role-based access control, structured logging, rate limiting, and more — all organized using a clean, modular N-Tier architecture.

---

## ✨ Key Features

- ✅ **CRUD Operations on Books** (`Create`, `Read`, `Update`, `Delete`)
- 🔐 **JWT-based Authentication**
- 👥 **Role-Based Access Control (RBAC)**
- 💡 **AutoMapper** for DTO ↔ Entity conversion
- 📜 **XML Comments & Swagger UI** for API documentation
- 🔁 **Rate Limiting** using Fixed Window strategy
- 🪵 **Serilog** for structured and extensible logging
- 🧠 **Entity Framework Core** for ORM & migrations
- 🧾 **Unique ISBN Constraint** enforced in database
- 🧱 **SOLID Principles** & Clean Code Practices
- 🏗️ **Strict N-Tier Architecture** for separation of concerns

---

## 📂 Project Architecture

```
LibraryManagement2/
│
├── LibraryManagement2.API           → API layer (Controllers, Middleware, Program.cs)
├── LibraryManagement2.Business      → Business logic & Interfaces
├── LibraryManagement2.Data          → EF Core DbContext & Repositories
├── LibraryManagement2.Shared        → DTOs and Common Contracts
├── LibraryManagement2.AutoMapper    → Mapping profiles for DTO <-> Entity
├── LibraryManagement2.Integration   → Integrations like TokenGeneration etc.
```

---

## ⚙️ Dependencies & Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- SQL Server (Express or full)
- Visual Studio 2022 or VS Code
- NuGet Packages:
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - Serilog.AspNetCore
  - AutoMapper.Extensions.Microsoft.DependencyInjection
  - Swashbuckle.AspNetCore (Swagger)
  - System.Threading.RateLimiting

---

## 🔑 API Endpoints – `/api/books`

| Method | Endpoint           | Description             | Auth Required |
|--------|--------------------|-------------------------|---------------|
| GET    | `/api/books`       | Get all books           | ✅            |
| GET    | `/api/books/{id}`  | Get a book by ID        | ✅            |
| POST   | `/api/books`       | Create a new book       | ✅ (Admin)    |
| PUT    | `/api/books/{id}`  | Update a book           | ✅ (Admin)    |
| DELETE | `/api/books/{id}`  | Delete a book           | ✅ (Admin)    |

> 📘 All endpoints require JWT in `Authorization: Bearer <token>` header.

---

## 🧪 Example Book JSON

```json
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "isbn": "9780132350884",
  "publishedYear": 2008
}
```

---

## 📌 Additional Highlights

- ✅ `GlobalExceptionMiddleware` handles all unhandled exceptions gracefully.
- ✅ Swagger UI with JWT Token support (`/swagger`)
- ✅ Proper logging to console and files using Serilog (configured via `appsettings.json`)
- ✅ Adherence to SOLID principles: DI, SRP, Interface Segregation, etc.

---

## 🛠️ Setup Instructions

1. Clone the repository
2. Update the `DefaultConnection` in `appsettings.json`
3. Run the EF Core migrations (if any)
4. Start the project (`dotnet run` or F5)
5. Use Swagger UI to explore APIs

---

