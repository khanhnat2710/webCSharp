using BlazorWeb.Data;
using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Services.brands;

public class BrandsService : IBrandService
{
    private readonly AppDbContext _context;

    public BrandsService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Brand>> GetAllBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand> GetBrandByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task CreateBrandAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBrandAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBrandAsync(int Id)
    {
        Brand brand = await _context.Brands.FindAsync(Id);
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
    }
}
