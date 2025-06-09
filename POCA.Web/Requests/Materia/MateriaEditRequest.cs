public record MateriaEditRequest(
    int IdMateria,
    string? NomeMateria,
    List<int>? IdProfessoresMateria = null,
    List<int>? IdAlunosMateria = null
);
