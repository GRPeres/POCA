using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using POCA.Web.Response.Login;

public class UserSessionService
{
    private readonly IJSRuntime _jsRuntime;
    private const string TokenStorageKey = "authToken";
    private const string UserStorageKey = "userInfo";

    public UserSessionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public UserInfo CurrentUser { get; private set; }
    public string AuthToken { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;

    public event Action OnChange;

    public async Task InitializeAsync()
    {
        // Load token
        var storedToken = await GetFromStorage(TokenStorageKey);
        if (!string.IsNullOrEmpty(storedToken))
        {
            AuthToken = storedToken;

            // Load user info
            var storedUser = await GetFromStorage(UserStorageKey);
            if (!string.IsNullOrEmpty(storedUser))
            {
                CurrentUser = JsonSerializer.Deserialize<UserInfo>(storedUser);
                NotifyStateChanged();
            }
        }
    }

    public async Task Login(PessoaAuthResponse authResponse)
    {
        AuthToken = authResponse.Token;
        CurrentUser = new UserInfo
        {
            IdPessoa = authResponse.IdPessoa,
            Login = authResponse.LoginPessoa,
            IsProfessor = authResponse.IsProfessor,
            IdAluno = authResponse.IdAluno,
            IdProfessor = authResponse.IdProfessor
        };

        await SaveToStorage(TokenStorageKey, AuthToken);
        await SaveToStorage(UserStorageKey, JsonSerializer.Serialize(CurrentUser));
        NotifyStateChanged();
    }

    public async Task Logout()
    {
        CurrentUser = null;
        AuthToken = null;
        await RemoveFromStorage(TokenStorageKey);
        await RemoveFromStorage(UserStorageKey);
        NotifyStateChanged();
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        if (CurrentUser == null)
            return new ClaimsPrincipal(new ClaimsIdentity());

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, CurrentUser.IdPessoa.ToString()),
            new Claim(ClaimTypes.Name, CurrentUser.Login)
        };

        if (CurrentUser.IsProfessor)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Professor"));
            if (CurrentUser.IdProfessor.HasValue)
                claims.Add(new Claim("ProfessorId", CurrentUser.IdProfessor.Value.ToString()));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, "Aluno"));
            if (CurrentUser.IdAluno.HasValue)
                claims.Add(new Claim("AlunoId", CurrentUser.IdAluno.Value.ToString()));
        }

        var identity = new ClaimsIdentity(claims, "custom");
        return new ClaimsPrincipal(identity);
    }

    private async Task<string> GetFromStorage(string key)
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }
        catch
        {
            // Handle JS interop errors if needed
            return null;
        }
    }

    private async Task SaveToStorage(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    private async Task RemoveFromStorage(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

// UserInfo.cs
public class UserInfo
{
    public int IdPessoa { get; set; }
    public string Login { get; set; }
    public bool IsProfessor { get; set; }
    public int? IdAluno { get; set; }
    public int? IdProfessor { get; set; }
}