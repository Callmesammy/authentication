# 🔐 AuthX – Modern Authentication API (ASP.NET 8 + JWT + Identity)

AuthX is a modern **authentication and authorization API** built with **.NET 8**, **ASP.NET Identity**, and **JWT**.  
It supports **user registration, login, and role-based access control (admin, user)** — ready for integration with any frontend (e.g., Next.js, React, Vue).

---

## 🚀 Features

✅ User registration & login  
✅ ASP.NET Core Identity integration  
✅ JWT (JSON Web Token) authentication  
✅ Role-based authorization (`user`, `admin`)  
✅ Modular minimal APIs (no controllers)  
✅ Clean architecture with DTOs, Services, and Endpoints  
✅ Easily extensible (e.g., for Task management API)

---

## 🧱 Tech Stack

| Layer | Technology |
|-------|-------------|
| Backend | ASP.NET Core 8 Web API |
| Auth | ASP.NET Identity + JWT |
| Database | InMemory (default) / SQL Server optional |
| Token Handling | Microsoft.IdentityModel.Tokens |
| Language | C# 12 |

---

## 🛠️ Installation Guide

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/YourUsername/AuthX.git
cd AuthX

dotnet restore


dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer


#
dotnet run
