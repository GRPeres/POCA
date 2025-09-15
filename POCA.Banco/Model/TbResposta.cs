using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCA.Banco.Model
{
    public class TbResposta
    {
        public int IdResposta { get; set; }

        public int IdAluno { get; set; }
        public TbAluno Aluno { get; set; }

        public int IdAtividade { get; set; }
        public TbAtividade Atividade { get; set; }

        public string RespostaAluno { get; set; } = string.Empty;
        public bool Correta { get; set; }   // opcional: true/false
        public DateTime DataResposta { get; set; } = DateTime.Now;
    }

}
