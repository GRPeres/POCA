// ScreenSound.Web.Services.PerguntasAPI
using ScreenSound.Web.Response;
using System.Net.Http.Json;

public class PerguntasAPI
{
    private readonly HttpClient _httpClient;

    public PerguntasAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    // Get all questions
    public async Task<ICollection<QuestaoResponse>?> GetPerguntasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<QuestaoResponse>>("perguntas");
    }

    // Get questions by activity
    public async Task<ICollection<QuestaoResponse>?> GetPerguntasPorAtividadeAsync(int atividadeId)
    {
        return await _httpClient.GetFromJsonAsync<ICollection<QuestaoResponse>>(
            $"atividades/{atividadeId}/perguntas");
    }

    // Create a new question
    public async Task AddPerguntaAsync(QuestaoRequest pergunta)
    {
        await _httpClient.PostAsJsonAsync("perguntas", pergunta);
    }

    // Update a question
    public async Task UpdatePerguntaAsync(QuestaoEditRequest pergunta)
    {
        await _httpClient.PutAsJsonAsync($"perguntas/{pergunta.Id}", pergunta);
    }
}