using BlazorWeb.Models;

namespace BlazorWeb.Services.Products;

public interface IProductService
{
    //Lay toan bo
    Task<List<Product>> GetAllProductsAsync(); 
    //Lay theo id
    Task<Product> GetProductByIdAsync(int Id);
    //them
    Task CreateProductAsync(Product product);
    //sua
    Task UpdateProductAsync(Product product);
    //xoa
    Task DeleteProductAsync(int Id);
}