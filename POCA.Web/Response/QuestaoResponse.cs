﻿// ScreenSound.Web.Response.QuestaoResponse
namespace POCA.Web.Response
{
    public record QuestaoResponse(
        int Id,
        string Enunciado,
        int AtividadeId,
        string NomeAtividade, // Activity name (like NomeArtista)
        ICollection<AlternativaResponse> OpcoesResposta
    );

    // Answer options in response
    public record AlternativaResponse(
        int Id,
        string Texto,
        bool EhCorreta
    );
}