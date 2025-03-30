// POCA.Web.Requests.QuestaoRequest
using System.ComponentModel.DataAnnotations;

public record QuestaoRequest(
    [Required] string EnunciadoQuestao,
    [Required] string RespostaCertaQuestao,
    [Required] string RespostaErrada1Questao,
    [Required] string RespostaErrada2Questao,
    [Required] string RespostaErrada3Questao
);