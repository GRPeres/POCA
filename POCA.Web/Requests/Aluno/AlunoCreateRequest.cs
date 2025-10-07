namespace POCA.Web.Requests.Aluno
{
    public record AlunoCreateRequest(
     string NomeAluno,
     int ProgressoAluno,
     string EmailAluno,
     string ContatoAluno,
     DateTime dtNascimento
 );
}
