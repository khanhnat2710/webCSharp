using BlazorWeb.Data;
using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.Carts;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cart>> GetAllCartsAsync()
    {
        return await _context.Carts
            .Include(c => c.Customer)
            .Include(c => c.CartDetails)
            .ThenInclude(d => d.Product)
            .ToListAsync();
    }

    public async Task<Cart> GetCartByIdAsync(int id)
    {
        return await _context.Carts
            .Include(c => c.Customer)
            .Include(c => c.CartDetails)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateCartAsync(Cart cart)
    {
        var customer = await _context.Customers.FindAsync(cart.CustomerId);
        if (customer == null)
        {
            throw new InvalidOperationException("Only existing customers can create carts.");
        }

        if (cart.CreateDate == null)
        {
            cart.CreateDate = DateTime.Today;
        }

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        var customer = await _context.Customers.FindAsync(cart.CustomerId);
        if (customer == null)
        {
            throw new InvalidOperationException("Only existing customers can create carts.");
        }

        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> AddToCartAsync(int customerId, int productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0.");
        }

        var product = await _context.Products.FindAsync(productId);
        if (product is null)
        {
            throw new InvalidOperationException("Product does not exist.");
        }

        if (product.Quantity < quantity)
        {
            throw new InvalidOperationException("Not enough stock.");
        }

        var cart = await GetOrCreateCartAsync(customerId);
        var detail = await _context.CartDetails.FirstOrDefaultAsync(d =>
            d.CartId == cart.Id && d.ProductId == productId);

        if (detail == null)
        {
            detail = new CartDetail
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            };

            _context.CartDetails.Add(detail);
        }
        else
        {
            var newQuantity = (detail.Quantity ?? 0) + quantity;
            if (product.Quantity < newQuantity)
            {
                throw new InvalidOperationException("Not enough stock.");
            }

            detail.Quantity = newQuantity;
        }

        await _context.SaveChangesAsync();
        return await GetCartByIdAsync(cart.Id);
    }

    public async Task<Cart> GetOrCreateCartAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new InvalidOperationException("Only existing customers can create carts.");
        }

        var cart = await _context.Carts
            .Include(c => c.Customer)
            .Include(c => c.CartDetails)
            .ThenInclude(d => d.Product)
            .Where(c => c.CustomerId == customerId)
            .OrderByDescending(c => c.Id)
            .FirstOrDefaultAsync();

        if (cart != null)
        {
            return cart;
        }

        var newCart = new Cart
        {
            CustomerId = customerId,
            CreateDate = DateTime.Today
        };

        await CreateCartAsync(newCart);
        return await GetCartByIdAsync(newCart.Id);
    }

    public async Task UpdateQuantityAsync(int cartId, int productId, int quantity)
    {
        var detail = await _context.CartDetails.FirstOrDefaultAsync(d =>
            d.CartId == cartId && d.ProductId == productId);

        if (detail == null)
        {
            return;
        }

        if (quantity <= 0)
        {
            _context.CartDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return;
        }

        var product = await _context.Products.FindAsync(productId);
        if (product == null || product.Quantity < quantity)
        {
            throw new InvalidOperationException("Not enough stock.");
        }

        detail.Quantity = quantity;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int cartId, int productId)
    {
        var detail = await _context.CartDetails.FirstOrDefaultAsync(d =>
            d.CartId == cartId && d.ProductId == productId);

        if (detail == null)
        {
            return;
        }

        _context.CartDetails.Remove(detail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(int id)
    {
        var cart = await _context.Carts
            .Include(c => c.CartDetails)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cart == null)
        {
            return;
        }

        _context.CartDetails.RemoveRange(cart.CartDetails);
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
    }
}
