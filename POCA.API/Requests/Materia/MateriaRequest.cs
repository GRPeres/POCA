using System.ComponentModel.DataAnnotations;

public record MateriaRequest
{
    [Required]
    public string NomeMateria { get; init; }
}
