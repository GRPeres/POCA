namespace POCA.API.Responses
{
    public record ProfessorResponse(
        int IdProfessor,
        string NomeProfessor,
        string MateriaProfessor,
        string ContatoProfessor,
        IEnumerable<int>? MateriasIds,
        IEnumerable<int>? PessoasIds
    );
}