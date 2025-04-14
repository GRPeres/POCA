namespace POCA.API.Response;

public record QuestaoResponse(
    int IdQuestao,
    string EnunciadoQuestao,
    string RespostaCertaQuestao,
    string RespostaErrada1Questao,
    string RespostaErrada2Questao,
    string RespostaErrada3Questao,
    string DificuldadeQuestao,
    string TemaQuestao
);