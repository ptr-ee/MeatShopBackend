using MeatShotBackend.Data;
using MeatShotBackend.Services;
using MeatShotBackend.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Load configuration (appsettings.json)
var configuration = builder.Configuration;

// 2️⃣ Add DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 3️⃣ Register Services (DI)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMeatService, MeatService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISalesService, SalesService>();

// 4️⃣ AutoMapper (if using it)
builder.Services.AddAutoMapper(typeof(Program));

// 5️⃣ Add Controllers
builder.Services.AddControllers();

// 6️⃣ Add Swagger (Recommended during development)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 7️⃣ Use Swagger (only in development, remove for prod)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8️⃣ Custom JWT Middleware (replaces UseAuthentication)
app.UseMiddleware<JwtMiddleware>();

// 9️⃣ Map Controllers
app.MapControllers();

// 🔟 Run app
app.Run();
