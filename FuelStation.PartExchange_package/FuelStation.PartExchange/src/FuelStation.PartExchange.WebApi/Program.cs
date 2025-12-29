using System.Reflection;
using FuelStation.PartExchange.Application.Interfaces;
using FuelStation.PartExchange.Application.Services;
using FuelStation.PartExchange.Infrastructure.Dependency;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); 
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FuelStation Part Exchange API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<OrderPartService>();
builder.Services.AddSingleton<ICacheProvider, CacheProvider>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();