public record QuestaoBatchImportRequest(List<QuestaoImportModel> Questoes);

public record QuestaoImportModel
{
    public string EnunciadoQuestao { get; set; }
    public string RespostaCertaQuestao { get; set; }
    public string RespostaErrada1Questao { get; set; }
    public string RespostaErrada2Questao { get; set; }
    public string RespostaErrada3Questao { get; set; }
    public string DificuldadeQuestao { get; set; }
    public string TemaQuestao { get; set; }
}