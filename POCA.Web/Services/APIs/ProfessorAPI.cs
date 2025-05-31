using System.Net.Http.Json;
using POCA.Web.Requests;
using POCA.Web.Response;

namespace POCA.Web.Services.APIs
{
    public class ProfessorAPI
    {
        private readonly HttpClient _httpClient;

        public ProfessorAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ProfessorResponse>?> GetProfessoresAsync(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ProfessorResponse>>("professores", cancellationToken);
        }

        public async Task<ProfessorResponse?> GetProfessorAsync(int idProfessor, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ProfessorResponse>($"professores/{idProfessor}", cancellationToken);
        }

        public async Task<ProfessorResponse?> AddProfessorAsync(ProfessorCreateRequest professor, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("professores", professor, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProfessorResponse>(cancellationToken: cancellationToken);
            }

            // Handle error cases
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Failed to add professor. Status: {response.StatusCode}. Message: {errorContent}");
        }

        public async Task UpdateProfessorAsync(ProfessorEditRequest professor, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"professores/{professor.IdProfessor}", professor, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to update professor. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }

        public async Task DeleteProfessorAsync(int idProfessor, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"professores/{idProfessor}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to delete professor. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }

        public async Task AddMateriaToProfessorAsync(int idProfessor, int idMateria, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(
                $"professores/{idProfessor}/materias/{idMateria}",
                null,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to add materia to professor. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }

        public async Task RemoveMateriaFromProfessorAsync(int idProfessor, int idMateria, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(
                $"professores/{idProfessor}/materias/{idMateria}",
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Failed to remove materia from professor. Status: {response.StatusCode}. Message: {errorContent}");
            }
        }
    }
}