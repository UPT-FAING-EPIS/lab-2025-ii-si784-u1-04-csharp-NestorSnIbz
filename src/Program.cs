using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shorten.Areas.Identity.Data;
using Shorten.Areas.Domain;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Puerto dinámico para Azure
// ======================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// ======================
// Conexión a la base de datos
// ======================
// Debes definir "ShortenIdentityDbContextConnection" en Azure App Service → Configuration → Connection Strings
var connectionString = builder.Configuration.GetConnectionString("ShortenIdentityDbContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'ShortenIdentityDbContextConnection' not found.");

// DbContext para Identity
builder.Services.AddDbContext<ShortenIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ShortenIdentityDbContext>();

// DbContext para tu aplicación
builder.Services.AddDbContext<ShortenContext>(options =>
    options.UseSqlServer(connectionString));

// QuickGrid adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// ======================
// Pipeline HTTP
// ======================
// Solo usar HSTS en desarrollo local, en contenedor podemos deshabilitar HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // app.UseHsts(); // Descomentar solo si agregas HTTPS en Azure
}

app.UseRouting();

// No forzar HTTPS en contenedor, Azure App Service se encarga del SSL externo
// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
