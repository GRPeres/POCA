using POCA.Banco.Model;
using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Atividade;

public record AtividadeEditRequest(
        [Required] int IdAtividade,
        [Required] string NomeAtividade,
        int? TbQuestoesIdQuestoes
    ) : AtividadeRequest(IdAtividade, NomeAtividade);