// POCA.Web.Responses.AlunoResponse
public record AlunoResponse(
    int IdAluno,
    string NomeAluno,
    DateTime NascimentoAluno,
    int ProgressoAluno,
    string EmailAluno,
    string ContatoAluno,
    IEnumerable<int>? MateriasIds,
    IEnumerable<int>? PessoasIds
);