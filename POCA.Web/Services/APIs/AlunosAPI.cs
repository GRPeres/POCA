using System.Net.Http.Json;
using POCA.Web.Requests.Aluno;

namespace POCA.Web.Services.APIs
{
    public class AlunosAPI
    {
        private readonly HttpClient _httpClient;

        public AlunosAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        // Get all alunos
        public async Task<ICollection<AlunoResponse>?> GetAlunosAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<AlunoResponse>>("alunos");
        }

        // Get single aluno by ID
        public async Task<AlunoResponse?> GetAlunosbyIDAsync(int idAluno)
        {
            return await _httpClient.GetFromJsonAsync<AlunoResponse>($"alunos/{idAluno}");
        }

        // Create new aluno
        public async Task<AlunoResponse?> AddAlunosAsync(AlunoCreateRequest aluno)
        {
            var response = await _httpClient.PostAsJsonAsync("alunos", aluno);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AlunoResponse>();
            }

            // Handle error cases here if needed
            return null;
        }

        // Update existing aluno
        public async Task<bool> UpdateAlunosAsync(AlunoEditRequest aluno)
        {
            var response = await _httpClient.PutAsJsonAsync($"alunos/{aluno.IdAluno}", aluno);
            return response.IsSuccessStatusCode;
        }

        // Delete aluno
        public async Task<bool> DeleteAlunosAsync(int idAluno)
        {
            var response = await _httpClient.DeleteAsync($"alunos/{idAluno}");
            return response.IsSuccessStatusCode;
        }

        // Get all matérias for an aluno
        public async Task<ICollection<MateriaResponse>?> GetMateriasByAlunoAsync(int idAluno, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<MateriaResponse>>(
                $"alunos/{idAluno}/materias",
                cancellationToken);
        }

        // Add materia to aluno
        public async Task<bool> AddMateriaToAlunoAsync(int idAluno, int idMateria)
        {
            var response = await _httpClient.PostAsync($"alunos/{idAluno}/materias/{idMateria}", null);
            return response.IsSuccessStatusCode;
        }

        // Remove materia from aluno
        public async Task<bool> RemoveMateriaFromAlunoAsync(int idAluno, int idMateria)
        {
            var response = await _httpClient.DeleteAsync($"alunos/{idAluno}/materias/{idMateria}");
            return response.IsSuccessStatusCode;
        }
    }
}