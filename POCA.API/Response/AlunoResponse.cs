namespace POCA.API.Responses
{
    public record AlunoResponse(
        int IdAluno,
        string NomeAluno,
        string NascimentoAluno,
        int ProgressoAluno,
        string ContatoAluno,
        string EmailAluno,
        IEnumerable<int>? MateriasIds,
        IEnumerable<int>? PessoasIds
    );
}