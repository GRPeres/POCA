using POCA.Web.Pages.Questoes;

namespace POCA.Web.Services.N_Sei_Nomear
{
    public class AtividadeState
    {
        private List<QuestaoResponse> _questoesSelecionadas = new();

        public void SetQuestoes(List<QuestaoResponse> questoes)
        {
            _questoesSelecionadas = questoes;
        }

        public List<QuestaoResponse> GetQuestoes()
        {
            return _questoesSelecionadas;
        }

        public void Clear()
        {
            _questoesSelecionadas.Clear();
        }
    }
}