using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Materia;
using POCA.API.Responses;
using POCA.Banco.Model;
using System.Collections.Generic;

namespace POCA.API.EndPoints
{
    public static class MateriaExtension
    {
        public static void AddEndpointsMaterias(this WebApplication app)
        {
            var group = app.MapGroup("/materias")
                           .WithTags("Materias")
                           .WithOpenApi();

            // GET all materias with related data
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var materias = await context.TbMaterias
                    .Select(m => new MateriaResponse(
                        m.IdMateria,
                        m.NomeMateria,
                        m.TbProfessoresIdProfessors.Select(p => p.IdProfessor),
                        m.TbAlunosIdAlunos.Select(a => a.IdAluno),
                        m.TbAtividadesIdAtividades.Select(at => at.IdAtividade)
                    ))
                    .ToListAsync();

                return Results.Ok(materias);
            });


            // GET materia by ID with related data
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias
                    .Where(m => m.IdMateria == id)
                    .Select(m => new MateriaResponse(
                        m.IdMateria,
                        m.NomeMateria,
                        m.TbProfessoresIdProfessors.Select(p => p.IdProfessor),
                        m.TbAlunosIdAlunos.Select(a => a.IdAluno),
                        m.TbAtividadesIdAtividades.Select(at => at.IdAtividade)
                    ))
                    .FirstOrDefaultAsync();

                return materia is null ? Results.NotFound() : Results.Ok(materia);
            });


            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                        [FromBody] MateriaRequest request) =>
            {
                if (string.IsNullOrWhiteSpace(request.NomeMateria))
                {
                    return Results.BadRequest("NomeMateria is required");
                }

                var materia = new TbMateria
                {
                    NomeMateria = request.NomeMateria,
                    TbProfessoresIdProfessors = new List<TbProfessore>(), // empty lists
                    TbAlunosIdAlunos = new List<TbAluno>(),
                    TbAtividadesIdAtividades = new List<TbAtividade>()
                };

                // Do NOT add related entities here, ignore IdsProfessores, IdsAlunos, IdsAtividades

                context.TbMaterias.Add(materia);
                await context.SaveChangesAsync();

                return Results.Created($"/materias/{materia.IdMateria}", materia);
            });


            // DELETE materia
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias
                    .Include(m => m.TbProfessoresIdProfessors)
                    .Include(m => m.TbAlunosIdAlunos)
                    .Include(m => m.TbAtividadesIdAtividades)
                    .FirstOrDefaultAsync(m => m.IdMateria == id);

                if (materia is null)
                    return Results.NotFound();

                materia.TbProfessoresIdProfessors.Clear();
                materia.TbAlunosIdAlunos.Clear();
                materia.TbAtividadesIdAtividades.Clear();

                await context.SaveChangesAsync(); // Save clearing relationships

                context.TbMaterias.Remove(materia);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });


            // Add professor to materia
            group.MapPost("/{idMateria}/professores/{idProfessor}", async (DbPocaContext context, int idMateria, int idProfessor) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbProfessoresIdProfessors).FirstOrDefaultAsync(m => m.IdMateria == idMateria);
                var professor = await context.TbProfessores.FindAsync(idProfessor);

                if (materia is null || professor is null)
                    return Results.NotFound();

                if (!materia.TbProfessoresIdProfessors.Any(p => p.IdProfessor == idProfessor))
                    materia.TbProfessoresIdProfessors.Add(professor);

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{idMateria}/professores/{idProfessor}", async (DbPocaContext context, int idMateria, int idProfessor) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbProfessoresIdProfessors).FirstOrDefaultAsync(m => m.IdMateria == idMateria);

                if (materia is null)
                    return Results.NotFound();

                var professor = materia.TbProfessoresIdProfessors.FirstOrDefault(p => p.IdProfessor == idProfessor);
                if (professor is null)
                    return Results.NotFound();

                materia.TbProfessoresIdProfessors.Remove(professor);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // Add aluno to materia
            group.MapPost("/{idMateria}/alunos/{idAluno}", async (DbPocaContext context, int idMateria, int idAluno) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbAlunosIdAlunos).FirstOrDefaultAsync(m => m.IdMateria == idMateria);
                var aluno = await context.TbAlunos.FindAsync(idAluno);

                if (materia is null || aluno is null)
                    return Results.NotFound();

                if (!materia.TbAlunosIdAlunos.Any(a => a.IdAluno == idAluno))
                    materia.TbAlunosIdAlunos.Add(aluno);

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{idMateria}/alunos/{idAluno}", async (DbPocaContext context, int idMateria, int idAluno) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbAlunosIdAlunos).FirstOrDefaultAsync(m => m.IdMateria == idMateria);

                if (materia is null)
                    return Results.NotFound();

                var aluno = materia.TbAlunosIdAlunos.FirstOrDefault(a => a.IdAluno == idAluno);
                if (aluno is null)
                    return Results.NotFound();

                materia.TbAlunosIdAlunos.Remove(aluno);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // Add atividade to materia
            group.MapPost("/{idMateria}/atividades/{idAtividade}", async (DbPocaContext context, int idMateria, int idAtividade) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbAtividadesIdAtividades).FirstOrDefaultAsync(m => m.IdMateria == idMateria);
                var atividade = await context.TbAtividades.FindAsync(idAtividade);

                if (materia is null || atividade is null)
                    return Results.NotFound();

                if (!materia.TbAtividadesIdAtividades.Any(a => a.IdAtividade == idAtividade))
                    materia.TbAtividadesIdAtividades.Add(atividade);

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{idMateria}/atividades/{idAtividade}", async (DbPocaContext context, int idMateria, int idAtividade) =>
            {
                var materia = await context.TbMaterias.Include(m => m.TbAtividadesIdAtividades).FirstOrDefaultAsync(m => m.IdMateria == idMateria);

                if (materia is null)
                    return Results.NotFound();

                var atividade = materia.TbAtividadesIdAtividades.FirstOrDefault(a => a.IdAtividade == idAtividade);
                if (atividade is null)
                    return Results.NotFound();

                materia.TbAtividadesIdAtividades.Remove(atividade);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}