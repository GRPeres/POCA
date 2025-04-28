using Microsoft.EntityFrameworkCore;
using POCA.Banco;
using POCA.Banco.Model;
using POCA.API.Response;
using System.Linq;

namespace POCA.API.Services
{
    public class AtividadeService
    {
        private readonly DbPocaContext _context;

        public AtividadeService(DbPocaContext context)
        {
            _context = context;
        }

        public async Task<List<QuestaoResponse>> GetRandomQuestionsAsync(
             string? tema = null,
             int? facilCount = null,
             int? medioCount = null,
             int? dificilCount = null)
        {
            IQueryable<TbQuestoes> query = _context.TbQuestoes;

            if (!string.IsNullOrEmpty(tema))
            {
                query = query.Where(q => q.TemaQuestao == tema);
            }

            // If no difficulty counts specified, return 10 fully random questions
            if (facilCount == null && medioCount == null && dificilCount == null)
            {
                return await GetFullyRandomQuestions(query, 10); // Default to 10
            }

            // Otherwise, use the sum of provided counts (treating null as 0)
            return await GetDifficultyBalancedQuestions(
                query,
                facilCount ?? 0,
                medioCount ?? 0,
                dificilCount ?? 0);
        }

        private async Task<List<QuestaoResponse>> GetFullyRandomQuestions(IQueryable<TbQuestoes> query, int total)
        {
            var allQuestions = await query.ToListAsync();
            return allQuestions
                .OrderBy(q => Guid.NewGuid())
                .Take(total)
                .Select(ToQuestaoResponse)
                .ToList();
        }

        private async Task<List<QuestaoResponse>> GetDifficultyBalancedQuestions(
    IQueryable<TbQuestoes> query,
    int facilCount,
    int medioCount,
    int dificilCount)
        {
            var result = new List<QuestaoResponse>();
            var random = new Random();

            if (facilCount > 0)
            {
                var facilQuestions = await query
                    .Where(q => q.DificuldadeQuestao == "facil")
                    .ToListAsync();

                result.AddRange(facilQuestions
                    .OrderBy(q => random.Next())
                    .Take(facilCount)
                    .Select(ToQuestaoResponse));
            }

            if (medioCount > 0)
            {
                var medioQuestions = await query
                    .Where(q => q.DificuldadeQuestao == "medio")
                    .ToListAsync();

                result.AddRange(medioQuestions
                    .OrderBy(q => random.Next())
                    .Take(medioCount)
                    .Select(ToQuestaoResponse));
            }

            if (dificilCount > 0)
            {
                var dificilQuestions = await query
                    .Where(q => q.DificuldadeQuestao == "dificil")
                    .ToListAsync();

                result.AddRange(dificilQuestions
                    .OrderBy(q => random.Next())
                    .Take(dificilCount)
                    .Select(ToQuestaoResponse));
            }

            // Shuffle the final result
            return result.OrderBy(q => random.Next()).ToList();
        }

        private QuestaoResponse ToQuestaoResponse(TbQuestoes q) => new(
            q.IdQuestao,
            q.EnunciadoQuestao,
            q.RespostacertaQuestao,
            q.Respostaerrada1Questao,
            q.Respostaerrada2Questao,
            q.Respostaerrada3Questao,
            q.DificuldadeQuestao,
            q.TemaQuestao);
    }
}