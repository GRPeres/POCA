namespace POCA.API.Requests.Professor
{
    public record ProfessorEditRequest(
        int IdProfessor,
        string NomeProfessor,
        string MateriaProfessor,
        string ContatoProfessor
    );
}