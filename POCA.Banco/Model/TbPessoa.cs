using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbPessoa
{
    public int IdPessoa { get; set; }

    public string LoginPessoa { get; set; } = null!;

    public string SenhaPessoa { get; set; } = null!;

    public sbyte BoolProfessorPessoa { get; set; }

    public virtual ICollection<TbAluno> TbAlunosIdAlunos { get; set; } = new List<TbAluno>();

    public virtual ICollection<TbProfessore> TbProfessoresIdProfessors { get; set; } = new List<TbProfessore>();
}
