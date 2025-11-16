using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Step 1 — Frontend calls: GET /auth/google/login-url
        // API returns the Google login URL
        group.MapGet("/login-url", () =>
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = "/auth/google/callback"
            };

            var challenge = Results.Challenge(props, ["Google"]) as ChallengeHttpResult;

            // Extract Google URL from the challenge
            var redirectUrl = challenge?.Properties?.RedirectUri;
            return Results.Ok(new { url = redirectUrl });
        });

        // Step 2 — Google redirects to callback
        group.MapGet("/callback", async (
            HttpContext http,
            [FromServices] DbPocaContext db) =>
        {
            var result = await http.AuthenticateAsync("Google");
            if (!result.Succeeded)
                return Results.BadRequest("Google authentication failed");

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            if (email is null)
                return Results.BadRequest("Google did not return email.");

            // Look up or create user
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

            // Instead of redirecting to Blazor,
            // we output JS that posts the auth data to the client
            var json = JsonSerializer.Serialize(authResponse);

            var html = $@"
                        <html>
                        <body>
                        <script>
                            // Send auth data to opener window (Blazor)
                            window.opener.postMessage(
                                {json},
                                '*'
                            );
                            window.close();
                        </script>
                        </body>
                        </html>";

            return Results.Content(html, "text/html");
        });
    }
}
