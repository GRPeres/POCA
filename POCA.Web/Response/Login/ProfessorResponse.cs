// POCA.Web.Responses.ProfessorResponse
public record ProfessorResponse(
    int IdProfessor,
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor,
    IEnumerable<int>? MateriasIds = null,
    IEnumerable<int>? PessoasIds = null
);