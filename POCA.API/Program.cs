using Microsoft.EntityFrameworkCore;
using POCA.API.Endpoints;
using POCA.API.EndPoints;
using POCA.API.Services;
using POCA.Banco.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<DbPocaContext>(options =>
    options.UseMySQL(builder.Configuration["ConnectionStrings:POCADB"]));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<AtividadeService>();
builder.Services.AddHttpClient<GENAIService>();

// Google ID Token validation (NO redirects)
builder.Services.AddSingleton(new GoogleAuthConfig
{
    ClientId = builder.Configuration["GoogleAuth:ClientId"]
});

// JSON options
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// CORS
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Map all endpoints
app.AddEndpointsQuestoes();
app.AddEndpointsAlunos();
app.AddEndpointsProfessores();
app.AddEndpointsMaterias();
app.AddEndpointsPessoas();
app.AddEndpointsAtividades();
app.AddEndpointsRespostas();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Google login endpoint (token validation)
app.AddEndpointsGoogleAuth();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
