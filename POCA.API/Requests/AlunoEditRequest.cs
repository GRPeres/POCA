using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests;

public record AlunoEditRequest(
    [Required] int IdAluno,
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