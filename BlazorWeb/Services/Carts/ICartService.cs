using BlazorWeb.Models;

namespace BlazorWeb.Services.Carts;

public interface ICartService
{
    Task<List<Cart>> GetAllCartsAsync();
    Task<Cart> GetCartByIdAsync(int id);
    Task CreateCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task<Cart> AddToCartAsync(int customerId, int productId, int quantity);
    Task<Cart> GetOrCreateCartAsync(int customerId);
    Task UpdateQuantityAsync(int cartId, int productId, int quantity);
    Task RemoveFromCartAsync(int cartId, int productId);
    Task DeleteCartAsync(int id);
}
