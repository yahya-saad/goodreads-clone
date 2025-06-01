<div align="center">
  <h1>Goodreads Clone</h1>
  <p>A modern, modular, and extensible RESTful API inspired by Goodreads, built with ASP.NET Core (.NET 9), Entity Framework Core, MediatR, Follows Clean Architecture.</p>
      <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="dotnet" />
      <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />
      <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" alt="sql-server" />
      <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white" alt="jwt">
      <img src = "https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white" alt="Swagger">
      <img src = "https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=Postman&logoColor=white" alt="Swagger">

  </div>

---

## Table of Contents

- [Features](#features)
- [Database Diagram](#database-diagram)
- [Endpoints](#endpoints)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Background Jobs](#background-jobs)
- [Health Checks](#health-checks)
- [Contributing](#contributing)

---

## Features

- User authentication & JWT-based authorization
- Book, author, and review management
- Reading progress tracking
- Yearly reading challenges
- Email Support
- File uploads via Azure Blob Storage
- Background jobs with Hangfire
- Health checks and monitoring
- Swagger/Scalar documentation

---

## Database Diagram

## ![Database Diagram](./assets//db_diagram.png)

## Endpoints

![01](./assets//01.png)
![02](./assets//02.png)
![03](./assets//03.png)
![04](./assets//04.png)
![05](./assets//05.png)
![06](./assets//06.png)
![07](./assets//07.png)

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- (Optional) [SMTP4Dev](https://github.com/rnwood/smtp4dev) for local email testing
- (Optional) [Azurite](https://github.com/Azure/Azurite) for local Azure Blob Storage emulation

### Configuration

1. **Clone the repository:**

2. **Restore packages**

   ```bash
   dotnet restore
   ```

3. **Set up configuration:**

- Update `src/Goodreads.API/appsettings.Development.json` with your local connection strings and secrets as needed.

4. **Apply database migrations and seed data:**

- By default, `RunMigrations` is set to `true` in development. The database will be created and seeded automatically on first run.

- Run the migrations (manually).
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

5. **Run the application**
   ```bash
   dotnet run
   ```

## API Documentation

- Interactive API docs are available via Swagger at `/swagger` and Scalar at `/scalar`.

---

## Background Jobs

- Hangfire is used for recurring and background jobs.
- Dashboard available at `/hangfire`.

---

## Health Checks

- Health check endpoint: `/healthz`
- Includes checks for database and blob storage connectivity.

---

## Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements.

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/yahya-saad-a98801187)
