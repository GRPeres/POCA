// EndPoints/PessoaExtension.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;
using POCA.API.Requests.Pessoa;
using POCA.API.Response;
using POCA.API.Responses;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class PessoaExtension
    {
        public static void AddEndpointsPessoas(this WebApplication app)
        {
            var group = app.MapGroup("/pessoas")
                           .WithTags("Pessoas")
                           .WithOpenApi();

            // GET all pessoas (basic info)
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var pessoas = await context.TbPessoas
                    .Select(p => new PessoaResponse(
                        p.IdPessoa,
                        p.LoginPessoa,
                        p.BoolProfessorPessoa == 1,
                        p.TbAlunosIdAlunos.FirstOrDefault().IdAluno,
                        p.TbProfessoresIdProfessors.FirstOrDefault().IdProfessor,
                        null,
                        null
                    ))
                    .ToListAsync();

                return Results.Ok(pessoas);
            });

            // GET pessoa by ID with details
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var pessoa = await context.TbPessoas
                    .Include(p => p.TbAlunosIdAlunos)
                    .Include(p => p.TbProfessoresIdProfessors)
                    .FirstOrDefaultAsync(p => p.IdPessoa == id);

                if (pessoa is null)
                    return Results.NotFound();

                string? alunoNome = null;
                string? professorNome = null;

                if (pessoa.BoolProfessorPessoa == 0 && pessoa.TbAlunosIdAlunos.Any())
                {
                    alunoNome = pessoa.TbAlunosIdAlunos.First().NomeAluno;
                }
                else if (pessoa.TbProfessoresIdProfessors.Any())
                {
                    professorNome = pessoa.TbProfessoresIdProfessors.First().NomeProfessor;
                }

                var response = new PessoaResponse(
                    pessoa.IdPessoa,
                    pessoa.LoginPessoa,
                    pessoa.BoolProfessorPessoa == 1,
                    pessoa.TbAlunosIdAlunos.FirstOrDefault()?.IdAluno,
                    pessoa.TbProfessoresIdProfessors.FirstOrDefault()?.IdProfessor,
                    alunoNome,
                    professorNome
                );

                return Results.Ok(response);
            });

            // POST new pessoa
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] PessoaCreateRequest request) =>
            {
                var pessoa = new TbPessoa
                {
                    LoginPessoa = request.LoginPessoa,
                    SenhaPessoa = request.SenhaPessoa,
                    BoolProfessorPessoa = (sbyte)(request.IsProfessor ? 1 : 0)
                };

                context.TbPessoas.Add(pessoa);
                await context.SaveChangesAsync();

                // Handle relationships
                if (!request.IsProfessor && request.IdAluno.HasValue)
                {
                    var aluno = await context.TbAlunos.FindAsync(request.IdAluno.Value);
                    if (aluno != null)
                    {
                        pessoa.TbAlunosIdAlunos.Add(aluno);
                    }
                }
                else if (request.IsProfessor && request.IdProfessor.HasValue)
                {
                    var professor = await context.TbProfessores.FindAsync(request.IdProfessor.Value);
                    if (professor != null)
                    {
                        pessoa.TbProfessoresIdProfessors.Add(professor);
                    }
                }

                await context.SaveChangesAsync();

                var response = new PessoaResponse(
                    pessoa.IdPessoa,
                    pessoa.LoginPessoa,
                    pessoa.BoolProfessorPessoa == 1,
                    pessoa.TbAlunosIdAlunos.FirstOrDefault()?.IdAluno,
                    pessoa.TbProfessoresIdProfessors.FirstOrDefault()?.IdProfessor,
                    null,
                    null
                );

                return Results.Created($"/pessoas/{pessoa.IdPessoa}", response);
            });

            // PUT update pessoa
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] PessoaEditRequest request) =>
            {
                if (id != request.IdPessoa)
                    return Results.BadRequest("ID mismatch");

                var pessoa = await context.TbPessoas
                    .Include(p => p.TbAlunosIdAlunos)
                    .Include(p => p.TbProfessoresIdProfessors)
                    .FirstOrDefaultAsync(p => p.IdPessoa == id);

                if (pessoa is null)
                    return Results.NotFound();

                pessoa.LoginPessoa = request.LoginPessoa;
                pessoa.SenhaPessoa = request.SenhaPessoa;
                pessoa.BoolProfessorPessoa = (sbyte)(request.IsProfessor ? 1 : 0);

                // Update relationships
                if (!request.IsProfessor && request.IdAluno.HasValue)
                {
                    var aluno = await context.TbAlunos.FindAsync(request.IdAluno.Value);
                    if (aluno != null)
                    {
                        pessoa.TbAlunosIdAlunos.Clear();
                        pessoa.TbAlunosIdAlunos.Add(aluno);
                    }
                }
                else if (request.IsProfessor && request.IdProfessor.HasValue)
                {
                    var professor = await context.TbProfessores.FindAsync(request.IdProfessor.Value);
                    if (professor != null)
                    {
                        pessoa.TbProfessoresIdProfessors.Clear();
                        pessoa.TbProfessoresIdProfessors.Add(professor);
                    }
                }

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(context, id))
                        return Results.NotFound();
                    throw;
                }

                return Results.NoContent();
            });

            // DELETE pessoa
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var pessoa = await context.TbPessoas.FindAsync(id);
                if (pessoa is null)
                    return Results.NotFound();

                context.TbPessoas.Remove(pessoa);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // Authentication endpoint
            group.MapPost("/login", async ([FromServices] DbPocaContext context,
                                         [FromBody] PessoaLoginRequest request) =>
            {
                var pessoa = await context.TbPessoas
                    .Include(p => p.TbAlunosIdAlunos)
                    .Include(p => p.TbProfessoresIdProfessors)
                    .FirstOrDefaultAsync(p => p.LoginPessoa == request.Login &&
                                             p.SenhaPessoa == request.Senha);

                if (pessoa is null)
                    return Results.Unauthorized();

                // Generate token (simplified - use proper JWT in production)
                var token = Guid.NewGuid().ToString();

                var response = new PessoaAuthResponse(
                    pessoa.IdPessoa,
                    pessoa.LoginPessoa,
                    pessoa.BoolProfessorPessoa == 1,
                    pessoa.TbAlunosIdAlunos.FirstOrDefault()?.IdAluno,
                    pessoa.TbProfessoresIdProfessors.FirstOrDefault()?.IdProfessor,
                    token
                );

                return Results.Ok(response);
            });
        }

        private static bool PessoaExists(DbPocaContext context, int id)
        {
            return context.TbPessoas.Any(e => e.IdPessoa == id);
        }
    }
}