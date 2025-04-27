using System.Net.Http.Json;

namespace POCA.Web.Services
{
    public class AlunoAPI
    {
        private readonly HttpClient _httpClient;

        public AlunoAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Using the same named client
        }

        public async Task<ICollection<AlunoResponse>?> GetAlunosAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<AlunoResponse>>("alunos");
        }

        public async Task<AlunoResponse?> GetAlunoAsync(int idAluno)
        {
            return await _httpClient.GetFromJsonAsync<AlunoResponse>($"alunos/{idAluno}");
        }

        public async Task AddAlunoAsync(AlunoRequest aluno)
        {
            await _httpClient.PostAsJsonAsync("alunos", aluno);
        }

        public async Task UpdateAlunoAsync(AlunoEditRequest aluno)
        {
            await _httpClient.PutAsJsonAsync($"alunos/{aluno.IdAluno}", aluno);
        }

        public async Task DeleteAlunoAsync(int idAluno)
        {
            await _httpClient.DeleteAsync($"alunos/{idAluno}");
        }
    }
}