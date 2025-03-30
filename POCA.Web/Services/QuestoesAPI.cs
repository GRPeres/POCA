// POCA.Web.Services.QuestoesAPI
using System.Net.Http.Json;

namespace POCA.Web.Services
{
    public class QuestoesAPI
    {
        private readonly HttpClient _httpClient;

        public QuestoesAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Use your named client
        }

        // All methods remain the same as before
        public async Task<ICollection<QuestaoResponse>?> GetQuestoesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<QuestaoResponse>>("questoes");
        }

        public async Task AddQuestaoAsync(QuestaoRequest questao)
        {
            await _httpClient.PostAsJsonAsync("questoes", questao);
        }

        public async Task UpdateQuestaoAsync(QuestaoEditRequest questao)
        {
            await _httpClient.PutAsJsonAsync($"questoes/{questao.IdQuestao}", questao);
        }

        public async Task DeleteQuestaoAsync(int idQuestao)
        {
            await _httpClient.DeleteAsync($"questoes/{idQuestao}");
        }
    }
}