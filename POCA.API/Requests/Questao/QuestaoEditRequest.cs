// POCA.API.Requests.QuestaoEditRequest
using POCA.API.Requests.NewFolder;
using System.ComponentModel.DataAnnotations;

public record QuestaoEditRequest(
    [Required] int IdQuestao,
    string EnunciadoQuestao,
    string RespostacertaQuestao,  // Changed to match entity
    string Respostaerrada1Questao, // Changed to match entity
    string Respostaerrada2Questao, // Changed to match entity
    string Respostaerrada3Questao, // Changed to match entity
    string DificuldadeQuestao,
    string TemaQuestao
) : QuestaoRequest(
    EnunciadoQuestao,
    RespostacertaQuestao,  // Changed to match entity
    Respostaerrada1Questao, // Changed to match entity
    Respostaerrada2Questao, // Changed to match entity
    Respostaerrada3Questao, // Changed to match entity
    DificuldadeQuestao,
    TemaQuestao
);