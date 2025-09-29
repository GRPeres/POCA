using System.Net.Http.Json;
using POCA.API.Response;
using POCA.Web.Response;

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

        // ✅ Calcular média por matéria (tratando int? para idAluno)
        public async Task<double?> GetMediaPorMateriaAsync(int? idAluno, int idMateria)
        {
            if (!idAluno.HasValue)
                return null; // retorna null se não for aluno

            try
            {
                return await _httpClient.GetFromJsonAsync<double>(
                    $"alunos/{idAluno.Value}/materias/{idMateria}/media");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
