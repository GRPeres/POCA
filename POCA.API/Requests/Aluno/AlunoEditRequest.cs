using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Aluno;

public record AlunoEditRequest(
        [Required] int IdAluno,
        string NomeAluno,
        string NascimentoAluno,
        string EmailAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno, ProgressoAluno, ContatoAluno);