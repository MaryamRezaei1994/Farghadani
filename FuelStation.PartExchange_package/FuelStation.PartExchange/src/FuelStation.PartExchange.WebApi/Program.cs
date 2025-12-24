using FuelStation.PartExchange.Application.Services;
using FuelStation.PartExchange.Infrastructure;
using FuelStation.PartExchange.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// In-memory caching for services
builder.Services.AddMemoryCache();

// DbContext (explicit registration using a local connection string)
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=Abc@1234;Include Error Detail=true";
builder.Services.AddDbContext<PartExchangeDbContext>(options =>
    options.UseNpgsql(connectionString));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FuelStation.PartExchange API", Version = "v1" });
});

// Register infrastructure (DbContext, repositories, etc.)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<PartRequestService>();

// No authentication/authorization here — handled by API Gateway

var app = builder.Build();

// Redirect root to Swagger UI
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FuelStation.PartExchange API v1");
    options.RoutePrefix = string.Empty; // serve swagger at app root if desired
});

app.UseHttpsRedirection();
    // Authentication and Authorization handled by API Gateway; not applied here
app.MapControllers();

app.Run();
