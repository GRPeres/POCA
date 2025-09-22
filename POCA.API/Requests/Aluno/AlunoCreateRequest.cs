namespace POCA.API.Requests.Aluno
{
    public record AlunoCreateRequest(
        string NomeAluno,
        DateTime NascimentoAluno,
        string EmailAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno,EmailAluno, ProgressoAluno, ContatoAluno);
}
