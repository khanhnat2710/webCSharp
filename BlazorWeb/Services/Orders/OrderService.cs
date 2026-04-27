using BlazorWeb.Data;
using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.Orders;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Admin)
            .Include(o => o.PaymentMethod)
            .Include(o => o.OrderDetails)
            .ThenInclude(d => d.Product)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Admin)
            .Include(o => o.PaymentMethod)
            .Include(o => o.OrderDetails)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task CreateOrderAsync(Order order)
    {
        if (order.OrderDate == default)
        {
            order.OrderDate = DateTime.Today;
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> CreateOrderFromCartAsync(int cartId, int paymentId)
    {
        var paymentMethod = await _context.PaymentMethods.FindAsync(paymentId);
        if (paymentMethod == null)
        {
            throw new InvalidOperationException("Payment method does not exist.");
        }

        var cart = await _context.Carts
            .Include(c => c.CartDetails)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null || cart.CustomerId == null)
        {
            throw new InvalidOperationException("Cart does not exist.");
        }

        if (cart.CartDetails.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        var order = new Order
        {
            OrderDate = DateTime.Today,
            Status = OrderStatus.Pending,
            PaymentId = paymentId,
            CustomerId = cart.CustomerId
        };

        foreach (var item in cart.CartDetails)
        {
            if (item.Product == null || item.Quantity == null || item.Quantity <= 0)
            {
                continue;
            }

            if (item.Product.Quantity < item.Quantity)
            {
                throw new InvalidOperationException("Not enough stock.");
            }

            order.OrderDetails.Add(new OrderDetail
            {
                ProductId = item.ProductId ?? 0,
                Price = item.Product.Price,
                Quantity = item.Quantity.Value
            });

            item.Product.Quantity -= item.Quantity.Value;
        }

        _context.Orders.Add(order);
        _context.CartDetails.RemoveRange(cart.CartDetails);
        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(order.Id);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return;
        }

        _context.OrderDetails.RemoveRange(order.OrderDetails);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
