using System.ComponentModel.DataAnnotations;

public record ProfessorRequest(
    [Required] string NomeProfessor,
    [Required] string MateriaProfessor,
    [Required] string ContatoProfessor
);