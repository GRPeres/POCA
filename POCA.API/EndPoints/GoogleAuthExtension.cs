using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POCA.API.Response;
using POCA.Banco.Model;
using System.Security.Claims;
using System.Text.Json;

namespace POCA.API.EndPoints;

public static class GoogleAuthExtension
{
    public static void AddEndpointsGoogleAuth(this WebApplication app)
    {
        var group = app.MapGroup("/auth/google")
                       .WithTags("GoogleAuth");

        // Step 1 — Backend gives frontend the login URL
        group.MapGet("/login-url", (
            IOptions<ExternalAuthSettings> opts
        ) =>
        {
            var backend = opts.Value.BackendBaseUrl.TrimEnd('/');
            var loginUrl = $"{backend}/auth/google/login";

            return Results.Ok(new { url = loginUrl });
        });

        // Step 2 — User starts Google login
        group.MapMethods("/login", new[] { "GET" }, (
             IOptions<ExternalAuthSettings> opts
         ) =>
        {
            var backend = opts.Value.BackendBaseUrl.TrimEnd('/');

            var props = new AuthenticationProperties
            {
                RedirectUri = $"{backend}/auth/google/callback"
            };

            return Results.Challenge(props, ["Google"]);
        });


        // Step 3 — Google returns here
        group.MapGet("/callback", async (
            HttpContext http,
            DbPocaContext db,
            IOptions<ExternalAuthSettings> opts
        ) =>
        {
            try
            {
                var result = await http.AuthenticateAsync("Google");

                if (!result.Succeeded)
                {
                    var failHtml = @"
            <html>
            <body>
            <h3>Google login failed or already processed.</h3>
            <script>window.close();</script>
            </body>
            </html>";
                    return Results.Content(failHtml, "text/html");
                }

                var email = result.Principal.FindFirstValue(ClaimTypes.Email);
                if (email is null)
                {
                    var failHtml = @"
            <html>
            <body>
            <h3>Google did not return an email.</h3>
            <script>window.close();</script>
            </body>
            </html>";
                    return Results.Content(failHtml, "text/html");
                }

                // Lookup or create user
                var pessoa = await db.TbPessoas
                    .Include(p => p.TbAlunosIdAlunos)
                    .Include(p => p.TbProfessoresIdProfessors)
                    .FirstOrDefaultAsync(p => p.LoginPessoa == email);

                if (pessoa == null)
                {
                    pessoa = new TbPessoa
                    {
                        LoginPessoa = email,
                        SenhaPessoa = "", // Google login → no password
                        BoolProfessorPessoa = 0
                    };

                    db.TbPessoas.Add(pessoa);
                    await db.SaveChangesAsync();
                }

                var authResponse = new PessoaAuthResponse(
                    pessoa.IdPessoa,
                    pessoa.LoginPessoa,
                    pessoa.BoolProfessorPessoa == 1,
                    pessoa.TbAlunosIdAlunos.FirstOrDefault()?.IdAluno,
                    pessoa.TbProfessoresIdProfessors.FirstOrDefault()?.IdProfessor,
                    $"google-{Guid.NewGuid()}"
                );

                var json = JsonSerializer.Serialize(authResponse);
                var frontendUrl = opts.Value.FrontendBaseUrl + "/login";
                var redirectUrl = $"{frontendUrl}?googleToken={Uri.EscapeDataString(json)}";

                var html = $@"
        <html>
        <body>
        <script>
            if (window.opener) {{
                 window.opener.location.href = '{redirectUrl}';
            }}
            window.close();
        </script>
        </body>
        </html>";

                return Results.Content(html, "text/html");
            }
            catch
            {
                var fallbackHtml = @"
        <html>
        <body>
        <h3>Google login could not be processed.</h3>
        <script>window.close();</script>
        </body>
        </html>";
                return Results.Content(fallbackHtml, "text/html");
            }
        });
    }
}
