// UserSessionService.cs
using Microsoft.JSInterop;
using System.Text.Json;

public class UserSessionService
{
    private readonly IJSRuntime _jsRuntime;
    private const string StorageKey = "userSession";

    public UserSessionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public AlunoResponse CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;

    public event Action OnChange;

    public async Task InitializeAsync()
    {
        var storedUser = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
        if (!string.IsNullOrEmpty(storedUser))
        {
            CurrentUser = JsonSerializer.Deserialize<AlunoResponse>(storedUser);
            NotifyStateChanged();
        }
    }

    public async Task Login(AlunoResponse user)
    {
        CurrentUser = user;
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
            StorageKey,
            JsonSerializer.Serialize(user));
        NotifyStateChanged();
    }

    public async Task Logout()
    {
        CurrentUser = null;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}