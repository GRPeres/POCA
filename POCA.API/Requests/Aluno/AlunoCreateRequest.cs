namespace POCA.API.Requests.Aluno
{
    public record AlunoCreateRequest(
        string NomeAluno,
        int IdadeAluno,
        int ProgressoAluno,
        string ContatoAluno
    ) : AlunoRequest(NomeAluno, IdadeAluno, ProgressoAluno, ContatoAluno);
}
