using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorWeb.Components.Authentication;

public class AdminAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public AdminAuthStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var AdminSessionResult = await _sessionStorage.GetAsync<string>("AdminSession");
            var email = AdminSessionResult.Success ? AdminSessionResult.Value : null;

            if (string.IsNullOrEmpty(email))
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
            var identity = new ClaimsIdentity(claims, "SessionAuth");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
        catch (Exception e)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task MarkAdminAsAuthenticatedAsync(string email)
    {
        await _sessionStorage.SetAsync("AdminSession", email);
        var claims = new List<Claim>{ new Claim(ClaimTypes.Name, email) };
        var identity = new ClaimsIdentity(claims, "SessionAuth");
        var admin = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(admin)));
    }

    public async Task MarkAdminAsLoggesOut()
    {
        await _sessionStorage.DeleteAsync("AdminSession");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
