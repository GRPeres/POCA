using System.ComponentModel.DataAnnotations;
public record MateriaRequest(
    [Required] int IdProfessorMateria,
    [Required] int IdAlunoMateria,
    string? NomeMateria
);