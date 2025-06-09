using POCA.Web.Pages.Professor.Questoes;

namespace POCA.Web.Services
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