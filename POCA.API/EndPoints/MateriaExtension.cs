using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;

using POCA.Banco.Model;
namespace POCA.API.EndPoints
{
    public static class MateriaExtension
    {
        public static void AddEndpointsMaterias(this WebApplication app)
        {
            var group = app.MapGroup("/materias")
                           .WithTags("Materias");

            // GET materia
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbMaterias.ToListAsync());
            });

            // GET materia by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                return materia is null ? Results.NotFound() : Results.Ok(materia);
            });

            // POST new materia
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] MateriaRequest request) =>
            {
                var materia = new TbMateria
                {
                };

                context.TbMaterias.Add(materia);
                await context.SaveChangesAsync();
                return Results.Created($"/Materias/{materia.IdMateria}", materia);
            });

            // PUT update materia
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] MateriaRequest request) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                if (materia is null) return Results.NotFound();

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
            // Additional endpoints for relationships
            group.MapPost("/{idMateria}/professores/{idProfessor}",
                async ([FromServices] DbPocaContext context, int idProfessor, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbProfessoresIdProfessors)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var professor = await context.TbProfessores.FindAsync(idProfessor);
                    if (professor is null)
                        return Results.NotFound("Professor not found");

                    if (!materia.TbProfessoresIdProfessors.Any(m => m.IdProfessor == idProfessor))
                    {
                        materia.TbProfessoresIdProfessors.Add(professor);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idMateria}/professores/{idProfessor}",
                async ([FromServices] DbPocaContext context, int idProfessor, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbProfessoresIdProfessors)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var professor = materia.TbProfessoresIdProfessors
                        .FirstOrDefault(m => m.IdProfessor == idProfessor);

                    if (professor is null)
                        return Results.NotFound("Professor not associated with materia");

                    materia.TbProfessoresIdProfessors.Remove(professor);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
            group.MapPost("/{idMateria}/alunos/{idAluno}",
                async ([FromServices] DbPocaContext context, int idAluno, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbAlunosIdAlunos)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var aluno = await context.TbAlunos.FindAsync(idAluno);
                    if (aluno is null)
                        return Results.NotFound("Aluno not found");

                    if (!materia.TbAlunosIdAlunos.Any(a => a.IdAluno == idAluno))
                    {
                        materia.TbAlunosIdAlunos.Add(aluno);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idMateria}/alunos/{idAluno}",
                async ([FromServices] DbPocaContext context, int idAluno, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbAlunosIdAlunos)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var aluno = materia.TbAlunosIdAlunos
                        .FirstOrDefault(a => a.IdAluno == idAluno);

                    if (aluno is null)
                        return Results.NotFound("Aluno not associated with materia");

                    materia.TbAlunosIdAlunos.Remove(aluno);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
            group.MapPost("/{idMateria}/atividades/{idAtividade}",
                async ([FromServices] DbPocaContext context, int idAtividade, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbAtividadesIdAtividades)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var atividade = await context.TbAtividades.FindAsync(idAtividade);
                    if (atividade is null)
                        return Results.NotFound("Atividade not found");

                    if (!materia.TbAtividadesIdAtividades.Any(a => a.IdAtividade == idAtividade))
                    {
                        materia.TbAtividadesIdAtividades.Add(atividade);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idMateria}/atividades/{idAtividade}",
                async ([FromServices] DbPocaContext context, int idAtividade, int idMateria) =>
                {
                    var materia = await context.TbMaterias
                        .Include(p => p.TbAtividadesIdAtividades)
                        .FirstOrDefaultAsync(p => p.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    var atividade = materia.TbAtividadesIdAtividades
                        .FirstOrDefault(a => a.IdAtividade == idAtividade);

                    if (atividade is null)
                        return Results.NotFound("Atividade not associated with materia");

                    materia.TbAtividadesIdAtividades.Remove(atividade);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
        }
    }
}
