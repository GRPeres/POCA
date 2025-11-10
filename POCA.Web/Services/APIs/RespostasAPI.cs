using System.Net.Http.Json;
using POCA.API.Response;
using POCA.Web.Response;
using static POCA.Web.Pages.Resolvedor.Materias.MateriasPage;

namespace POCA.Web.Services.APIs
{
    public class RespostasAPI
    {
        private readonly HttpClient _httpClient;

        public RespostasAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        // Buscar todas respostas
        public async Task<ICollection<RespostaResponse>?> GetRespostasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ICollection<RespostaResponse>>("respostas");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        // ✅ Buscar média do aluno por matéria
        public async Task<MediaResponse?> GetMediaPorMateriaAsync(int idAluno, int idMateria)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MediaResponse>(
                    $"alunos/{idAluno}/materias/{idMateria}/media");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
