using System.Net.Http.Json;

public class AuthAPI
{
    private readonly HttpClient _http;
    public AuthAPI(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }

    public async Task<GoogleLoginUrlResponse?> GetGoogleLoginUrl()
        => await _http.GetFromJsonAsync<GoogleLoginUrlResponse>("auth/google/login-url");
}

public class GoogleLoginUrlResponse
{
    public string Url { get; set; } = "";
}
