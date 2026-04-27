using BlazorWeb.Models;

namespace BlazorWeb.Services.Orders;

public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(Order order);
    Task<Order> CreateOrderFromCartAsync(int cartId, int paymentId);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}
