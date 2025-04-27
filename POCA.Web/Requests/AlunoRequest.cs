// POCA.Web.Requests.AlunoRequest
using System.ComponentModel.DataAnnotations;
public record AlunoRequest(
    [Required] string NomeAluno,
    [Required] int IdadeAluno,
    [Required] int ProgressoAluno,
    [Required] string ContatoAluno,
    [Required] string LoginAluno,
    [Required] string SenhaAluno
);