using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Resposta;
using POCA.API.Response;
using POCA.API.Responses;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class RespostaExtension
    {
        public static void AddEndpointsRespostas(this WebApplication app)
        {
            var group = app.MapGroup("/respostas")
                           .WithTags("Respostas")
                           .WithOpenApi();

            // GET all respostas
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var respostas = await context.TbRespostas
                    .Select(r => new RespostaResponse(
                        r.IdResposta,
                        r.FinalResposta,
                        r.IdAtividade,
                        r.IdAluno,
                        r.IdQuestao
                    ))
                    .ToListAsync();

                return Results.Ok(respostas);
            });

            // GET resposta by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var resposta = await context.TbRespostas
                    .FirstOrDefaultAsync(r => r.IdResposta == id);

                if (resposta is null)
                    return Results.NotFound();

                var response = new RespostaResponse(
                    resposta.IdResposta,
                    resposta.FinalResposta,
                    resposta.IdAtividade,
                    resposta.IdAluno,
                    resposta.IdQuestao
                );

                return Results.Ok(response);
            });

            // POST create resposta
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                     [FromBody] RespostaRequest request) =>
            {
                var resposta = new TbResposta
                {
                    FinalResposta = request.FinalResposta,
                    IdAtividade = request.IdAtividade,
                    IdAluno = request.IdAluno,
                    IdQuestao = request.IdQuestao
                };

                context.TbRespostas.Add(resposta);
                await context.SaveChangesAsync();

                var response = new RespostaResponse(
                    resposta.IdResposta,
                    resposta.FinalResposta,
                    resposta.IdAtividade,
                    resposta.IdAluno,
                    resposta.IdQuestao
                );

                return Results.Created($"/respostas/{resposta.IdResposta}", response);
            });

            // PUT update resposta
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] RespostaEditRequest request) =>
            {
                if (id != request.IdResposta)
                    return Results.BadRequest("ID mismatch");

                var resposta = await context.TbRespostas.FindAsync(id);
                if (resposta is null)
                    return Results.NotFound();

                resposta.FinalResposta = request.FinalResposta;
                resposta.IdAtividade = request.IdAtividade;
                resposta.IdAluno = request.IdAluno;
                resposta.IdQuestao = request.IdQuestao;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE resposta
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var resposta = await context.TbRespostas.FindAsync(id);
                if (resposta is null)
                    return Results.NotFound();

                context.TbRespostas.Remove(resposta);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // GET aluno de uma resposta
            group.MapGet("/{idResposta}/aluno",
                async ([FromServices] DbPocaContext context, int idResposta) =>
                {
                    var resposta = await context.TbRespostas
                        .Include(r => r.Aluno)
                        .FirstOrDefaultAsync(r => r.IdResposta == idResposta);

                    if (resposta is null)
                        return Results.NotFound("Resposta not found");

                    if (resposta.Aluno is null)
                        return Results.NotFound("Aluno not linked to this resposta");

                    var response = new AlunoResponse(
                        resposta.Aluno.IdAluno,
                        resposta.Aluno.NomeAluno,
                        resposta.Aluno.NascimentoAluno,
                        resposta.Aluno.ProgressoAluno,
                        resposta.Aluno.ContatoAluno,
                        resposta.Aluno.EmailAluno,
                        null,
                        null
                    );

                    return Results.Ok(response);
                });

            // GET atividade de uma resposta
            group.MapGet("/{idResposta}/atividade",
                async ([FromServices] DbPocaContext context, int idResposta) =>
                {
                    var resposta = await context.TbRespostas
                        .Include(r => r.Atividade)
                        .FirstOrDefaultAsync(r => r.IdResposta == idResposta);

                    if (resposta is null)
                        return Results.NotFound("Resposta not found");

                    if (resposta.Atividade is null)
                        return Results.NotFound("Atividade not linked to this resposta");

                    var response = new AtividadeResponse(
                        resposta.Atividade.IdAtividade,
                        resposta.Atividade.NomeAtividade,
                        resposta.Atividade.TbQuestoesIdQuestoes?.Select(q => q.IdQuestao),
                        null
                    );

                    return Results.Ok(response);
                });

            // GET questão de uma resposta
            group.MapGet("/{idResposta}/questao",
                async ([FromServices] DbPocaContext context, int idResposta) =>
                {
                    var resposta = await context.TbRespostas
                        .Include(r => r.Questao)
                        .FirstOrDefaultAsync(r => r.IdResposta == idResposta);

                    if (resposta is null)
                        return Results.NotFound("Resposta not found");

                    if (resposta.Questao is null)
                        return Results.NotFound("Questao not linked to this resposta");

                    var response = new QuestaoResponse(
                        resposta.Questao.IdQuestao,
                        resposta.Questao.EnunciadoQuestao,
                        resposta.Questao.RespostacertaQuestao,
                        null,
                        null,
                        null,
                        null,
                        null
                    );

                    return Results.Ok(response);
                });

        }
    }
}
