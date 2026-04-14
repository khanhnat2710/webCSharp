using BlazorWeb.Models;

namespace BlazorWeb.Services.categories;

public interface ICategoryService
{
    //Lay du lieu danh sach
    Task<List<Category>> GetAllCategoriesAsync();
    //lay 1 ban ghi theo id
    Task<Category> GetCategoryByIdAsync(int Id);
    //them
    Task CreateCategoryAsync(Category category);
    //sua 
    Task UpdateCategoryAsync(Category category);
    //xoa
    Task DeleteCategoryAsync(int Id);
}