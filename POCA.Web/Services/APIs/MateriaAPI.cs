using System.Net.Http.Json;

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

        // Associa um professor a uma matéria
        public async Task<bool> AddProfessorToMateriaAsync(int idMateria, int idProfessor)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/professores/{idProfessor}", null);
            return response.IsSuccessStatusCode;
        }

        // Remove a associação de um professor com uma matéria
        public async Task<bool> RemoveProfessorFromMateriaAsync(int idMateria, int idProfessor)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/professores/{idProfessor}");
            return response.IsSuccessStatusCode;
        }

        // Associa um aluno a uma matéria
        public async Task<bool> AddAlunoToMateriaAsync(int idMateria, int idAluno)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/alunos/{idAluno}", null);
            return response.IsSuccessStatusCode;
        }

        // Remove a associação de um aluno com uma matéria
        public async Task<bool> RemoveAlunoFromMateriaAsync(int idMateria, int idAluno)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/alunos/{idAluno}");
            return response.IsSuccessStatusCode;
        }

        // Associa uma atividade a uma matéria
        public async Task<bool> AddAtividadeToMateriaAsync(int idMateria, int idAtividade)
        {
            var response = await _httpClient.PostAsync($"materias/{idMateria}/atividades/{idAtividade}", null);
            return response.IsSuccessStatusCode;
        }

        // Remove a associação de uma atividade com uma matéria
        public async Task<bool> RemoveAtividadeFromMateriaAsync(int idMateria, int idAtividade)
        {
            var response = await _httpClient.DeleteAsync($"materias/{idMateria}/atividades/{idAtividade}");
            return response.IsSuccessStatusCode;
        }
    }
}