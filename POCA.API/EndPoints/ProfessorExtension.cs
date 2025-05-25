using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;

using POCA.Banco.Model;
namespace POCA.API.EndPoints
{
    public static class ProfessorExtension
    {
        public static void AddEndpointsProfessores(this WebApplication app)
        {
            var group = app.MapGroup("/professores")
                           .WithTags("Professores");

            // GET professor
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbProfessores.ToListAsync());
            });

            // GET professor by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var professor = await context.TbProfessores.FindAsync(id);
                return professor is null ? Results.NotFound() : Results.Ok(professor);
            });

            // POST new professor
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] ProfessorRequest request) =>
            {
                var professor = new TbProfessore
                {
                    NomeProfessor = request.NomeProfessor,
                    MateriaProfessor = request.MateriaProfessor,
                    ContatoProfessor = request.ContatoProfessor,
                    LoginProfessor = request.LoginProfessor,
                    SenhaProfessor = request.SenhaProfessor
                };

                context.TbProfessores.Add(professor);
                await context.SaveChangesAsync();
                return Results.Created($"/Professores/{professor.IdProfessor}", professor);
            });

            // PUT update professor
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] ProfessorRequest request) =>
            {
                var professor = await context.TbProfessores.FindAsync(id);
                if (professor is null) return Results.NotFound();

                professor.NomeProfessor = request.NomeProfessor;
                professor.MateriaProfessor = request.MateriaProfessor;
                professor.ContatoProfessor = request.ContatoProfessor;
                professor.LoginProfessor = request.LoginProfessor;
                professor.SenhaProfessor = request.SenhaProfessor;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE professor
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var professor = await context.TbProfessores.FindAsync(id);
                if (professor is null) return Results.NotFound();

                context.TbProfessores.Remove(professor);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
