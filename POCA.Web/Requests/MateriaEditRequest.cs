// POCA.Web.Requests.MateriaEditRequest
public record MateriaEditRequest(
    int IdMateria,
    int IdProfessorMateria,
    int IdAlunoMateria
) : MateriaRequest(
    IdProfessorMateria,
    IdAlunoMateria
);