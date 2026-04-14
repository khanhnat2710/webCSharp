using BlazorWeb.Data;
using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;
    
    //Constructor
    public CustomerService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int Id)
    {
        Customer customer = await _context.Customers.FindAsync(Id);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
