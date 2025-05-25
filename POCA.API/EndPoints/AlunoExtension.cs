using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;

using POCA.Banco.Model;
namespace POCA.API.EndPoints
{
    public static class AlunoExtension
    {
     public static void AddEndpointsAlunos(this WebApplication app)
        {
            var group = app.MapGroup("/alunos")
                           .WithTags("Alunos");

            // GET alunos
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbAlunos.ToListAsync());
            });

            // GET alunos by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var aluno = await context.TbAlunos.FindAsync(id);
                return aluno is null ? Results.NotFound() : Results.Ok(aluno);
            });

            // POST new alunos
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] AlunoRequest request) =>
            {
                var aluno = new TbAluno
                {
                    NomeAluno = request.NomeAluno,
                    IdadeAluno = request.IdadeAluno,
                    ProgressoAluno = request.ProgressoAluno,
                    ContatoAluno = request.ContatoAluno,
                };

                context.TbAlunos.Add(aluno);
                await context.SaveChangesAsync();
                return Results.Created($"/alunos/{aluno.IdAluno}", aluno);
            });

            // PUT update alunos
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] AlunoRequest request) =>
            {
                var aluno = await context.TbAlunos.FindAsync(id);
                if (aluno is null) return Results.NotFound();

                aluno.NomeAluno = request.NomeAluno;
                aluno.IdadeAluno = request.IdadeAluno;
                aluno.ProgressoAluno = request.ProgressoAluno;
                aluno.ContatoAluno = request.ContatoAluno;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE alunos
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var aluno = await context.TbAlunos.FindAsync(id);
                if (aluno is null) return Results.NotFound();

                context.TbAlunos.Remove(aluno);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
