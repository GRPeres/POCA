using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCA.Banco.Migrations;

namespace POCA.Banco.Model
{
    public class TbResposta
    {
        public int IdResposta { get; set; }

        public string FinalResposta { get; set; } = string.Empty;

        public int IdAtividade { get; set; }
        public TbAtividade Atividade { get; set; }

        public int IdAluno { get; set; }
        public TbAluno Aluno { get; set; }

        public int IdQuestao { get; set; }
        public TbQuesto Questao { get; set; }

        //public int TbAlunosIdAluno { get; set; }

        //public int TbQuestoesIdQuestoes { get; set; }

        //public virtual ICollection<TbAluno> TbAlunosIdAluno { get; set; } = new List<TbAluno>();
    }

}
