using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Aluno;

public record AlunoEditRequest(
        [Required] int IdAluno,
        string NomeAluno,
        DateTime NascimentoAluno,
        int ProgressoAluno,
        string ContatoAluno,
        string EmailAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno, ProgressoAluno, ContatoAluno, EmailAluno);