using System.ComponentModel.DataAnnotations;
using POCA.API.Requests.Materia;

public record MateriaEditRequest(
    [Required]int IdMateria,
    int IdProfessorMateria,
    int IdAlunoMateria
) : MateriaRequest(
    IdProfessorMateria,
    IdAlunoMateria
);