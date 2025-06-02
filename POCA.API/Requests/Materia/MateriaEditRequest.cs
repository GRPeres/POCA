using System.ComponentModel.DataAnnotations;

public record MateriaEditRequest(
    [Required] int IdMateria,
    int IdProfessorMateria,
    int IdAlunoMateria,
    string? NomeMateria
) : MateriaRequest(
    IdProfessorMateria,
    IdAlunoMateria,
    NomeMateria
);