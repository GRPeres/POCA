namespace POCA.API.Response
{
    public class RespostaResponse
    {
        public int IdResposta { get; set; }
        public string FinalResposta { get; set; } = "";

        public int IdAtividade { get; set; }
        public int IdAluno { get; set; }
        public int IdQuestao { get; set; }
    }
}
