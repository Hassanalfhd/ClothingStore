# ClothingStore

A backend REST API for an e-commerce clothing store, built with **ASP.NET Core and .NET 8**.

The project demonstrates how to design and structure a practical e-commerce backend with product management, product variants, shopping carts, orders, authentication, API versioning, centralized error handling, logging, and performance profiling.

> **Portfolio Project:** This project was developed to demonstrate backend development, API design, architecture, and practical problem-solving using the .NET ecosystem.

---

## 📌 Project Overview

ClothingStore is an e-commerce backend designed to support the core operations of an online clothing store.

The API is organized around common e-commerce domains such as:

* Products
* Product Categories
* Brands
* Product Variants
* Sizes
* Colors
* Shopping Cart
* Orders
* Product Images
* Authentication

The project focuses on building a maintainable backend rather than implementing only basic CRUD operations.

---

## ✨ Key Features

### 🛍️ Product Management

* Create, update, retrieve, and manage products
* Product categories
* Brands
* Product images
* Product variants
* Sizes
* Colors

### 🛒 Shopping Cart

* Cart management
* Product and variant selection
* Cart-related operations

### 📦 Order Management

* Order creation and management
* Order-related business operations
* Structured order workflow

### 🔐 Authentication

* User authentication
* Protected API endpoints
* Authentication-related backend logic

### 🧩 API Versioning

The API uses versioning to make the backend easier to evolve while maintaining compatibility with existing clients.

Example:

```text
/api/v1/...
```

API versioning helps prevent breaking existing consumers when future versions of the API are introduced.

### 🛡️ Global Exception Handling

The application includes centralized exception handling through middleware.

Instead of handling errors independently inside every controller, exceptions can be processed through a common middleware pipeline.

This provides a more consistent API error-handling strategy.

### 📝 Request Logging

The application includes request logging middleware and **Serilog** integration.

This helps with:

* Troubleshooting
* Monitoring requests
* Investigating unexpected behavior
* Understanding API activity

### ⚡ Performance Profiling

The project integrates **MiniProfiler** to help investigate request performance and identify potential bottlenecks.

This is useful when diagnosing issues such as:

* Slow API requests
* Expensive database operations
* Performance bottlenecks

### 📚 API Documentation

The project uses **Swagger / OpenAPI tooling** to make the API easier to explore and test during development.

---

# 🏗️ Architecture

The backend is separated into multiple layers to keep responsibilities organized.

```text
ClothingStoreBackEnd
│
├── ClothingStore.API
│   ├── Controllers
│   ├── Middlewares
│   └── API configuration
│
├── ClothingStore.Applicaion
│   └── Application/business logic
│
└── ClothingStore.Infrastructure
    └── Data access and infrastructure concerns
```

### API Layer

Responsible for:

* HTTP requests and responses
* Controllers
* API configuration
* Middleware
* API versioning

### Application Layer

Responsible for application-level operations and business logic.

### Infrastructure Layer

Responsible for infrastructure and data-access concerns.

This separation makes the system easier to maintain, test, and extend.

---

# 🧰 Technologies

| Technology            | Purpose                       |
| --------------------- | ----------------------------- |
| C#                    | Primary programming language  |
| .NET 8                | Application runtime           |
| ASP.NET Core          | REST API development          |
| Entity Framework Core | Data access / ORM             |
| SQL Server            | Relational database           |
| Swagger / OpenAPI     | API documentation and testing |
| Serilog               | Logging                       |
| MiniProfiler          | Performance profiling         |
| API Versioning        | Versioned API design          |

---

# 📂 Main API Areas

The API currently contains endpoints/controllers covering areas such as:

```text
Authentication
Brands
Categories
Carts
Colors
Orders
Product Images
Product Variants
Products
Sizes
```

This structure represents the main business areas required by a clothing e-commerce backend.

---

# 🔄 Example Request Flow

A typical request follows a structured backend pipeline:

```text
Client
   │
   ▼
HTTP Request
   │
   ▼
ASP.NET Core Middleware
   │
   ├── Request Logging
   ├── Exception Handling
   └── Authentication
   │
   ▼
Controller
   │
   ▼
Application Layer
   │
   ▼
Infrastructure / Data Access
   │
   ▼
SQL Server
   │
   ▼
HTTP Response
```

This structure makes it easier to identify and troubleshoot problems across different layers of the application.

---

# 🧪 Development & Troubleshooting

One of the goals of this project is to demonstrate practical backend engineering and troubleshooting skills.

The architecture provides clear boundaries that make it possible to investigate issues such as:

* HTTP API errors
* Authentication problems
* Database-related issues
* Business logic errors
* Unexpected API responses
* Request performance problems
* Unhandled exceptions
* Logging and diagnostic issues

A typical troubleshooting process is:

```text
Problem
   ↓
Reproduce the issue
   ↓
Inspect logs / exception
   ↓
Trace the request
   ↓
Identify the root cause
   ↓
Apply the fix
   ↓
Verify the result
```

---

# 🚀 Getting Started

## Prerequisites

Before running the project, make sure you have:

* .NET 8 SDK
* SQL Server
* Visual Studio 2022 or another compatible .NET IDE
* Git

---

## 1. Clone the repository

```bash
git clone https://github.com/Hassanalfhd/ClothingStore.git
```

```bash
cd ClothingStore
```

---

## 2. Configure the Database

Configure the SQL Server connection string in the application's configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

> Do not commit real production credentials or sensitive connection strings to the repository.

---

## 3. Restore Dependencies

```bash
dotnet restore
```

---

## 4. Build the Project

```bash
dotnet build
```

---

## 5. Run the API

Navigate to the API project and run:

```bash
dotnet run
```

The application will start using the configured ASP.NET Core environment.

---

# 📖 API Documentation

After running the application, Swagger can be used to explore and test the available API endpoints.

The exact Swagger URL depends on the configured application environment and launch settings.

---

# 🎯 Project Goals

This project was developed with the following goals:

* Practice real-world ASP.NET Core Web API development
* Apply layered backend architecture
* Build an e-commerce-oriented domain
* Work with relational data and Entity Framework Core
* Implement authentication and protected endpoints
* Apply centralized exception handling
* Implement structured logging
* Explore API versioning
* Investigate API performance
* Practice maintainable backend development

---

# 💡 What This Project Demonstrates

From a software engineering perspective, this project demonstrates experience with:

**Backend Development**

```text
C#
.NET 8
ASP.NET Core
REST APIs
```

**Data**

```text
Entity Framework Core
SQL Server
Relational Data
```

**API Engineering**

```text
API Versioning
Swagger / OpenAPI
Middleware
Authentication
```

**Reliability & Diagnostics**

```text
Global Exception Handling
Serilog
Request Logging
MiniProfiler
```

**Architecture**

```text
API
 ↓
Application
 ↓
Infrastructure
```

---

# 📌 Project Status

This repository is a portfolio and learning project focused on demonstrating practical backend engineering concepts.

It can be extended in the future with additional production-oriented features such as:

* Automated unit and integration testing
* Docker containerization
* CI/CD
* Cloud deployment
* Advanced caching
* Additional security hardening
* Expanded payment integration

These features are intentionally kept outside the current project scope.

---

# 👨‍💻 Author

**Hassan Alfahd**

C# / ASP.NET Core Backend Developer

GitHub:

https://github.com/Hassanalfhd
