using Microsoft.EntityFrameworkCore;
using KundenUmfrageTool.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Datenbankverbindung aktivieren (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    )
);

// Controller + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
