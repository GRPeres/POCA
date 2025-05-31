using POCA.Banco.Model;
using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Atividade
{
    public record AtividadeCreateRequest(
        int IdAtividade,
        string NomeAtividade,
        int? TbQuestoesIdQuestaos
    );
}