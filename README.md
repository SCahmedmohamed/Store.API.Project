# 🛍️ Store API — E-Commerce Backend

> A production-ready RESTful API for a full-featured E-Commerce platform, built with **.NET 8** and following **Onion Architecture** principles.

---

## 📌 About The Project

**Store API** is a comprehensive backend for an e-commerce system. It handles everything from user authentication and product browsing, to cart management, order placement, and payment processing — all structured in a clean, layered architecture that prioritizes separation of concerns and scalability.

---

## ✨ Features

| Feature | Description |
| :--- | :--- |
| 🔐 **Authentication & Authorization** | JWT-based login and registration using ASP.NET Core Identity |
| 📦 **Product Catalog** | Paginated product listing with filtering and sorting support |
| 🛒 **Shopping Basket** | Temporary cart stored in **Redis** for high-speed access |
| 💳 **Payment Integration** | Secure checkout powered by **Stripe** with payment intent support |
| 📝 **Order Management** | Full order lifecycle — creation, tracking, and history |
| ⚙️ **Specification Pattern** | Flexible query building using the Specification design pattern |
| 🗺️ **AutoMapper** | Clean separation between domain entities and response DTOs |
| 🔁 **Unit of Work** | Consistent and atomic database transactions |
| 🛡️ **Global Error Handling** | Centralized middleware for clean, consistent error responses |

---

## 🏗️ Architecture Overview

The solution follows **Onion Architecture**, split into four main layers:

```
Store.API.Project/
│
├── 🟣 Core/
│   ├── Domain                   # Entities, Enums, Base classes
│   ├── Services                 # Business logic & AutoMapper profiles
│   └── Services.Abstractions    # Interfaces & contracts
│
├── 🔵 Infrastructure/
│   ├── Persistence              # EF Core, Migrations, Redis, Repos, Unit of Work
│   └── Presentation             # API Controllers
│
├── 🟢 Web/                      # Entry point: DI, Middleware, Swagger, CORS
└── 🟡 Shared/                   # DTOs, Error models, Paginated responses
```

**Dependency Direction:**
```
Web  ──►  Presentation  ──►  Services.Abstractions  ◄──  Persistence
                                      ▲
                                   Services  ──►  Domain
```

---

## 🛠️ Tech Stack

| Layer | Technology |
| :--- | :--- |
| **Runtime** | .NET 8 / C# 12 |
| **ORM** | Entity Framework Core 8 |
| **Database** | Microsoft SQL Server |
| **Caching** | Redis via StackExchange.Redis |
| **Payments** | Stripe (Stripe.net) |
| **Mapping** | AutoMapper |
| **API Docs** | Swagger / Swashbuckle |
| **Security** | ASP.NET Core Identity + JWT Bearer |

---

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- Microsoft SQL Server
- Redis Server
- Stripe Account *(for test keys)*

### Setup & Run

**1. Clone the repository**
```bash
git clone https://github.com/your-username/Store.API.Project.git
```

**2. Configure `appsettings.json`** inside the `Web` project
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=StoreDb;Trusted_Connection=True;",
    "Redis": "localhost"
  },
  "Token": {
    "Key": "your-super-secret-jwt-key",
    "Issuer": "StoreAPI",
    "Audience": "StoreAPIUsers"
  },
  "StripeSettings": {
    "SecretKey": "sk_test_..."
  }
}
```

**3. Run the application**
```bash
cd Web
dotnet run
```
> ✅ The database will be created and seeded automatically on first run.

**4. Open Swagger UI**
```
https://localhost:<port>/swagger
```

---

## 🔌 API Endpoints

### 🔐 Authentication
| Method | Endpoint | Description |
| :---: | :--- | :--- |
| `POST` | `/api/auth/register` | Register a new user |
| `POST` | `/api/auth/login` | Login and receive JWT token |

### 📦 Products
| Method | Endpoint | Description |
| :---: | :--- | :--- |
| `GET` | `/api/products` | Get paginated product list |
| `GET` | `/api/products/{id}` | Get a single product by ID |

### 🛒 Basket
| Method | Endpoint | Description |
| :---: | :--- | :--- |
| `GET` | `/api/baskets` | Get customer basket from Redis |
| `POST` | `/api/baskets` | Create or update basket |
| `DELETE` | `/api/baskets` | Delete basket |

### 📝 Orders
| Method | Endpoint | Description |
| :---: | :--- | :--- |
| `POST` | `/api/orders` | Place a new order |
| `GET` | `/api/orders` | Get all orders for current user |
| `GET` | `/api/orders/{id}` | Get a specific order |

### 💳 Payments
| Method | Endpoint | Description |
| :---: | :--- | :--- |
| `POST` | `/api/payments/{basketId}` | Create or update Stripe payment intent |

---

## 📄 License

This project is licensed under the **MIT License**.
