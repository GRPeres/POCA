using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Aluno;

public record AlunoEditRequest(
        [Required] int IdAluno,
        string NomeAluno,
        DateTime NascimentoAluno,
        string EmailAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno, EmailAluno, ProgressoAluno, ContatoAluno);