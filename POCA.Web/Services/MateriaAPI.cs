using System.Net.Http.Json;

namespace POCA.Web.Services
{
    public class MateriaAPI
    {
        private readonly HttpClient _httpClient;

        public MateriaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<MateriaResponse>?> GetMateriasAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<MateriaResponse>>("materias");
        }

        public async Task<MateriaResponse?> GetMateriaAsync(int idMateria)
        {
            return await _httpClient.GetFromJsonAsync<MateriaResponse>($"materias/{idMateria}");
        }

        public async Task AddMateriaAsync(MateriaRequest materia)
        {
            await _httpClient.PostAsJsonAsync("materias", materia);
        }

        public async Task UpdateMateriaAsync(MateriaEditRequest materia)
        {
            await _httpClient.PutAsJsonAsync($"materias/{materia.IdMateria}", materia);
        }

        public async Task DeleteMateriaAsync(int idMateria)
        {
            await _httpClient.DeleteAsync($"materias/{idMateria}");
        }
    }
}