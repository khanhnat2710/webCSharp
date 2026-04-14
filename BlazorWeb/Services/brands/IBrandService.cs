using BlazorWeb.Models;

namespace BlazorWeb.Services.brands;

public interface IBrandService
{
    //Lấy dữ liệu danh sách
    Task<List<Brand>> GetAllBrandsAsync();
    //Lấy 1 bản ghi theo id
    Task<Brand> GetBrandByIdAsync(int id);
    //Them
    Task CreateBrandAsync(Brand brand);
    //Sửa
    Task UpdateBrandAsync(Brand brand);
    //Xóa
    Task DeleteBrandAsync(int Id);
}
