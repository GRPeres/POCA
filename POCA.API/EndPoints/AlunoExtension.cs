using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;
using POCA.API.Response;
using POCA.Banco;
using POCA.Banco.Model;
namespace POCA.API.EndPoints
{
    public static class AlunoExtension
    {
     public static void AddEndpointsQuestoes(this WebApplication app)
        {
            var group = app.MapGroup("/alunos")
                           .WithTags("Alunos");

            // GET aluno
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbAlunos.ToListAsync());
            });

            // GET aluno by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var aluno = await context.TbAlunos.FindAsync(id);
                return aluno is null ? Results.NotFound() : Results.Ok(aluno);
            });

            // POST new question
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] AlunoRequest request) =>
            {
                var aluno = new TbAluno
                {
                    NomeAluno = request.NomeAluno,
                    IdadeAluno = request.IdadeAluno,
                    ProgressoAluno = request.ProgressoAluno,
                    ContatoAluno = request.ContatoAluno,
                    LoginAluno = request.LoginAluno,
                    SenhaAluno = request.SenhaAluno
                };

                context.TbAlunos.Add(aluno);
                await context.SaveChangesAsync();
                return Results.Created($"/alunos/{aluno.IdAluno}", aluno);
            });

            // PUT update question
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
                aluno.LoginAluno = request.LoginAluno;
                aluno.SenhaAluno = request.SenhaAluno;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE question
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
