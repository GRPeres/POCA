using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Aluno;

public record AlunoEditRequest(
        [Required] int IdAluno,
        string NomeAluno,
        int IdadeAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, IdadeAluno, ProgressoAluno, ContatoAluno);