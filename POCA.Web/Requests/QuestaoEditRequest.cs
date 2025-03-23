// POCA.Web.Requests.QuestionEditRequest
public record QuestaoEditRequest(
    int Id,
    string Enunciado,
    int AtividadeId,
    ICollection<AlternativaRequest>? OpcoesResposta = null
) : QuestaoRequest(Enunciado, AtividadeId, OpcoesResposta);