using KundenUmfrageTool.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Datenbankverbindung aktivieren (MySQL)
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Controller + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App bauen
var app = builder.Build();

// Swagger aktivieren (API-Doku)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard-Middleware
//app.UseHttpsRedirection();  // deaktiviert, da kein HTTPS
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
