using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POCA.API.Endpoints;
using POCA.API.EndPoints;
using POCA.API.Services;
using POCA.Banco.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// DATABASE
// ===============================
builder.Services.AddDbContext<DbPocaContext>(options =>
    options.UseMySQL(builder.Configuration["ConnectionStrings:POCADB"]));

// ===============================
// SWAGGER
// ===============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===============================
// SERVICES
// ===============================
builder.Services.AddScoped<AtividadeService>();
builder.Services.AddHttpClient<GENAIService>();

// ===============================
// FORWARDED HEADERS (🔥 REQUIRED)
// ===============================
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto;

    // REQUIRED for AWS / Nginx
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// ===============================
// GOOGLE AUTH + COOKIES
// ===============================
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "Google";
    })
    .AddCookie("Cookies", options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.Events.OnRedirectToLogin = ctx =>
        {
            ctx.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    })
    .AddGoogle("Google", options =>
    {
        options.ClientId = builder.Configuration["ExternalAuth:GoogleClientId"];
        options.ClientSecret = builder.Configuration["ExternalAuth:GoogleClientSecret"];
        options.CallbackPath = "/signin-google";
        options.SaveTokens = true;

        options.CorrelationCookie.SameSite = SameSiteMode.None;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
    });

// ===============================
// MVC + AUTH
// ===============================
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// ===============================
// JSON
// ===============================
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// ===============================
// CORS
// ===============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("poca_cors", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5000",
            "http://localhost:5173",
            "http://localhost:7165",
            "https://localhost:7165",
            "http://poca-test.s3-website.us-east-2.amazonaws.com",
            "https://poca-test.s3-website.us-east-2.amazonaws.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// ===============================
// CONFIG
// ===============================
builder.Services.Configure<ExternalAuthSettings>(
    builder.Configuration.GetSection("ExternalAuth"));

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ExternalAuthSettings>>().Value);

builder.Services.AddHttpContextAccessor();


// ===============================
// PIPELINE
// ===============================
var app = builder.Build();

// 🔥 MUST BE FIRST
app.UseForwardedHeaders();

// ❌ DO NOT redirect HTTPS in EB
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors("poca_cors");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Unspecified,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireCors("poca_cors");

// ===============================
// MINIMAL ENDPOINTS (🔥 REQUIRE CORS)
// ===============================
app.AddEndpointsQuestoes();
app.AddEndpointsAlunos();
app.AddEndpointsProfessores();
app.AddEndpointsMaterias();
app.AddEndpointsPessoas();
app.AddEndpointsAtividades();
app.AddEndpointsRespostas();
app.AddEndpointsGoogleAuth();

// ===============================
// SWAGGER
// ===============================
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
