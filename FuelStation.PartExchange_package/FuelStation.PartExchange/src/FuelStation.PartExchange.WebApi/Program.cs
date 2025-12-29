using FuelStation.PartExchange.Application.Interfaces;
using FuelStation.PartExchange.Application.Services;
using FuelStation.PartExchange.Infrastructure.Dependency;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ فقط این خط — با builder.Configuration
builder.Services.AddInfrastructure(builder.Configuration);

// سرویس‌های لایه Application
builder.Services.AddScoped<OrderPartService>();
builder.Services.AddSingleton<ICacheProvider, CacheProvider>();

var app = builder.Build();

// app.Use((context, next) =>
// {
//     if (context.Request.Path == "/")
//     {
//         context.Response.Redirect("/swagger");
//         return Task.CompletedTask;
//     }
//     return next();
// });

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();