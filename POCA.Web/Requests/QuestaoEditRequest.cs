// POCA.Web.Requests.QuestaoEditRequest
public record QuestaoEditRequest(
    int IdQuestao,  // Changed from 'Id' to match your DB model
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