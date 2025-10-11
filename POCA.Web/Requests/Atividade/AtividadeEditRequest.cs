using POCA.Banco.Model;
using System.ComponentModel.DataAnnotations;


namespace POCA.API.Requests.Atividade;

public record AtividadeEditRequest(
        int IdAtividade,
        string NomeAtividade,
        int? TbQuestoesIdQuestoes
    );