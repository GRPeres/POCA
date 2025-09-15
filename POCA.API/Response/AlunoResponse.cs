namespace POCA.API.Responses
{
    public record AlunoResponse(
        int IdAluno,
        string NomeAluno,
        string NascimentoAluno,
        string EmailAluno,
        int ProgressoAluno,
        string ContatoAluno,
        IEnumerable<int>? MateriasIds,
        IEnumerable<int>? PessoasIds
    );
}