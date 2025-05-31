using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly UserSessionService _userSession;

    public CustomAuthStateProvider(UserSessionService userSession)
    {
        _userSession = userSession;
        _userSession.OnChange += () =>
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        };
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        if (_userSession.IsLoggedIn && _userSession.CurrentUser is not null)
        {
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userSession.CurrentUser.IdPessoa.ToString()),
                new Claim(ClaimTypes.Role, _userSession.CurrentUser.IsProfessor ? "Professor" : "Aluno")
            }, authenticationType: "custom");
        }
        else
        {
            identity = new ClaimsIdentity(); // Anonymous
        }

        var user = new ClaimsPrincipal(identity);
        return Task.FromResult(new AuthenticationState(user));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

}
