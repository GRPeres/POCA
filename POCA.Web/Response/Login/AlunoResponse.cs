// POCA.Web.Responses.AlunoResponse
public record AlunoResponse(
    int IdAluno,
    string NomeAluno,
    int IdadeAluno,
    int ProgressoAluno,
    string ContatoAluno,
    IEnumerable<int>? MateriasIds,
    IEnumerable<int>? PessoasIds
);