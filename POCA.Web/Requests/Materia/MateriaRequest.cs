using System.ComponentModel.DataAnnotations;

public record MateriaRequest(
    string? NomeMateria,
    [Required] int IdProfessorMateria,
    [Required] int IdAlunoMateria
);