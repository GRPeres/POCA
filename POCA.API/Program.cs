using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POCA.API.Endpoints;
using POCA.API.EndPoints;
using POCA.API.Services;
using POCA.Banco.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ###############################
// DATABASE
// ###############################
builder.Services.AddDbContext<DbPocaContext>(options =>
    options.UseMySQL(builder.Configuration["ConnectionStrings:POCADB"]));

// ###############################
// SWAGGER
// ###############################
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ###############################
// SERVICES
// ###############################
builder.Services.AddScoped<AtividadeService>();
builder.Services.AddHttpClient<GENAIService>();

// ###############################
// GOOGLE AUTH + COOKIES
// ###############################
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "Google";
    })
    .AddCookie("Cookies", options =>
    {
        // REQUIRED for OAuth flow with Blazor WASM
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        // IMPORTANT — without this callback won't work
        options.Events.OnRedirectToLogin = ctx =>
        {
            ctx.Response.StatusCode = 401; // prevents automatic redirects
            return Task.CompletedTask;
        };
    })
    .AddGoogle("Google", options =>
    {
        options.ClientId = builder.Configuration["ExternalAuth:GoogleClientId"];
        options.ClientSecret = builder.Configuration["ExternalAuth:GoogleClientSecret"];

        // Must be EXACTLY what's in Google Cloud Console
        options.CallbackPath = "/signin-google";

        options.SaveTokens = true;

        // REQUIRED for cross-site OAuth
        options.CorrelationCookie.SameSite = SameSiteMode.None;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
    });

// Required for Blazor authorization
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// ###############################
// JSON OPTIONS
// ###############################
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// ###############################
// CORS
// ###############################
builder.Services.AddCors(options =>
{
    options.AddPolicy("poca_cors", policy =>
    {
        policy.WithOrigins(
            "https://localhost:7165",                                        // local frontend
            "http://poca-test.s3-website.us-east-2.amazonaws.com",          // prod front
            "https://poca-test.s3-website.us-east-2.amazonaws.com"          // (if https)
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // REQUIRED FOR OAUTH
    });
});

// ###############################
// URLS
// ###############################
builder.Services.Configure<ExternalAuthSettings>(
    builder.Configuration.GetSection("ExternalAuth"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ExternalAuthSettings>>().Value);

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseCors("poca_cors");

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Unspecified,
    Secure = CookieSecurePolicy.Always
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ###############################
// ENDPOINTS
// ###############################
app.AddEndpointsQuestoes();
app.AddEndpointsAlunos();
app.AddEndpointsProfessores();
app.AddEndpointsMaterias();
app.AddEndpointsPessoas();
app.AddEndpointsAtividades();
app.AddEndpointsRespostas();
app.AddEndpointsGoogleAuth();

// ###############################
// SWAGGER
// ###############################
app.UseSwagger();
app.UseSwaggerUI();

// ###############################
// HTTPS (DEV ONLY)
// ###############################

// Only bind to HTTPS in Development
if (app.Environment.IsDevelopment())
{
    app.Urls.Clear();
    app.Urls.Add("https://localhost:5001");  // backend
}

app.Run();
