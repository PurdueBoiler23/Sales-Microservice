using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sales_Microservice.Data;
using Sales_Microservice.Services;
using Sales_Microservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:5173") // React app URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Entity Framework Core with SQL Server
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sales Microservice API",
        Version = "v1",
        Description = "A microservice for managing transactions",
        Contact = new OpenApiContact
        {
            Name = "Jacob Gaeta",
            Email = "jgaet99@gmail.com",
        }
    });
});

var app = builder.Build();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors("AllowReactApp"); // Apply CORS policy

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
