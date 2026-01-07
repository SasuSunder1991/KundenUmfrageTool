using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Profiles;
using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

// ---------- DATABASE ----------
var connectionString = config.GetConnectionString("Default");
services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ---------- SERVICES ----------
services.AddAutoMapper(typeof(MappingProfile));
services.AddScoped<TokenService>();
services.AddScoped<AdminService>();
services.AddScoped<IRestaurantService, RestaurantService>();
services.AddScoped<IReportService, ReportService>();
services.AddScoped<IUserService, UserService>();

// ---------- AUTHORIZATION POLICIES ----------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("QM", policy =>
        policy.RequireClaim("roleId", "1"));   // 1 = QM

    options.AddPolicy("RestaurantManager", policy =>
        policy.RequireClaim("roleId", "2"));   // 2 = RM
});


// ---------- CORS ----------
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200", "https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ---------- CONTROLLERS + JSON OPTIONS ----------
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = 
    System.Text.Json.JsonNamingPolicy.CamelCase;   
});

services.AddEndpointsApiExplorer();

// ---------- SWAGGER ----------
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "KundenUmfrageTool.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT eingeben: Bearer <token>",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ---------- AUTH / JWT ----------
var jwt = config.GetSection("Jwt");

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)),
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

// ---------- BUILD ----------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
