// POCA.Web.Requests.QuestaoEditRequest
public record QuestaoEditRequest(
    int IdQuestao,
    string EnunciadoQuestao,
    string RespostaCertaQuestao,
    string RespostaErrada1Questao,
    string RespostaErrada2Questao,
    string RespostaErrada3Questao,
    string DificuldadeQuestao,
    string TemaQuestao
) : QuestaoRequest(
    EnunciadoQuestao,
    RespostaCertaQuestao,
    RespostaErrada1Questao,
    RespostaErrada2Questao,
    RespostaErrada3Questao,
    DificuldadeQuestao,
    TemaQuestao
);