using BlazorWeb.Data;
using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.PaymentMethods;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentMethod>> GetAllPaymentMethodsAsync()
    {
        return await _context.PaymentMethods.ToListAsync();
    }

    public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
    {
        return await _context.PaymentMethods.FindAsync(id);
    }

    public async Task CreatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        _context.PaymentMethods.Update(paymentMethod);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePaymentMethodAsync(int id)
    {
        PaymentMethod paymentMethod = await _context.PaymentMethods.FindAsync(id);
        _context.PaymentMethods.Remove(paymentMethod);
        await _context.SaveChangesAsync();
    }
}
