using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests;
public record MateriaRequest(
    [Required] int IdProfessorMateria,
    [Required] int IdAlunoMateria
);