public record ProfessorCreateRequest(
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor
) : ProfessorRequest(
    NomeProfessor,
    MateriaProfessor,
    ContatoProfessor
);