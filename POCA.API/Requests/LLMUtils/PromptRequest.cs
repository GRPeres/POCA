namespace POCA.API.Requests.NewFolder
{
    // Request model for AI with extra metadata
    public record PromptWithMetadataRequest(
        string Prompt,
        string? DificuldadeQuestao,
        string? TemaQuestao
    );
}
