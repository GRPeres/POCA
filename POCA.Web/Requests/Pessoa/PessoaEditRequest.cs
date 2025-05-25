namespace POCA.Web.Requests.Pessoa
{
    public record PessoaEditRequest(
        int IdPessoa,
        string LoginPessoa,
        string SenhaPessoa,
        bool IsProfessor,
        int? IdAluno = null,
        int? IdProfessor = null
    );
}
