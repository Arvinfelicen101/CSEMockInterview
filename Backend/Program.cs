using Backend.Context;
using Backend.Middlewares;
using Backend.Models;
using Backend.Repository.Auth;
using Backend.Repository.Importer;
using Backend.Repository.Question;
using Backend.Repository.SubCategory;
using Backend.Repository.UserManagement;
using Backend.Services.Authentication;
using Backend.Services.Importer;
using Backend.Services.Question;
using Backend.Services.Question.QuestionValidator;
using Backend.Services.SubCategory;
using Backend.Services.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Backend.Repository.ChoicesManagement;
using Backend.Repository.ParagraphManagement;
using Backend.Repository.YearPeriodManagement;
using Backend.Services.ChoicesManagement;
using Backend.Services.ParagraphManagement;
using Backend.Services.YearPeriodManagement;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMemoryCache();

//DI Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IImporterRepository, ImporterRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IChoicesRepository, ChoicesRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IYearPeriodRepository, YearPeriodManagement>();
builder.Services.AddScoped<IParagraphManagementRepository, ParagraphManagementRepository>();

//DI Services
builder.Services.AddScoped<IQuestionValidator, QuestionValidator>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserManagementServices, UserManagementServices>();
builder.Services.AddScoped<IImporterService, ImporterService>();
builder.Services.AddScoped<IQuestionService,  QuestionService>();
builder.Services.AddScoped<IChoiceService, ChoiceService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IYearPeriodService, YearPeriodService>();
builder.Services.AddScoped<IParagraphManagementService, ParagraphManagementService>();

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

var jwtConfig = builder.Configuration.GetSection("JwtConfig");
var key = jwtConfig["key"] ?? throw new InvalidOperationException("JWT Key is missing");
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
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
