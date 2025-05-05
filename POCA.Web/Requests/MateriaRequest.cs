// POCA.Web.Requests.MateriaRequest
using System.ComponentModel.DataAnnotations;

public record MateriaRequest(
    [Required] int IdProfessorMateria,
    [Required] int IdAlunoMateria
);