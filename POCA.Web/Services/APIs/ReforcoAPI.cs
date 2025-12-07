using System.Net.Http;
using System.Net.Http.Json;

namespace POCA.Web.Services.APIs
{
    public class ReforcoAPI
    {
        private readonly HttpClient _http;

        public ReforcoAPI(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API");
        }

        public async Task<List<int>> GetReforcoAsync(int idAluno, int idMateria)
        {
            var result = await _http.GetFromJsonAsync<List<int>>(
                $"alunos/{idAluno}/materias/{idMateria}/reforco");

            return result ?? new List<int>();
        }
    }

}