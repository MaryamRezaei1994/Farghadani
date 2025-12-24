# Farghadani (Part Exchange) - .NET 8

This is a minimal Clean Architecture scaffold for a Part Exchange Web API using .NET 8, EF Core, and Swagger.

Structure:
- Domain: Entities and repository interfaces ([FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Domain](FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Domain))
- Application: DTOs and services ([FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Application](FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Application))
- Infrastructure: EF Core DbContext and repository implementation ([FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Infrastructure](FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Infrastructure))
- WebApi: Controllers and Program.cs ([FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi](FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi))

Getting started

1. Install .NET 8 SDK (if not already installed).
2. Update the connection string in `FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi/appsettings.json`.
3. Restore packages and run:

```powershell
cd "e:\Rezaei\Backend\Farghadani"
dotnet restore "Farghadani.sln"
dotnet ef database update # optional - requires dotnet-ef tool and a configured DB provider
dotnet run --project FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi -c Debug
```

4. Open Swagger UI: https://localhost:5001/swagger (port shown in console)

APIs (Swagger will list them):
- POST /api/partrequests
- GET /api/orders
- POST /api/orders/{id}/confirm
- POST /api/auth/token

Notes
- This scaffold uses PostgreSQL by default (Npgsql). Change the connection string in `appsettings.json` to point to your database.
- `global.json` pins the SDK to `9.0.308` for consistent tooling on developer machines.
- Add migrations with `dotnet ef migrations add Initial --project FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.Infrastructure` and apply with `dotnet ef database update`.
- You can extend the Application layer with MediatR, FluentValidation, or other patterns as needed.

If you need help setting up the database, CI, or running locally, open an issue on the repository: https://github.com/MaryamRezaei1994/Farghadani
