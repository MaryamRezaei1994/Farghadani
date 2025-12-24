using FuelStation.PartExchange.Application.Services;
using FuelStation.PartExchange.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// In-memory caching for services
builder.Services.AddMemoryCache();

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

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
