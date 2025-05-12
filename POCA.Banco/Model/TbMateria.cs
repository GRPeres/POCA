using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbMateria
{
    public int IdMateria { get; set; }

    public int? IdProfessorMateria { get; set; }

    public int? IdAlunoMateria { get; set; }

    public virtual TbAluno? IdAlunoMateriaNavigation { get; set; }

    public virtual TbProfessores? IdProfessorMateriaNavigation { get; set; }
}
