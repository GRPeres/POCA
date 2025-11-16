using System.Net.Http.Json;

public class AuthAPI
{
    private readonly HttpClient _client;

    public AuthAPI(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("API");
    }

    public async Task<GoogleUrlResponse?> GetGoogleLoginUrl()
    {
        return await _client.GetFromJsonAsync<GoogleUrlResponse>("auth/google/login-url");
    }
}

public class GoogleUrlResponse
{
    public string? Url { get; set; }
}
