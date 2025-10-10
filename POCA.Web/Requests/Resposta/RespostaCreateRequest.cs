namespace POCA.Web.Requests.Resposta
{
    public record RespostaCreateRequest(
        int IdResposta,
        string FinalResposta,
        int IdAtividade,
        int IdAluno,
        int IdQuestao
    );
}
