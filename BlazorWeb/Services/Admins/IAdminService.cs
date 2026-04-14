using BlazorWeb.Models;

namespace BlazorWeb.Services.Admins;

public interface IAdminService
{
    //Lấy dữ liệu danh sách
    Task<List<Admin>> GetAllAdminsAsync();
    //Lất 1 bản ghi theo id
    Task<Admin> GetAdminByIdAsync(int id);
    //Thêm 
    Task CreateAdminAsync(Admin admin);
    //Sửa
    Task UpdateAdminAsync(Admin admin);
    //Xóa
    Task DeleteAdminAsync(int id);
}
