using System.ComponentModel.DataAnnotations;

public record MateriaEditRequest : MateriaRequest
{
    [Required]
    public int IdMateria { get; init; }

    public List<int>? IdsProfessores { get; init; } = new();
    public List<int>? IdsAlunos { get; init; } = new();
    public List<int>? IdsAtividades { get; init; } = new();

}
