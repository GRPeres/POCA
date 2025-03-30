using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests;
public record QuestaoEditRequest(
    [Required] int IdQuestao,
    string EnunciadoQuestao,
    string RespostaCertaQuestao,
    string RespostaErrada1Questao,
    string RespostaErrada2Questao,
    string RespostaErrada3Questao
) : QuestaoRequest(
    EnunciadoQuestao,
    RespostaCertaQuestao,
    RespostaErrada1Questao,
    RespostaErrada2Questao,
    RespostaErrada3Questao
);