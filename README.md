
# 📝 Blog Platform API

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-blue?style=for-the-badge)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Authentication](https://img.shields.io/badge/Auth-JWT%20%2B%20Identity-red?style=for-the-badge)](#-security--authentication)

A feature-rich, RESTful Backend Web API for a blog publishing platform built with ASP.NET Core 8 and Entity Framework Core[cite: 9]. The platform supports multi-role user management, post publishing with tag categorization, interactive comment systems, and token revocation middleware.

---

## 🏗 Project Structure

The project follows a modular repository pattern with separated concerns across Data Transfer Objects, Repositories, Domain Models, and custom Middleware[cite: 9]:

```text
Blog_Platform/
├── Controllers/            # API Endpoints (Account, BlogPost, Comment, Tag, Role)
├── DTO/                    # Data Transfer Objects for Request/Response payloads
├── Data/                   # Entity Framework Core DbContext & Configurations
├── IRepository/            # Repository Interfaces
├── Repository/             # Repository Implementations & Persistence Logic
├── Models/                 # Domain Entities (AppUser, BlogPost, Comment, Tag, etc.)
├── Migrations/             # EF Core Database Migrations
├── GeneralResponse.cs      # Unified Response Wrapper
├── HelpAddAndUpdatePost.cs  # Helper utility for post tag management
├── RevokeMiddleWare.cs     # Middleware for Token Revocation checks
├── Program.cs              # Dependency Injection & Application Pipeline
└── appsettings.json        # Application Configuration & JWT Settings

```

---

## ✨ Core Features

* **Authentication & Authorization**: Full identity management with JWT bearer tokens, custom user claims, and dynamic token revocation tracking.


* **Role-Based Access Control (RBAC)**: Fine-grained authorizations across `Developer`, `Admin`, `Author`, and `Reader` roles.


* **Blog Post Management**: Full CRUD operations for articles with many-to-many tag relationships and author validation checks.


* **Interactive Comments**: Threaded comment functionality linked to blog posts and specific user authors.


* **Tag System**: Categorization and filtering of posts through customizable tags.


* **Security Middleware**: Custom middleware (`RevokeMiddleWare`) that invalidates revoked JWT tokens dynamically.



---

## 🔐 Security & Authentication Flow

1. **User Registration**: New accounts are created via `POST /api/Account/Register` and automatically assigned the `Reader` role.


2. **JWT Authentication**: Login via `POST /api/Account/Login` generates a signed JWT token embedded with user ID, name, email, and role claims.


3. **Token Management & Revocation**: Active tokens are persisted in the `Token` repository. If a user modifies credentials, changes roles, or gets deleted, the token status is flagged as `IsRevoked`, causing `RevokeMiddleWare` to deny subsequent requests.



---

## 📡 API Endpoint Reference

### 👤 Account & User Management (`/api/Account`)

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Account/Register` | Public | Register a new user account (Default role: Reader).

 |
| `POST` | `/api/Account/Login` | Public | Authenticate user credentials and return JWT token.

 |
| `GET` | `/api/Account` | Authenticated | Retrieve current authenticated user details.

 |
| `GET` | `/api/Account/GetAllUsers` | `Admin` | Retrieve a list of all registered users.

 |
| `PUT` | `/api/Account` | Authenticated | Update user profile and change password.

 |
| `DELETE` | `/api/Account` | Authenticated | Delete the logged-in user account & revoke token.

 |
| `DELETE` | `/api/Account/DeleteAnotherUser/{UserId}` | `Admin` | Delete a specific user account by ID.

 |
| `POST` | `/api/Account/AddUserForRole` | `Admin` | Assign a role to a user.

 |
| `POST` | `/api/Account/RemoveUserFromRole` | `Admin` | Revoke a role from a user.

 |

---

### 📰 Blog Post Management (`/api/BlogPost`)

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| `POST` | `/api/BlogPost` | `Admin`, `Author` | Create a new blog post with attached tags.

 |
| `GET` | `/api/BlogPost/GetAll` | Authenticated | Retrieve all blog posts with short comment and tag details.

 |
| `GET` | `/api/BlogPost/{Id}` | `Admin`, `Author` | Get detailed information for a specific post by ID.

 |
| `PUT` | `/api/BlogPost` | `Admin`, `Author` | Update a blog post (Author ownership check enforced).

 |
| `DELETE` | `/api/BlogPost` | `Admin`, `Author` | Delete a blog post and its associated comments.

 |

---

### 💬 Comment System (`/api/Comment`)

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Comment` | Authenticated | Add a comment to a specified blog post.

 |
| `GET` | `/api/Comment` | Authenticated | Retrieve all comments for a post by `PostId`.

 |
| `PUT` | `/api/Comment` | Authenticated | Update a comment (Author ownership check enforced).

 |
| `DELETE` | `/api/Comment` | Authenticated | Delete a comment (Author/Admin ownership check).

 |

---

### 🏷 Tag Management (`/api/Tag`)

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Tag` | Authenticated | Create a new tag.

 |
| `GET` | `/api/Tag/GetAll` | Authenticated | Retrieve all tags.

 |
| `GET` | `/api/Tag/{Id}` | Authenticated | Retrieve details of a specific tag by ID.

 |
| `PUT` | `/api/Tag` | Authenticated | Update an existing tag.

 |
| `DELETE` | `/api/Tag` | Authenticated | Delete a tag by ID.

 |

---

### 🛡 Role Management (`/api/Role`)

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Role` | `Developer` | Create a new user role in the system.

 |

---

## 📊 Database Domain Models

* **`AppUser`**: Extends `IdentityUser` to associate user profiles with blog posts, comments, and security tokens.


* **`BlogPost`**: Represents articles containing titles, main body content, creation/update timestamps, tags, and comment collections.


* **`Comment`**: Connects `AppUser` to a `BlogPost` with text content and timestamp tracking.


* **`Tag` & `BlogPostHasTag**`: Junction entity managing many-to-many associations between posts and categories.



---

## 🚀 Getting Started

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download)

* [SQL Server](https://www.microsoft.com/en-us/sql-server/)

### Installation & Local Setup

1. **Clone the repository**:
```bash
git clone [https://github.com/omargomah/Blog_Platform.git](https://github.com/omargomah/Blog_Platform.git)
cd Blog_Platform

```


2. **Configure App Settings**:
Update `Blog_Platform/appsettings.json` with your database connection string and JWT secret key:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=BlogPlatformDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JWT": {
    "Key": "YOUR_STRONG_SECRET_KEY_HERE",
    "issuer": "Blog_Platform",
    "audience": "Blog_Platform_Users"
  }
}

```


3. **Apply Database Migrations**:
```bash
dotnet ef database update

```


4. **Run the API**:
```bash
dotnet run --project Blog_Platform

```


