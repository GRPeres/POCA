namespace POCA.API.Requests.Pessoa
{
    public record PessoaCreateRequest(
        string LoginPessoa,
        string SenhaPessoa,
        bool IsProfessor,
        int? IdAluno,      // Optional - for student accounts
        int? IdProfessor   // Optional - for professor accounts
    ) : PessoaRequest(LoginPessoa, SenhaPessoa, IsProfessor);
}
