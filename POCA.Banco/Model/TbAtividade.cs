﻿using System;
using System.Collections.Generic;

namespace POCA.Banco.Model;

public partial class TbAtividade
{
    public int IdAtividade { get; set; }

    public string NomeAtividade { get; set; } = null!;

    public virtual ICollection<TbMateria> TbMateriasIdMateria { get; set; } = new List<TbMateria>();

    public virtual ICollection<TbQuesto> TbQuestoesIdQuestaos { get; set; } = new List<TbQuesto>();
}
