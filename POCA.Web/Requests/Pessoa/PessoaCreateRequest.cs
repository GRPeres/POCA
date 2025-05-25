namespace POCA.Web.Requests.Pessoa
{
    public record PessoaCreateRequest(
       string LoginPessoa,
       string SenhaPessoa,
       bool IsProfessor,
       int? IdAluno = null,      // For student accounts
       int? IdProfessor = null   // For professor accounts
   );
}
