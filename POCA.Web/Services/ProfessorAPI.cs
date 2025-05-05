using System.Net.Http.Json;

namespace POCA.Web.Services
{
    public class ProfessorAPI
    {
        private readonly HttpClient _httpClient;

        public ProfessorAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Using the same named client
        }

        public async Task<ICollection<ProfessorResponse>?> GetProfessoresAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ProfessorResponse>>("professores");
        }

        public async Task<ProfessorResponse?> GetProfessorAsync(int idProfessor)
        {
            return await _httpClient.GetFromJsonAsync<ProfessorResponse>($"professores/{idProfessor}");
        }

        public async Task AddProfessorAsync(ProfessorRequest professor)
        {
            await _httpClient.PostAsJsonAsync("professores", professor);
        }

        public async Task UpdateProfessorAsync(ProfessorEditRequest professor)
        {
            await _httpClient.PutAsJsonAsync($"professores/{professor.IdProfessor}", professor);
        }

        public async Task DeleteProfessorAsync(int idProfessor)
        {
            await _httpClient.DeleteAsync($"professores/{idProfessor}");
        }
    }
}