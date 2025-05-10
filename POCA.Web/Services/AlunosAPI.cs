// POCA.Web.Services.AlunoAPI
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using POCA.Banco.Model;
using POCA.Web.Pages;
using POCA.Web.Utils;

namespace POCA.Web.Services
{
    public class AlunosAPI
    {
        private readonly HttpClient _httpClient;

        public AlunosAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API"); // Using the same named client
        }

        public async Task<ICollection<AlunoResponse>?> GetAlunosAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<AlunoResponse>>("alunos");
        }

        public async Task<AlunoResponse?> GetAlunosbyIDAsync(int idAluno)
        {
            return await _httpClient.GetFromJsonAsync<AlunoResponse>($"alunos/{idAluno}");
        }

        public async Task AddAlunosAsync(AlunoRequest aluno)
        {
            await _httpClient.PostAsJsonAsync("alunos", aluno);
        }

        public async Task UpdateAlunosAsync(AlunoEditRequest aluno)
        {
            await _httpClient.PutAsJsonAsync($"alunos/{aluno.IdAluno}", aluno);
        }

        public async Task DeleteAlunosAsync(int idAluno)
        {
            await _httpClient.DeleteAsync($"alunos/{idAluno}");
        }

        public class LoginRequest
        {
            public string Login { get; set; }
            public string Senha { get; set; }
        }
        public async Task<AlunoResponse?> CheckAlunosAsync(string login, string senha)
        {
            ICollection<AlunoResponse> alunos = await _httpClient.GetFromJsonAsync<ICollection<AlunoResponse>>("alunos");
            var alunosList = alunos.ToList();

            foreach (var aluno in alunosList)
            {
                if (aluno.LoginAluno == login && aluno.SenhaAluno == senha)
                {
                    // If login is successful, return the student response
                    return aluno;
                }
            }
            return null; // If login failed, return null
        }
    }
}