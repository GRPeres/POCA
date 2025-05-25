// POCA.Web.Services.QuestoesAPI
using System.Net.Http.Json;
using static POCA.Web.Pages.Questoes.ImportarQuestoes;

namespace POCA.Web.Services.APIs
{
    public class QuestoesAPI
    {
        private readonly HttpClient _httpClient;

        public QuestoesAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Use your named client
        }

        public async Task<ICollection<QuestaoResponse>?> GetRandomQuestionsAsync(
            string? tema = null,
            int? facilCount = null,
            int? medioCount = null,
            int? dificilCount = null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(tema))
                queryParams.Add($"tema={Uri.EscapeDataString(tema)}");

            if (facilCount.HasValue)
                queryParams.Add($"facilCount={facilCount.Value}");

            if (medioCount.HasValue)
                queryParams.Add($"medioCount={medioCount.Value}");

            if (dificilCount.HasValue)
                queryParams.Add($"dificilCount={dificilCount.Value}");

            var queryString = queryParams.Any() ? $"?{string.Join("&", queryParams)}" : "";

            return await _httpClient.GetFromJsonAsync<ICollection<QuestaoResponse>>($"questoes/random{queryString}");
        }

        public async Task<ICollection<QuestaoResponse>?> GetQuestoesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<QuestaoResponse>>("questoes");
        }

        public async Task<QuestaoResponse?> GetQuestaoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<QuestaoResponse>($"questoes/{id}");
        }

        public async Task AddQuestaoAsync(QuestaoRequest questao)
        {
            await _httpClient.PostAsJsonAsync("questoes", questao);
        }

        public async Task<HttpResponseMessage> ImportQuestoesBatchAsync(QuestaoBatchImportRequest batchRequest)
        {
            return await _httpClient.PostAsJsonAsync("questoes/import", batchRequest);
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