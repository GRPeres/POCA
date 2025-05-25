namespace POCA.API.Response
{
    public record PessoaResponse(
        int IdPessoa,
        string LoginPessoa,
        bool IsProfessor,
        int? IdAluno,
        int? IdProfessor,
        string? AlunoNome,      // Optional - populated when needed
        string? ProfessorNome   // Optional - populated when needed
    );

    public record PessoaAuthResponse(
        int IdPessoa,
        string LoginPessoa,
        bool IsProfessor,
        int? IdAluno,
        int? IdProfessor,
        string Token
    );
}
