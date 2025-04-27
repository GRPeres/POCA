using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbAluno
{
    public int IdAluno { get; set; }

    public string NomeAluno { get; set; } = null!;

    public int IdadeAluno { get; set; }

    public int ProgressoAluno { get; set; }

    public string ContatoAluno { get; set; } = null!;

    public string LoginAluno { get; set; } = null!;

    public string SenhaAluno { get; set; } = null!;
}
