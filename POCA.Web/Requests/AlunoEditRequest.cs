// POCA.Web.Requests.QuestaoEditRequest
public record AlunoEditRequest(
    int IdAluno,
    string NomeAluno,
    int IdadeAluno,
    int ProgressoAluno,
    string ContatoAluno,
    string LoginAluno,
    string SenhaAluno
) : AlunoRequest(
    NomeAluno,
    IdadeAluno,
    ProgressoAluno,
    ContatoAluno,
    LoginAluno,
    SenhaAluno
);