namespace POCA.Web.Response
{
    public record RespostaResponse(
        int IdResposta,
        string FinalResposta,
        int IdAtividade,
        int IdAluno,
        int IdQuestao
    );
}
