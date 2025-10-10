namespace POCA.Web.Requests.Aluno
{
    public record AlunoCreateRequest(
    string NomeAluno,
    DateTime NascimentoAluno,
    int ProgressoAluno,
    string EmailAluno,
    string ContatoAluno
 );
}
