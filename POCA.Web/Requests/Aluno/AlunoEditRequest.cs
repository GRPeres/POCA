// POCA.Web.Requests.QuestaoEditRequest
public record AlunoEditRequest(
    int IdAluno,
    string NomeAluno,
    DateTime NascimentoAluno,
    int ProgressoAluno,
    string EmailAluno,
    string ContatoAluno
);
