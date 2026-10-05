# 🐾 PAWCARE - Customer & Pet Insurance Portal

## Project Overview

PAWCARE is a Customer and Pet Insurance Management Portal developed using Angular, ASP.NET Core Web API, Entity Framework Core, and SQLite.

The application enables customers to:

- Register and Login
- Manage Pets
- View Dashboard Information
- View Insurance Policies
- View Claims
- Manage Profile Information
- Import and Export Customer Data

---

# Technology Stack

## Frontend

- Angular
- TypeScript
- HTML
- CSS
- Bootstrap

## Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- Repository Pattern
- Dependency Injection
- Service Layer Architecture

## Database

- SQLite

## Testing

- NUnit

---

# Architecture

```text
Angular UI
     ↓
Controllers
     ↓
Services
     ↓
Repositories
     ↓
AppDbContext
     ↓
SQLite Database
```

---

# Project Structure

```text
CustomerPetPortal.API
│
├── Controllers
├── Services
├── Repositories
├── DTOs
├── Models
├── Data
├── Middleware
├── Program.cs
└── appsettings.json

customer-pet-portal-ui
│
├── pages
├── core/services
├── routing
└── components
```

---

# Implemented Features

## Customer Management

✅ Customer Registration

✅ Customer Login

✅ View Customer Profile

✅ Update Customer Profile

---

## Dashboard

✅ Total Pets

✅ Active Policies

✅ Open Claims

✅ Policies Expiring Soon

---

## Pet Management

✅ Add Pet

✅ View Pets

✅ Update Pet

✅ Deactivate Pet

---

## Policies

✅ View Policies

---

## Claims

✅ View Claims

---

## Excel Operations

✅ Import Customer Data

✅ Export Customer Data

---

# Business Rules Implemented

## Pet DOB Validation

Future dates are not allowed.

```csharp
if(dto.DateOfBirth > DateTime.Today)
{
    throw new ArgumentException(
        "Pet date of birth cannot be in the future.");
}
```

## Customer Validation

Only active customers can add pets.

## Duplicate Pet Validation

Active customers cannot have duplicate active pets with the same name.

## Pet Deactivation

Pets with active policies are deactivated instead of being permanently deleted.

---

# Dependency Injection

Configured in Program.cs

```csharp
builder.Services.AddScoped<
    ICustomerService,
    CustomerService>();

builder.Services.AddScoped<
    ICustomerRepository,
    CustomerRepository>();

builder.Services.AddScoped<
    IPetService,
    PetService>();

builder.Services.AddScoped<
    IPetRepository,
    PetRepository>();
```

Benefits:

- Loose Coupling
- Better Maintainability
- Easier Unit Testing
- SOLID Principles

---

# API Endpoints

## Customer APIs

```http
POST   /api/customers
POST   /api/customers/login
GET    /api/customers/{id}
PUT    /api/customers/{id}
DELETE /api/customers/{id}
```

## Pet APIs

```http
POST   /api/customers/{id}/pets
GET    /api/customers/{id}/pets
PUT    /api/customers/{id}/pets/{petId}
DELETE /api/customers/{id}/pets/{petId}
```

## Dashboard

```http
GET /api/customers/{id}/dashboard
```

## Policies

```http
GET /api/customers/{id}/policies
```

## Claims

```http
GET /api/customers/{id}/claims
```

## Excel Operations

```http
POST /api/excel/customers/import
GET  /api/excel/customers/export
```

---

# Database Tables

```text
Customers
Pets
Policies
Claims
Addresses
```

---

# Request Flow

```text
Angular
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
AppDbContext
   ↓
SQLite Database
```

Response:

```text
SQLite Database
   ↑
AppDbContext
   ↑
Repository
   ↑
Service
   ↑
Controller
   ↑
Angular
```

---

# Design Patterns Used

- Layered Architecture
- Repository Pattern
- Dependency Injection
- DTO Pattern
- Service-Oriented Architecture

---

# Learning Outcomes

This project helped me gain experience in:

- Angular Development
- ASP.NET Core Web API
- Entity Framework Core
- SQLite Database
- REST API Development
- Dependency Injection
- Repository Pattern
- Service Layer Architecture
- NUnit Testing

---

# Author

**Priyadarsini Sreenivasan**

Trainee Developer

PAWCARE - Customer & Pet Insurance Portal