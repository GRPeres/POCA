using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Aluno;

public record AlunoRequest(
    [Required] string NomeAluno,
    [Required] int IdadeAluno,
    [Required] int ProgressoAluno,
    [Required] string ContatoAluno
);