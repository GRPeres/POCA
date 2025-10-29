using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Resposta
{
    public record RespostaEditRequest(
     [Required]int IdResposta,
     string FinalResposta,
     int IdAtividade,
     int IdAluno,
     int IdQuestao
     ):RespostaRequest(FinalResposta, IdAtividade, IdAluno, IdQuestao);

}
