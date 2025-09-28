using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Resposta;

public record RespostaRequest(
    [Required] string FinalResposta,
    [Required] int IdAtividade,
    [Required] int IdAluno,
    [Required] int IdQuestao
);