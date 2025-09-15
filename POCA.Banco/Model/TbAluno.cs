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

    public ICollection<TbResposta> TbRespostasIdRespostas { get; set; } = new List<TbResposta>();

    public virtual ICollection<TbMateria> TbMateriasIdMateria { get; set; } = new List<TbMateria>();

    public virtual ICollection<TbPessoa> TbPessoasIdPessoas { get; set; } = new List<TbPessoa>();
}
