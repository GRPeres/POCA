using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests;
public record ProfessorEditRequest(
    [Required]int IdProfessor,
    string NomeProfessor,
    string MateriaProfessor,
    string ContatoProfessor,
    string LoginProfessor,
    string SenhaProfessor
) : ProfessorRequest(
    NomeProfessor,
    MateriaProfessor,
    ContatoProfessor,
    LoginProfessor,
    SenhaProfessor
);