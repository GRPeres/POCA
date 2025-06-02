
public record MateriaEditRequest(
    int IdMateria,
    int IdProfessorMateria,
    int IdAlunoMateria,
    string? NomeMateria
) : MateriaRequest(

    NomeMateria,
    IdProfessorMateria,
    IdAlunoMateria
);