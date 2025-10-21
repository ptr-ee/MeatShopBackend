using MeatShotBackend.Data;
using MeatShotBackend.Services;
using MeatShotBackend.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️ Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin(); // TODO: Restrict in production
    });
});

// 2️ Load configuration (appsettings.json)
var configuration = builder.Configuration;

// 3️ Add DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 4️ Register Services (DI)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMeatService, MeatService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISalesService, SalesService>();

// 5️ AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 6️ Add Controllers
builder.Services.AddControllers();

// 7️ Add Swagger (Recommended during development)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 8️ Enable Swagger UI (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9️ Enable CORS BEFORE JWT Middleware
app.UseCors("AllowAll");

// 10 Add Custom JWT Middleware
app.UseMiddleware<JwtMiddleware>();

// 1️1️ Use Authorization if any controllers require [Authorize]
app.UseAuthorization();

// 1️2️ Map Controllers
app.MapControllers();

// 1️3️ Run app
app.Run();
