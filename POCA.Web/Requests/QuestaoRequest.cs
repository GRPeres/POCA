// POCA.Web.Requests.QuestaoRequest
using System.ComponentModel.DataAnnotations;

public record QuestaoRequest(
    [Required] string Enunciado, // The question text
    [Required] int AtividadeId, // Activity it belongs to (like ArtistaId)
    ICollection<AlternativaRequest>? OpcoesResposta = null // Answer options
);

// For answer choices (sub-request)
public record AlternativaRequest(
    [Required] string Texto,
    [Required] bool EhCorreta
);