// Add this to your models
public record PessoaResponse(
    int IdPessoa,
    string LoginPessoa,
    string SenhaPessoa,
    bool IsProfessor,
    int? IdAluno,          // Will be null for professors
    int? IdProfessor       // Will be null for students
);