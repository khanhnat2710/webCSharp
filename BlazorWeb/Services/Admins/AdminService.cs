using BlazorWeb.Models;

using BlazorWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.Admins;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Admin>> GetAllAdminsAsync()
    {
        return await _context.Admins.ToListAsync();
    }

    public async Task<Admin> GetAdminByIdAsync(int id)
    {
        return await _context.Admins.FindAsync(id);
    }

    public async Task CreateAdminAsync(Admin admin)
    {
        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAdminAsync(Admin admin)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAdminAsync(int id)
    {
        Admin admin = await _context.Admins.FindAsync(id);
        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();
    }
}
