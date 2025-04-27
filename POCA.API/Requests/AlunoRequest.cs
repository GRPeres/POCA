using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests;

public record AlunoRequest(
    [Required] string NomeAluno,
    [Required] int IdadeAluno,
    [Required] int ProgressoAluno,
    [Required] string ContatoAluno,
    [Required] string LoginAluno,
    [Required] string SenhaAluno
);