using BlazorWeb.Models;

namespace BlazorWeb.Services.Admins;

public class AdminService : IAdminService
{
    public Task<List<Admin>> GetAllAdminsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Admin>> GetAdminByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAdminAsync(Admin admin)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAdminAsync(Admin admin)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAdminAsync(int id)
    {
        throw new NotImplementedException();
    }
}