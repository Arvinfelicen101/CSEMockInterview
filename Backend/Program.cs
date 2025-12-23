using Backend.Context;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Middlewares;
using Scalar.AspNetCore;
using Backend.Repository.Auth;
using Backend.Repository.Importer;
using Backend.Services.Authentication;
using Backend.Repository.UserManagement;
using Backend.Services.Importer;
using Backend.Services.UserManagement;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMemoryCache();
//DI Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IImporterRepository, ImporterRepository>();

//DI Services
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserManagementServices, UserManagementServices>();
builder.Services.AddScoped<IImporterService, ImporterService>();

//DI Middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// database config
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "CSEMockExam";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";

var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={pass}";

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Identity
builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

var jwtConfig = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();



app.Run();
