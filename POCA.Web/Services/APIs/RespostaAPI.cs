using System.Net.Http.Json;
using POCA.API.Responses;
using POCA.API.Requests;

namespace POCA.Web.Services.APIs
{
    public class RespostaAPI
    {
        private readonly HttpClient _httpClient;

        public RespostaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Use your named client
        }

        // Pega todas
        public async Task<ICollection<RespostaResponse>?> GetRespostasAsync(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<RespostaResponse>>("respostas", cancellationToken);
        }

        // Pega uma por id
        public async Task<RespostaResponse?> GetRespostabyidAsync(int idResposta, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<RespostaResponse>($"respostas/{idResposta}", cancellationToken);
        }

        // Adiciona uma resposta
        public async Task<RespostaResponse?> AddRespostaAsync(RespostaCreateRequest resposta, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("respostas", resposta, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RespostaResponse>(cancellationToken: cancellationToken);
            }

            // Handle error cases
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Failed to add resposta. Status: {response.StatusCode}. Message: {errorContent}");
        }

        // Altera uma resposta por id
        public async Task UpdateRespostaAsync(RespostaEditRequest resposta, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"respostas/{resposta.IdResposta}", resposta, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to update resposta. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }

        // Deleta uma resposta por id
        public async Task DeleteRespostaAsync(int idResposta, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"respostas/{idResposta}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to delete resposta. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }

        // GET aluno de uma resposta
        public async Task<AlunoResponse?> GetAlunoFromResposta(int idResposta, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<AlunoResponse>($"respostas/{idResposta}/aluno", cancellationToken);
        }

        // GET atividade de uma resposta
        public async Task<AtividadeResponse?> GetAtividadeFromResposta(int idResposta, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<AtividadeResponse>($"respostas/{idResposta}/atividade", cancellationToken);
        }

        // GET questão de uma resposta
        public async Task<QuestaoResponse?> GetQuestaoFromResposta(int idResposta, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<QuestaoResponse>($"respostas/{idResposta}/questao", cancellationToken);
        }
    }
}
