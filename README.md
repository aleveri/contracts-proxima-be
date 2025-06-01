# 📄 Contract Management API

This is a clean, modular **ASP.NET Core Web API** designed to manage contracts and tariffs. It follows **Hexagonal Architecture** and **Domain-Driven Design (DDD)** and uses **ADO.NET + PostgreSQL** (without ORM) for full control over database access.

---

## 🚀 Features

- 🔹 Create, update, and list contracts
- 🔹 Retrieve all available tariffs
- 🔹 Validated inputs with error handling
- 🔹 Environment-ready configuration (CORS, Swagger, JSON formatting)
- 🔹 Clean layering: `Domain`, `Application`, `Infrastructure`, `WebApi`

---

## 🧱 Tech Stack

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- PostgreSQL
- ADO.NET
- JSON API
- Swagger / OpenAPI

---

## 📦 Project Structure

```
ContractsApi/
├── Domain/             # Core entities and interfaces
├── Application/        # Use cases and service logic
├── Infrastructure/     # Repositories, database access
├── WebApi/             # Controllers and program config
```

---

## ⚙️ Setup Instructions

### 1. Clone the repo

```bash
git clone https://github.com/aleveri/contracts-proxima-be.git
cd contracts-api
```

### 2. Configure database

Create a PostgreSQL database and update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=contractsdb;Username=postgres;Password=yourpassword"
  }
}
```

> 💡 Ensure the `tarifas` and `contracts` tables exist.

## ⚠️ Initial Setup: Insert Tarifa Data

Before running the application, make sure to populate the `tarifas` table using the provided SQL script:

```bash
psql -U postgres -d contractsdb -f scripts/insert_tarifas.sql
```

> Replace `postgres` and `contractsdb` with your actual PostgreSQL user and database names if different.

This step is required to have available tariffs for contract creation.


### 3. Run the API

```bash
dotnet build
dotnet run
```

API will be available at:  
`https://localhost:5001` or `http://localhost:5000`

---

## 📑 Endpoints

| Method | Endpoint            | Description              |
|--------|---------------------|--------------------------|
| GET    | `/api/contratos`    | List all contracts       |
| POST   | `/api/contratos`    | Create a new contract    |
| PUT    | `/api/contratos/:id`| Update an existing one   |
| GET    | `/api/tarifas`      | List all tariffs         |

---

## 🧪 Testing (optional)

You can use tools like:
- [Postman](https://www.postman.com/)
- [curl](https://curl.se/)
- Or the built-in Swagger UI at: `http://localhost:5000/swagger`

---

## ✅ CORS Setup

In `Program.cs`, CORS is restricted to frontend hosted at:

```csharp
.WithOrigins("http://localhost:5173")
```

---

## 📜 License

MIT License – Feel free to use and adapt.

---

## 👨‍💻 Author

Developed by Andres Leveri – [LinkedIn](https://www.linkedin.com/in/andres-leveri/)