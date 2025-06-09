using POCA.API.Responses;

public record MateriaResponse(
    int IdMateria,
    string? NomeMateria,
    IEnumerable<int>? tbProfessoresIdProfessors,
    IEnumerable<int>? tbAlunosIdAlunos,
    IEnumerable<int>? tbAtividadesIdAtividades
);
