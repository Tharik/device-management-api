# Device Management API

REST API for managing devices, built with ASP.NET Core, Entity Framework Core, and MySQL.

## Overview

This API allows clients to:

- Create devices
- Retrieve devices
- Filter devices by brand and state
- Update devices (full and partial)
- Delete devices

It also enforces important business rules to ensure data integrity.



## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9
- MySQL
- Pomelo.EntityFrameworkCore.MySql
- Swagger (Swashbuckle)
- xUnit, Moq, FluentAssertions
- Docker / Docker Compose


## Business Rules

- `CreatedAt` is generated on creation and cannot be modified
- `Name` and `Brand` cannot be changed when a device is `InUse`
- A device in `InUse` state cannot be deleted


## Running the Application

### Option 1 — Using Docker (recommended)

```bash
docker-compose up --build
```

#### API will be available at:
```bash
http://localhost:8080
```

#### Swagger UI:
```bash
http://localhost:8080/swagger
```

### Option 2 — Running locally
#### 1. Start MySQL
Ensure you have a running MySQL instance.

#### 2. Configure connection string
Update `appsettings.json` if needed:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=devicedb;user=root;password=yourpassword"
}
```

#### 3. Apply migrations
```bash
dotnet ef database update
```

### 4.Run the app
```bash
dotnet run --project src/DeviceManagement.Api
```

## API Endpoints

### Create Device

```bash
POST /api/devices
```

Example request:
```json
{
  "name": "iPhone 15 Pro",
  "brand": "Apple",
  "state": 1
}
```

### Get All Devices
```bash
GET /api/devices
```

### Get Device by Id
```bash
GET /api/devices/{id}
```

### Filter Devices
By brand:
```bash
GET /api/devices?brand=Apple
```

By state:
```bash
GET /api/devices?state=2
```

State values:
 - 1 = Available
 - 2 = InUse
 - 3 = Inactive

### Update Device (Full)
```bash
PUT /api/devices/{id}
```

### Update Device (Partial)
```bash
PATCH /api/devices/{id}
```

Example:
```json
{
  "name": "Updated Device Name"
}
```

### Delete Device
```bash
DELETE /api/devices/{id}
```

## Validation
Basic validation is applied to requests:

- Name and Brand are required for create and update
- Maximum length: 100 characters
- Patch allows partial updates but enforces field constraints

## Error Handling
The API uses a global exception handling middleware to map domain exceptions into HTTP responses.

Examples:
- `400 Bad Request` → validation or business rule violations
- `404 Not Found` → resource not found

## Tests
Unit tests are implemented using:
- xUnit
- Moq
- FluentAssertions

To run tests:
```bash
dotnet test
```

## Project Structure
```bash
src/
  DeviceManagement.Api/
    Controllers/
    Contracts/
    Domain/
    Application/
    Infrastructure/
    Middleware/

tests/
  DeviceManagement.Api.Tests/
  ```

## Notes
 - The architecture follows a simplified layered approach inspired by `Clean Architecture`
 - Focus was placed on clarity, separation of concerns, and maintainability
 - The solution avoids unnecessary complexity while keeping business rules properly encapsulated