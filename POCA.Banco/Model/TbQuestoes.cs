using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbQuestoes
{
    public int IdQuestao { get; set; }

    public string EnunciadoQuestao { get; set; } = null!;

    public string RespostacertaQuestao { get; set; } = null!;

    public string Respostaerrada1Questao { get; set; } = null!;

    public string Respostaerrada2Questao { get; set; } = null!;

    public string Respostaerrada3Questao { get; set; } = null!;
}
