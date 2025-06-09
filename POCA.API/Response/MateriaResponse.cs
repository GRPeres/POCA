using System.Collections.Generic;

namespace POCA.API.Responses
{
    public record MateriaResponse(
        int IdMateria,
        string NomeMateria,
        IEnumerable<int>? TbProfessoresIdProfessors,
        IEnumerable<int>? TbAlunosIdAlunos,
        IEnumerable<int>? TbAtividadesIdAtividades
    );
}
