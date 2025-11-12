// Services/PessoasAPI.cs
using POCA.Web.Requests.Pessoa;
using POCA.Web.Response.Login;
using System.Net.Http.Json;

namespace POCA.Web.Services
{
    public interface IPessoasAPI
    {
        Task<List<PessoaResponse>?> GetPessoasAsync();
        Task<PessoaResponse?> GetPessoaAsync(int id);
        Task<PessoaResponse?> CreatePessoaAsync(PessoaCreateRequest request);
        Task<bool> UpdatePessoaAsync(PessoaEditRequest request);
        Task<bool> DeletePessoaAsync(int id);
        Task<PessoaAuthResponse?> LoginAsync(PessoaLoginRequest request);
    }
    public class PessoasAPI
    {
        private readonly HttpClient _httpClient;

        public PessoasAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<List<PessoaResponse>?> GetPessoasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PessoaResponse>>("pessoas");
        }

        public async Task<PessoaResponse?> GetPessoaAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PessoaResponse>($"pessoas/{id}");
        }

        public async Task<PessoaResponse?> CreatePessoaAsync(PessoaCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("pessoas", request);
            return await response.Content.ReadFromJsonAsync<PessoaResponse>();
        }

        public async Task<bool> UpdatePessoaAsync(PessoaEditRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"pessoas/{request.IdPessoa}", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePessoaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"pessoas/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PessoaAuthResponse?> LoginAsync(PessoaLoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("pessoas/login", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PessoaAuthResponse>();
            }
            return null;
        }
    }
}