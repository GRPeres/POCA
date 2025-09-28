using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using POCA.API.Requests.NewFolder;
using POCA.API.Response;
using POCA.API.Services;
using POCA.Banco;
using POCA.Banco.Model;
using System.Text.Json;

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
                // Include related activities when loading the question
                var questao = await context.TbQuestoes
                    .Include(q => q.TbAtividadesIdAtividades)
                    .FirstOrDefaultAsync(q => q.IdQuestao == id);

                if (questao is null) return Results.NotFound();

                try
                {
                    // Remove all activity associations first
                    foreach (var atividade in questao.TbAtividadesIdAtividades.ToList())
                    {
                        questao.TbAtividadesIdAtividades.Remove(atividade);
                    }

                    // Now delete the question
                    context.TbQuestoes.Remove(questao);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (DbUpdateException ex)
                {
                    // Log the error details
                    Console.WriteLine($"Error deleting question: {ex.InnerException?.Message}");
                    return Results.Problem("Could not delete question due to database constraints");
                }
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

            // ASSOCIAR questão a uma atividade
            group.MapPost("/{idQuestao}/atividades/{idAtividade}",
                async ([FromServices] DbPocaContext context, int idQuestao, int idAtividade) =>
                {
                    var questao = await context.TbQuestoes
                        .Include(q => q.TbAtividadesIdAtividades)
                        .FirstOrDefaultAsync(q => q.IdQuestao == idQuestao);

                    if (questao is null)
                        return Results.NotFound("Questão não encontrada");

                    var atividade = await context.TbAtividades.FindAsync(idAtividade);
                    if (atividade is null)
                        return Results.NotFound("Atividade não encontrada");

                    if (!questao.TbAtividadesIdAtividades.Any(a => a.IdAtividade == idAtividade))
                    {
                        questao.TbAtividadesIdAtividades.Add(atividade);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            // DESASSOCIAR questão de uma atividade
            group.MapDelete("/{idQuestao}/atividades/{idAtividade}",
                async ([FromServices] DbPocaContext context, int idQuestao, int idAtividade) =>
                {
                    var questao = await context.TbQuestoes
                        .Include(q => q.TbAtividadesIdAtividades)
                        .FirstOrDefaultAsync(q => q.IdQuestao == idQuestao);

                    if (questao is null)
                        return Results.NotFound("Questão não encontrada");

                    var atividade = questao.TbAtividadesIdAtividades
                        .FirstOrDefault(a => a.IdAtividade == idAtividade);

                    if (atividade is null)
                        return Results.NotFound("Atividade não associada à questão");

                    questao.TbAtividadesIdAtividades.Remove(atividade);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });

            group.MapPost("/ai-generate", async (
                [FromServices] DbPocaContext context,
                [FromServices] GENAIService aiService,
                [FromBody] PromptWithMetadataRequest request) =>
            {
                if (string.IsNullOrWhiteSpace(request.Prompt))
                    return Results.BadRequest("Prompt is required.");

                var systemInstruction =
                    "You are a JSON API that outputs only an array of multiple-choice questions. " +
                    "Each question must be an object with these fields: " +
                    "EnunciadoQuestao (string), RespostaCertaQuestao (string), RespostaErrada1Questao (string), " +
                    "RespostaErrada2Questao (string), RespostaErrada3Questao (string), " +
                    "DificuldadeQuestao (string), TemaQuestao (string). " +
                    "The field DificuldadeQuestao must be one of exactly these values: 'Fácil', 'Médio', or 'Difícil'. " +
                    "The field TemaQuestao must be one of exactly these values: 'Teoria' or 'Programação'. " +
                    "Do not include any text, explanation, or markdown—output only JSON starting with '[' and ending with ']'.";

                List<QuestaoRequest> generatedQuestions;
                try
                {
                    generatedQuestions = await aiService.GenerateQuestionsAsync(request.Prompt, systemInstruction);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

                foreach (var q in generatedQuestions)
                {
                    context.TbQuestoes.Add(new TbQuesto
                    {
                        EnunciadoQuestao = q.EnunciadoQuestao,
                        RespostacertaQuestao = q.RespostaCertaQuestao,
                        Respostaerrada1Questao = q.RespostaErrada1Questao,
                        Respostaerrada2Questao = q.RespostaErrada2Questao,
                        Respostaerrada3Questao = q.RespostaErrada3Questao,
                        DificuldadeQuestao = string.IsNullOrWhiteSpace(q.DificuldadeQuestao) ? "NOT PROVIDED" : q.DificuldadeQuestao,
                        TemaQuestao = string.IsNullOrWhiteSpace(q.TemaQuestao) ? "NOT PROVIDED" : q.TemaQuestao
                    });
                }

                await context.SaveChangesAsync();
                return Results.Ok(generatedQuestions);
            });

            group.MapPost("/ai-generate-based-on-existing", async (
                    [FromServices] DbPocaContext context,
                    [FromServices] GENAIService aiService) =>
                {
                var existingQuestions = context.TbQuestoes.Take(10).ToList(); // e.g., first 10 for input

                var prompt = System.Text.Json.JsonSerializer.Serialize(existingQuestions.Select(q => new {
                    q.EnunciadoQuestao,
                    q.RespostacertaQuestao,
                    q.Respostaerrada1Questao,
                    q.Respostaerrada2Questao,
                    q.Respostaerrada3Questao,
                    q.DificuldadeQuestao,
                    q.TemaQuestao
                }));

                var systemInstruction =
                    "You are a JSON API that creates new multiple-choice questions inspired by provided examples. " +
                    "Use the style and difficulty of the given input, but do not repeat them. " +
                    "Output only an array of JSON objects with fields: " +
                    "EnunciadoQuestao, RespostaCertaQuestao, RespostaErrada1Questao, RespostaErrada2Questao, RespostaErrada3Questao, " +
                    "DificuldadeQuestao (one of 'Fácil', 'Médio', 'Difícil'), TemaQuestao (one of 'Teoria', 'Programação'). " +
                    "Do not output any extra text, only JSON.";

                List<QuestaoRequest> generatedQuestions;
                try
                {
                    generatedQuestions = await aiService.GenerateQuestionsAsync(prompt, systemInstruction);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

                foreach (var q in generatedQuestions)
                {
                    context.TbQuestoes.Add(new TbQuesto
                    {
                        EnunciadoQuestao = q.EnunciadoQuestao,
                        RespostacertaQuestao = q.RespostaCertaQuestao,
                        Respostaerrada1Questao = q.RespostaErrada1Questao,
                        Respostaerrada2Questao = q.RespostaErrada2Questao,
                        Respostaerrada3Questao = q.RespostaErrada3Questao,
                        DificuldadeQuestao = string.IsNullOrWhiteSpace(q.DificuldadeQuestao) ? "NOT PROVIDED" : q.DificuldadeQuestao,
                        TemaQuestao = string.IsNullOrWhiteSpace(q.TemaQuestao) ? "NOT PROVIDED" : q.TemaQuestao
                    });
                }

                await context.SaveChangesAsync();
                return Results.Ok(generatedQuestions);
            });
        }
    }
}