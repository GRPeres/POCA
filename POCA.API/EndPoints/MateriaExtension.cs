using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class MateriaExtension
    {
        public static void AddEndpointsMaterias(this WebApplication app)
        {
            var group = app.MapGroup("/materias")
                           .WithTags("Materias");

            // GET all materias with related data
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbMaterias
                    .Include(m => m.TbProfessoresIdProfessors)
                    .Include(m => m.TbAlunosIdAlunos)
                    .Include(m => m.TbAtividadesIdAtividades)
                    .ToListAsync());
            });

            // GET materia by ID with related data
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias
                    .Include(m => m.TbProfessoresIdProfessors)
                    .Include(m => m.TbAlunosIdAlunos)
                    .Include(m => m.TbAtividadesIdAtividades)
                    .FirstOrDefaultAsync(m => m.IdMateria == id);

                return materia is null ? Results.NotFound() : Results.Ok(materia);
            });

            // POST new materia
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] MateriaRequest request) =>
            {
                // Find related entities
                var professor = await context.TbProfessores.FindAsync(request.IdProfessorMateria);
                var aluno = await context.TbAlunos.FindAsync(request.IdAlunoMateria);

                if (professor == null || aluno == null)
                {
                    return Results.BadRequest("Professor or Aluno not found");
                }

                var materia = new TbMateria
                {
                    NomeMateria = request.NomeMateria,
                    TbProfessoresIdProfessors = new List<TbProfessore> { professor },
                    TbAlunosIdAlunos = new List<TbAluno> { aluno }
                };

                context.TbMaterias.Add(materia);
                await context.SaveChangesAsync();
                return Results.Created($"/materias/{materia.IdMateria}", materia);
            });

            // PUT update materia
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] MateriaEditRequest request) =>
            {
                if (id != request.IdMateria)
                {
                    return Results.BadRequest("ID mismatch");
                }

                var materia = await context.TbMaterias
                    .Include(m => m.TbProfessoresIdProfessors)
                    .Include(m => m.TbAlunosIdAlunos)
                    .FirstOrDefaultAsync(m => m.IdMateria == id);

                if (materia is null) return Results.NotFound();

                // Update basic properties
                materia.NomeMateria = request.NomeMateria;

                // Update relationships if needed
                if (request.IdProfessorMateria > 0)
                {
                    var professor = await context.TbProfessores.FindAsync(request.IdProfessorMateria);
                    if (professor != null)
                    {
                        materia.TbProfessoresIdProfessors.Clear();
                        materia.TbProfessoresIdProfessors.Add(professor);
                    }
                }

                if (request.IdAlunoMateria > 0)
                {
                    var aluno = await context.TbAlunos.FindAsync(request.IdAlunoMateria);
                    if (aluno != null)
                    {
                        materia.TbAlunosIdAlunos.Clear();
                        materia.TbAlunosIdAlunos.Add(aluno);
                    }
                }

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE materia
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                if (materia is null) return Results.NotFound();

                context.TbMaterias.Remove(materia);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}