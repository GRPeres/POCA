﻿public record ProfessorEditRequest(
    int IdProfessor,
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor
) : ProfessorRequest(
    NomeProfessor,
    MateriaProfessor,
    ContatoProfessor
);