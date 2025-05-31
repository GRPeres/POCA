using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Professor;
using POCA.API.Responses;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class ProfessorExtension
    {
        public static void AddEndpointsProfessores(this WebApplication app)
        {
            var group = app.MapGroup("/professores")
                           .WithTags("Professores")
                           .WithOpenApi();

            // GET all professores with basic info
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var professores = await context.TbProfessores
                    .Select(p => new ProfessorResponse(
                        p.IdProfessor,
                        p.NomeProfessor,
                        p.MateriaProfessor,
                        p.ContatoProfessor,
                        p.TbMateriasIdMateria.Select(m => m.IdMateria),
                        p.TbPessoasIdPessoas.Select(p => p.IdPessoa)
                    ))
                    .ToListAsync();

                return Results.Ok(professores);
            });

            // GET professor by ID with details
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var professor = await context.TbProfessores
                    .Include(p => p.TbMateriasIdMateria)
                    .Include(p => p.TbPessoasIdPessoas)
                    .FirstOrDefaultAsync(p => p.IdProfessor == id);

                if (professor is null)
                    return Results.NotFound();

                var response = new ProfessorResponse(
                    professor.IdProfessor,
                    professor.NomeProfessor,
                    professor.MateriaProfessor,
                    professor.ContatoProfessor,
                    professor.TbMateriasIdMateria.Select(m => m.IdMateria),
                    professor.TbPessoasIdPessoas.Select(p => p.IdPessoa)
                );

                return Results.Ok(response);
            });

            // POST new professor
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] ProfessorCreateRequest request) =>
            {
                var professor = new TbProfessore
                {
                    NomeProfessor = request.NomeProfessor,
                    MateriaProfessor = request.MateriaProfessor,
                    ContatoProfessor = request.ContatoProfessor
                };

                context.TbProfessores.Add(professor);
                await context.SaveChangesAsync();

                var response = new ProfessorResponse(
                    professor.IdProfessor,
                    professor.NomeProfessor,
                    professor.MateriaProfessor,
                    professor.ContatoProfessor,
                    null,
                    null
                );

                return Results.Created($"/professores/{professor.IdProfessor}", response);
            });

            // PUT update professor
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] ProfessorEditRequest request) =>
            {
                if (id != request.IdProfessor)
                    return Results.BadRequest("ID mismatch");

                var professor = await context.TbProfessores.FindAsync(id);
                if (professor is null)
                    return Results.NotFound();

                professor.NomeProfessor = request.NomeProfessor;
                professor.MateriaProfessor = request.MateriaProfessor;
                professor.ContatoProfessor = request.ContatoProfessor;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE professor
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var professor = await context.TbProfessores.FindAsync(id);
                if (professor is null)
                    return Results.NotFound();

                context.TbProfessores.Remove(professor);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // Additional endpoints for relationships
            group.MapPost("/{idProfessor}/materias/{idMateria}",
                async ([FromServices] DbPocaContext context, int idProfessor, int idMateria) =>
                {
                    var professor = await context.TbProfessores
                        .Include(p => p.TbMateriasIdMateria)
                        .FirstOrDefaultAsync(p => p.IdProfessor == idProfessor);

                    if (professor is null)
                        return Results.NotFound("Professor not found");

                    var materia = await context.TbMaterias.FindAsync(idMateria);
                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    if (!professor.TbMateriasIdMateria.Any(m => m.IdMateria == idMateria))
                    {
                        professor.TbMateriasIdMateria.Add(materia);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idProfessor}/materias/{idMateria}",
                async ([FromServices] DbPocaContext context, int idProfessor, int idMateria) =>
                {
                    var professor = await context.TbProfessores
                        .Include(p => p.TbMateriasIdMateria)
                        .FirstOrDefaultAsync(p => p.IdProfessor == idProfessor);

                    if (professor is null)
                        return Results.NotFound("Professor not found");

                    var materia = professor.TbMateriasIdMateria
                        .FirstOrDefault(m => m.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not associated with professor");

                    professor.TbMateriasIdMateria.Remove(materia);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
        }
    }
}