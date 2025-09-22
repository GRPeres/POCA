using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Aluno;

public record AlunoRequest(
    [Required] string NomeAluno,
    [Required] DateTime NascimentoAluno,
    [Required] string EmailAluno,
    [Required] int ProgressoAluno,
    [Required] string ContatoAluno
);