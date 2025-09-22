namespace POCA.API.Responses
{
    public record AlunoResponse(
        int IdAluno,
        string NomeAluno,
        DateTime NascimentoAluno,
        int ProgressoAluno,
        string ContatoAluno,
        string EmailAluno,
        IEnumerable<int>? MateriasIds,
        IEnumerable<int>? PessoasIds
    );
}