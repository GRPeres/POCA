namespace POCA.Web.Requests.Aluno
{
    public record AlunoCreateRequest(
     string NomeAluno,
     int IdadeAluno,
     int ProgressoAluno,
     string ContatoAluno
 );
}
