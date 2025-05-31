using POCA.Banco.Model;

namespace POCA.API.Responses
{
    public record AtividadeResponse(
        int IdAtividade,
        string NomeAtividade,
        IEnumerable<int>? TbMateriasIdMateria,
        IEnumerable<int>? TbQuestoesIdQuestaos
    );
}