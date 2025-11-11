using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Materia
{
    public record MateriaRequest
{
    [Required]
    public string NomeMateria { get; init; }
}
}
