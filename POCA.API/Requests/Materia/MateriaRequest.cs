using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Materia;
public record MateriaRequest(
    [Required] int IdProfessorMateria,
    [Required] int IdAlunoMateria
);