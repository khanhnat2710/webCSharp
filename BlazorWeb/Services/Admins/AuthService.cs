using BlazorWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.Admins;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ValidateAdminAuthAsync(string email, string password)
    {
        email = email.Trim();
        password = password.Trim();

        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());

        if (admin == null)
        {
            return false;
        }
        
        bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, admin.Password);
        return isValidPassword;
    }
}
