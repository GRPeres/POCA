using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Aluno;
using POCA.API.Requests.Atividade;
using POCA.API.Responses;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class AtividadeExtension
    {
        public static void AddEndpointsAtividades(this WebApplication app)
        {
            var group = app.MapGroup("/atividade")
                           .WithTags("Atividades")
                           .WithOpenApi();

            // GET all atividades with basic info
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var atividades = await context.TbAtividades
                    .Select(a => new AtividadeResponse(
                        a.IdAtividade,
                        a.NomeAtividade,
                        a.TbMateriasIdMateria.Select(m => m.IdMateria),
                        a.TbQuestoesIdQuestoes.Select(p => p.IdQuestao)
                    ))
                    .ToListAsync();

                return Results.Ok(atividades);
            });

            // GET atividade by ID with details
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var atividade = await context.TbAtividades
                    .Include(a => a.TbMateriasIdMateria)
                    .Include(a => a.TbQuestoesIdQuestoes)
                    .FirstOrDefaultAsync(a => a.IdAtividade == id);

                if (atividade is null)
                    return Results.NotFound();

                var response = new AtividadeResponse(
                        atividade.IdAtividade,
                        atividade.NomeAtividade,
                        atividade.TbMateriasIdMateria.Select(m => m.IdMateria),
                        atividade.TbQuestoesIdQuestoes.Select(p => p.IdQuestao)
                    );

                return Results.Ok(response);
            });

            // POST new atividade
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] AtividadeCreateRequest request) =>
            {
                var atividade = new TbAtividade
                {
                    NomeAtividade = request.NomeAtividade
                };

                context.TbAtividades.Add(atividade);
                await context.SaveChangesAsync();

                var response = new AtividadeResponse(
                    atividade.IdAtividade,
                    atividade.NomeAtividade,
                    null,
                    null
                );

                return Results.Created($"/atividades/{atividade.IdAtividade}", response);
            });

            // PUT update atividade
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] AtividadeEditRequest request) =>
            {
                if (id != request.IdAtividade)
                    return Results.BadRequest("ID mismatch");

                var atividade = await context.TbAtividades.FindAsync(id);
                if (atividade is null)
                    return Results.NotFound();

                atividade.NomeAtividade = request.NomeAtividade;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE atividade
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                // Include related materias and questions when loading the activity
                var atividade = await context.TbAtividades
                    .Include(a => a.TbMateriasIdMateria)
                    .Include(a => a.TbQuestoesIdQuestoes)
                    .FirstOrDefaultAsync(a => a.IdAtividade == id);

                if (atividade is null) return Results.NotFound();

                try
                {
                    // Remove all materia associations first
                    foreach (var materia in atividade.TbMateriasIdMateria.ToList())
                    {
                        atividade.TbMateriasIdMateria.Remove(materia);
                    }

                    // Remove all question associations
                    foreach (var questao in atividade.TbQuestoesIdQuestoes.ToList())
                    {
                        atividade.TbQuestoesIdQuestoes.Remove(questao);
                    }

                    // Now delete the activity
                    context.TbAtividades.Remove(atividade);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (DbUpdateException ex)
                {
                    // Log the error details
                    Console.WriteLine($"Error deleting activity: {ex.InnerException?.Message}");
                    return Results.Problem("Could not delete activity due to database constraints");
                }
            });

            // Additional endpoints for relationships
            group.MapPost("/{idAtividade}/questoes/{idQuestao}",
                async ([FromServices] DbPocaContext context, int idAtividade, int idQuestao) =>
                {
                    var atividade = await context.TbAtividades
                        .Include(a => a.TbQuestoesIdQuestoes)
                        .FirstOrDefaultAsync(a => a.IdAtividade == idAtividade);

                    if (atividade is null)
                        return Results.NotFound("Atividade not found");

                    var questao = await context.TbQuestoes.FindAsync(idQuestao);
                    if (questao is null)
                        return Results.NotFound("Questao not found");

                    if (!atividade.TbQuestoesIdQuestoes.Any(m => m.IdQuestao == idQuestao))
                    {
                        atividade.TbQuestoesIdQuestoes.Add(questao);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idAtividade}/questoes/{idQuestao}",
                async ([FromServices] DbPocaContext context, int idAtividade, int idQuestao) =>
                {
                    var atividade = await context.TbAtividades
                        .Include(a => a.TbQuestoesIdQuestoes)
                        .FirstOrDefaultAsync(a => a.IdAtividade == idAtividade);

                    if (atividade is null)
                        return Results.NotFound("Aluno not found");

                    var questao = atividade.TbQuestoesIdQuestoes
                        .FirstOrDefault(m => m.IdQuestao == idQuestao);

                    if (questao is null)
                        return Results.NotFound("Materia not associated with aluno");

                    atividade.TbQuestoesIdQuestoes.Remove(questao);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
        }
    }
}