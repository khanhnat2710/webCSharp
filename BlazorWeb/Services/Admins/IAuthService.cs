namespace BlazorWeb.Services.Admins;

public interface IAuthService
{
    Task<bool> ValidateAdminAuthAsync(string Email, string Password);
}