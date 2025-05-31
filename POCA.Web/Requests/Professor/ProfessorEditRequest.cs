// POCA.Web.Requests.ProfessorEditRequest
public record ProfessorEditRequest(
    int IdProfessor,
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor,
    string LoginProfessor,
    string SenhaProfessor
) : ProfessorRequest(
    NomeProfessor,
    MateriaProfessor,
    ContatoProfessor,
    LoginProfessor,
    SenhaProfessor
);