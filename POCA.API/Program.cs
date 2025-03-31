using Microsoft.EntityFrameworkCore;
using POCA.API.Endpoints;
using POCA.Banco;
using POCA.Banco.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// MySQL configuration - ensure connection string is in appsettings
builder.Services.AddDbContext<DbPocaContext>(options =>
    options.UseMySQL(builder.Configuration["ConnectionStrings:POCADB"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Prevents circular reference issues in JSON responses
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Open CORS policy - tighten for production
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.AddEndpointsQuestoes();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();