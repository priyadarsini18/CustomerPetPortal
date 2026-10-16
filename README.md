# 🐾 PawCare - Customer and Pet Account Portal

## Overview

PawCare is a Customer and Pet Account Portal developed as part of POC 4 - Pet Insurance Assignment.

The application allows customers to:

- Register and manage their profiles
- Manage pet information
- View insurance policies
- View claims history
- Access customer dashboard information
- Import and export customer data using Excel

The solution follows a layered architecture using Angular, ASP.NET Core Web API, EF Core, LINQ, SQLite, and NUnit.

---

# Technology Stack

## Frontend

- Angular
- TypeScript
- HTML
- CSS

## Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- LINQ
- SQLite

## Testing

- NUnit
- Moq
- FluentAssertions

## Documentation

- Swagger/OpenAPI
- Postman Collection

## Version Control

- Git
- GitHub

---

# Architecture

```text
Angular UI
    |
    V
ASP.NET Core Web API
    |
    V
Business Services
    |
    V
Repositories
    |
    V
Entity Framework Core
    |
    V
SQLite Database
```

---

# Features

## Customer Registration

Customers can register using:

- First Name
- Last Name
- Email
- Phone Number
- Address
- ZIP Code
- Password
- Confirm Password

### Validations

- Mandatory field validation
- Email format validation
- Unique email validation
- Password confirmation validation

---

## Customer Dashboard

Dashboard contains:

### Summary Cards

- Total Pets
- Active Policies
- Open Claims
- Policies Expiring Soon

### Sections

- My Pets
- My Policies
- Recent Claims
- Notifications

---

## Pet Management

### Pet Information

- Pet Name
- Species
- Breed
- Date Of Birth
- Age
- Status

### Actions

- Add Pet
- Edit Pet
- Deactivate Pet
- View Coverage

### Business Rules

- Pet Date Of Birth cannot be in the future.
- Every pet must belong to an active customer.
- Duplicate active pets are not allowed.

---

## Policies

Policies are available in read-only mode.

### Details

- Policy Number
- Coverage Type
- Premium
- Start Date
- End Date
- Policy Status

---

## Claims

Claims are available in read-only mode.

### Details

- Claim Number
- Treatment Date
- Claimed Amount
- Approved Amount
- Claim Status

---

## Customer Profile

Customers can:

- View Profile
- Update Phone Number
- Update Address
- Update Communication Preference

---

# Business Rules Implemented

✅ Customer email must be unique.

✅ Customer email must be valid.

✅ Pet DOB cannot be in the future.

✅ Every pet belongs to an existing customer.

✅ Duplicate active pets are not allowed.

✅ Dashboard data is customer-specific.

✅ Policies are read-only.

✅ Claims are read-only.

✅ Customer cannot be deleted while active policies exist.

✅ Sensitive information is not logged.

---

# LINQ Features Demonstrated

## Any()

Used for:

- Duplicate email validation
- Duplicate pet validation

## All()

Used for:

- Customer deletion eligibility check

## Count()

Used for:

- Dashboard summary counts

## Select()

Used for:

- DTO projection
- Dashboard projection

## Join()

Used for:

- Customer and Pet relationship queries

## Include()

Used for:

- Loading related entities

## ThenInclude()

Used for:

- Nested relationship loading

---

# API Endpoints

## Customer APIs

### Register Customer

```http
POST /api/customers
```

### Get Customer

```http
GET /api/customers/{id}
```

### Update Customer

```http
PUT /api/customers/{id}
```

### Delete Customer

```http
DELETE /api/customers/{id}
```

---

## Pet APIs

### Add Pet

```http
POST /api/customers/{id}/pets
```

### Get Pets

```http
GET /api/customers/{id}/pets
```

### Update Pet

```http
PUT /api/customers/{id}/pets/{petId}
```

### Delete / Deactivate Pet

```http
DELETE /api/customers/{id}/pets/{petId}
```

---

## Dashboard API

```http
GET /api/customers/{id}/dashboard
```

---

## Policies API

```http
GET /api/customers/{id}/policies
```

---

## Claims API

```http
GET /api/customers/{id}/claims
```

---

## Excel APIs

### Import Data

```http
POST /api/excel/customers/import
```

### Export Data

```http
GET /api/excel/customers/export
```

---

# Database Entities

## Customers

```text
CustomerId
FirstName
LastName
Email
Phone
CommunicationPreference
IsActive
```

## Addresses

```text
AddressId
CustomerId
Line1
City
State
ZipCode
```

## Pets

```text
PetId
CustomerId
PetName
Species
Breed
DateOfBirth
IsActive
```

## CustomerPolicies

```text
PolicyId
PolicyNumber
CustomerId
PetId
Premium
Status
```

## CustomerClaims

```text
ClaimId
ClaimNumber
CustomerId
PetId
TreatmentDate
ClaimAmount
ApprovedAmount
Status
```

---

# Validation Implemented

## Customer

- Required fields validation
- Email validation
- Duplicate email validation

## Pets

- Required fields validation
- Date Of Birth validation
- Future DOB prevention
- Duplicate active pet validation

---

# Logging

Structured logging is implemented
