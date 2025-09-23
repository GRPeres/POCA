namespace POCA.API.Requests.Aluno
{
    public record AlunoCreateRequest(
        string NomeAluno,
        DateTime NascimentoAluno,
        int ProgressoAluno,
        string ContatoAluno,
        string EmailAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno, ProgressoAluno, ContatoAluno, EmailAluno);
}
