namespace POCA.Web.Requests.Resposta
{
    public record RespostaEditRequest(
        int IdResposta,
        string FinalResposta,
        int IdAtividade,
        int IdAluno,
        int IdQuestao
    );
}
