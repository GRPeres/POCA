namespace POCA.API.Responses
{
    public record AlunoResponse(
        int IdAluno,
        string NomeAluno,
        int IdadeAluno,
        int ProgressoAluno,
        string ContatoAluno,
        IEnumerable<int>? MateriasIds,
        IEnumerable<int>? PessoasIds
    );
}