// Frontend QuestaoEditRequest
public record QuestaoEditRequest(
    int IdQuestao,
    string EnunciadoQuestao,
    string RespostacertaQuestao,  // Changed to match backend
    string Respostaerrada1Questao, // Changed to match backend
    string Respostaerrada2Questao, // Changed to match backend
    string Respostaerrada3Questao, // Changed to match backend
    string DificuldadeQuestao,
    string TemaQuestao
);