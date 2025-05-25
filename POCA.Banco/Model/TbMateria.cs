using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbMateria
{
    public int IdMateria { get; set; }

    public string? NomeMateria { get; set; }

    public virtual ICollection<TbAluno> TbAlunosIdAlunos { get; set; } = new List<TbAluno>();

    public virtual ICollection<TbAtividade> TbAtividadesIdAtividades { get; set; } = new List<TbAtividade>();

    public virtual ICollection<TbProfessore> TbProfessoresIdProfessors { get; set; } = new List<TbProfessore>();
}
