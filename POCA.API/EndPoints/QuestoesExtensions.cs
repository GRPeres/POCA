using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;
using POCA.API.Response;
using POCA.API.Services;
using POCA.Banco;
using POCA.Banco.Model;

namespace POCA.API.Endpoints
{
    public static class QuestoesExtensions
    {
        public static void AddEndpointsQuestoes(this WebApplication app)
        {
            var group = app.MapGroup("/questoes")
                           .WithTags("Questões");

            // GET all questions
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbQuestoes.ToListAsync());
            });

            // GET question by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var questao = await context.TbQuestoes.FindAsync(id);
                return questao is null ? Results.NotFound() : Results.Ok(questao);
            });

            // POST new question
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] QuestaoRequest request) =>
            {
                var questao = new TbQuesto
                {
                    EnunciadoQuestao = request.EnunciadoQuestao,
                    RespostacertaQuestao = request.RespostaCertaQuestao,
                    Respostaerrada1Questao = request.RespostaErrada1Questao,
                    Respostaerrada2Questao = request.RespostaErrada2Questao,
                    Respostaerrada3Questao = request.RespostaErrada3Questao,
                    DificuldadeQuestao = request.DificuldadeQuestao,
                    TemaQuestao = request.TemaQuestao
                };

                context.TbQuestoes.Add(questao);
                await context.SaveChangesAsync();
                return Results.Created($"/questoes/{questao.IdQuestao}", questao);
            });

            // PUT update question
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] QuestaoRequest request) =>
            {
                var questao = await context.TbQuestoes.FindAsync(id);
                if (questao is null) return Results.NotFound();

                questao.EnunciadoQuestao = request.EnunciadoQuestao;
                questao.RespostacertaQuestao = request.RespostaCertaQuestao;
                questao.Respostaerrada1Questao = request.RespostaErrada1Questao;
                questao.Respostaerrada2Questao = request.RespostaErrada2Questao;
                questao.Respostaerrada3Questao = request.RespostaErrada3Questao;
                questao.DificuldadeQuestao = request.DificuldadeQuestao;
                questao.TemaQuestao = request.TemaQuestao;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE question
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var questao = await context.TbQuestoes.FindAsync(id);
                if (questao is null) return Results.NotFound();

                context.TbQuestoes.Remove(questao);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            //Cria Atividade de forma automatica
            group.MapGet("/random", async (
                [FromServices] AtividadeService service,
                [FromQuery] string? tema,
                [FromQuery] int? facilCount,
                [FromQuery] int? medioCount,
                [FromQuery] int? dificilCount) =>
            {
                var questions = await service.GetRandomQuestionsAsync(
                    tema,
                    facilCount,
                    medioCount,
                    dificilCount);

                return Results.Ok(questions);
            });

            // Add this method to your QuestoesExtensions class
            group.MapPost("/import", async (
                [FromServices] DbPocaContext context,
                [FromBody] QuestaoBatchImportRequest batchRequest) =>
            {
                // Validate the request
                if (batchRequest?.Questoes == null || !batchRequest.Questoes.Any())
                {
                    return Results.BadRequest("Nenhuma questão fornecida para importação.");
                }

                var importedQuestions = new List<TbQuesto>();
                var errors = new List<string>();
                var rowNumber = 1;

                foreach (var questao in batchRequest.Questoes)
                {
                    rowNumber++;

                    // Basic validation
                    if (string.IsNullOrWhiteSpace(questao.EnunciadoQuestao))
                    {
                        errors.Add($"Linha {rowNumber}: Enunciado não pode ser vazio");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(questao.RespostaCertaQuestao))
                    {
                        errors.Add($"Linha {rowNumber}: Resposta correta não pode ser vazia");
                        continue;
                    }

                    // Create the entity
                    var newQuestao = new TbQuesto
                    {
                        EnunciadoQuestao = questao.EnunciadoQuestao,
                        RespostacertaQuestao = questao.RespostaCertaQuestao,
                        Respostaerrada1Questao = questao.RespostaErrada1Questao,
                        Respostaerrada2Questao = questao.RespostaErrada2Questao,
                        Respostaerrada3Questao = questao.RespostaErrada3Questao,
                        DificuldadeQuestao = questao.DificuldadeQuestao,
                        TemaQuestao = questao.TemaQuestao
                    };

                    context.TbQuestoes.Add(newQuestao);
                    importedQuestions.Add(newQuestao);
                }

                try
                {
                    await context.SaveChangesAsync();

                    var result = new
                    {
                        Success = true,
                        ImportedCount = importedQuestions.Count,
                        ErrorCount = errors.Count,
                        Errors = errors
                    };

                    if (errors.Any())
                    {
                        return Results.Ok(result); // Partial success
                    }

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    // Rollback any changes if there was an error
                    foreach (var question in importedQuestions)
                    {
                        context.Entry(question).State = EntityState.Detached;
                    }

                    return Results.Problem($"Erro ao importar questões: {ex.Message}");
                }
            }).WithName("ImportQuestoesBatch")
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}