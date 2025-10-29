using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Aluno;

public record AlunoRequest(
    [Required] string NomeAluno,
    [Required] DateTime NascimentoAluno,
    [Required] int ProgressoAluno,
    [Required] string ContatoAluno,
    [Required] string EmailAluno
);