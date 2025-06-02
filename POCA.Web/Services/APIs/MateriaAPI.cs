using System.Net.Http.Json;
using POCA.Web.Requests;

namespace POCA.Web.Services.APIs
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
            try
            {
                return await _httpClient.GetFromJsonAsync<ICollection<MateriaResponse>>("materias");
            }
            catch (HttpRequestException)
            {
                // Handle error or log
                return null;
            }
        }

        public async Task<MateriaResponse?> GetMateriaAsync(int idMateria)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MateriaResponse>($"materias/{idMateria}");
            }
            catch (HttpRequestException)
            {
                // Handle error or log
                return null;
            }
        }

        public async Task<MateriaResponse?> AddMateriaAsync(MateriaRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("materias", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MateriaResponse>();
                }
                return null;
            }
            catch (HttpRequestException)
            {
                // Handle error or log
                return null;
            }
        }

        public async Task<bool> UpdateMateriaAsync(MateriaEditRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"materias/{request.IdMateria}", request);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                // Handle error or log
                return false;
            }
        }

        public async Task<bool> DeleteMateriaAsync(int idMateria)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"materias/{idMateria}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                // Handle error or log
                return false;
            }
        }

        // The relationship methods remain the same as they're still valid
        public async Task<bool> AddProfessorToMateriaAsync(int idMateria, int idProfessor)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/professores/{idProfessor}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveProfessorFromMateriaAsync(int idMateria, int idProfessor)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/professores/{idProfessor}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAlunoToMateriaAsync(int idMateria, int idAluno)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/alunos/{idAluno}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAlunoFromMateriaAsync(int idMateria, int idAluno)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/alunos/{idAluno}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAtividadeToMateriaAsync(int idMateria, int idAtividade)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/atividades/{idAtividade}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAtividadeFromMateriaAsync(int idMateria, int idAtividade)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/atividades/{idAtividade}");
            return response.IsSuccessStatusCode;
        }
    }
}