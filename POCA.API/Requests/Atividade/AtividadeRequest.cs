using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Atividade;

public record AtividadeRequest(
    [Required] int IdAtividade,
    [Required] string NomeAtividade
);