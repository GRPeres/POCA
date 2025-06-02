using POCA.API.Responses;

public record MateriaResponse(
    int IdMateria,
    string? NomeMateria,
    IEnumerable<ProfessorResponse>? Professores,
    IEnumerable<AlunoResponse>? Alunos,
    IEnumerable<AtividadeResponse>? Atividades
);