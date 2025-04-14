using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests;
using POCA.API.Response;
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
                var questao = new TbQuestoes
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
        }
    }
}