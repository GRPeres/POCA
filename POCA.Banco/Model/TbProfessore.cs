using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbProfessore
{
    public int IdProfessor { get; set; }

    public string NomeProfessor { get; set; } = null!;

    public string MateriaProfessor { get; set; } = null!;

    public string ContatoProfessor { get; set; } = null!;

    public string LoginProfessor { get; set; } = null!;

    public string SenhaProfessor { get; set; } = null!;

    public virtual ICollection<TbMateria> TbMateria { get; set; } = new List<TbMateria>();
}
