using System.Net.Http.Json;
using POCA.API.Requests.Atividade;
using POCA.API.Responses;
using POCA.Web.Requests.Atividade;

namespace POCA.Web.Services.APIs
{
    public class AtividadesAPI
    {
        private readonly HttpClient _httpClient;

        public AtividadesAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        // Get all atividades
        public async Task<ICollection<AtividadeResponse>?> GetAtividadesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<AtividadeResponse>>("atividade");
        }

        // Get single atividade by ID
        public async Task<AtividadeResponse?> GetAtividadeByIdAsync(int idAtividade)
        {
            return await _httpClient.GetFromJsonAsync<AtividadeResponse>($"atividade/{idAtividade}");
        }

        // Create new atividade
        public async Task<AtividadeResponse?> AddAtividadeAsync(AtividadeCreateRequest atividade)
        {
            var response = await _httpClient.PostAsJsonAsync("atividade", atividade);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AtividadeResponse>();
            }

            // Handle error cases here if needed
            return null;
        }

        // Update existing atividade
        public async Task<bool> UpdateAtividadeAsync(AtividadeEditRequest atividade)
        {
            var response = await _httpClient.PutAsJsonAsync($"atividade/{atividade.IdAtividade}", atividade);
            return response.IsSuccessStatusCode;
        }

        // Delete atividade
        public async Task<bool> DeleteAtividadeAsync(int idAtividade)
        {
            var response = await _httpClient.DeleteAsync($"atividade/{idAtividade}");
            return response.IsSuccessStatusCode;
        }

        // Add questao to atividade
        public async Task<bool> AddQuestaoToAtividadeAsync(int idAtividade, int idQuestao)
        {
            var response = await _httpClient.PostAsync($"atividade/{idAtividade}/questoes/{idQuestao}", null);
            return response.IsSuccessStatusCode;
        }

        // Remove questao from atividade
        public async Task<bool> RemoveQuestaoFromAtividadeAsync(int idAtividade, int idQuestao)
        {
            var response = await _httpClient.DeleteAsync($"atividade/{idAtividade}/questoes/{idQuestao}");
            return response.IsSuccessStatusCode;
        }
    }
}