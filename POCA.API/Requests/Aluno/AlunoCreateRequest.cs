namespace POCA.API.Requests.Aluno
{
    public record AlunoCreateRequest(
        string NomeAluno,
        string NascimentoAluno,
        string EmailAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, NascimentoAluno, ProgressoAluno, ContatoAluno);
}
