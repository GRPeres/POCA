using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests;

public record QuestaoRequest(
    [Required] string EnunciadoQuestao,
    [Required] string RespostaCertaQuestao,
    [Required] string RespostaErrada1Questao,
    [Required] string RespostaErrada2Questao,
    [Required] string RespostaErrada3Questao,
    [Required] string DificuldadeQuestao,
    [Required] string TemaQuestao
);