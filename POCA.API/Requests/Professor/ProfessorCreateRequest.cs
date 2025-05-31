namespace POCA.API.Requests.Professor
{
    public record ProfessorCreateRequest(
        string NomeProfessor,
        string MateriaProfessor,
        string ContatoProfessor
    );
}