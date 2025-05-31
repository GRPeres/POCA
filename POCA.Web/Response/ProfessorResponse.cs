public record ProfessorResponse(
    int IdProfessor,
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor,
    IEnumerable<int>? IdsMaterias = null,
    IEnumerable<int>? IdsPessoas = null
);