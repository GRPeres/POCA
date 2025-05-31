namespace POCA.Web.Response.Login
{
    public record PessoaAuthResponse(
        int IdPessoa,
        string LoginPessoa,
        bool IsProfessor,
        int? IdAluno,
        int? IdProfessor,
        string Token
    );
}
