using POCA.API.Requests.NewFolder;
using System.Text.Json;

public class GENAIService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public GENAIService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<List<QuestaoRequest>> GenerateQuestionsAsync(string prompt, string systemInstruction)
    {
        var apiKey = _config["OpenAI:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
            throw new InvalidOperationException("AI API key not configured.");

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = systemInstruction },
                new { role = "user", content = prompt }
            },
            max_tokens = 800
        };

        var aiResponse = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

        if (!aiResponse.IsSuccessStatusCode)
        {
            var error = await aiResponse.Content.ReadAsStringAsync();
            throw new ApplicationException($"AI API call failed: {error}");
        }

        var json = await aiResponse.Content.ReadFromJsonAsync<JsonElement>();
        var content = json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<QuestaoRequest>>(content ?? "[]")
                   ?? new List<QuestaoRequest>();
        }
        catch
        {
            throw new ApplicationException("Failed to parse AI response into questions.");
        }
    }
}
