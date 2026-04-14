using BlazorWeb.Models;

namespace BlazorWeb.Services.Customers;

public interface ICustomerService
{
    //Lay du lieu danh sach
    Task<List<Customer>> GetAllCustomersAsync();
    //lay 1 ban ghi theo id
    Task<Customer> GetCustomerByIdAsync(int id);
    //them
    Task CreateCustomerAsync(Customer customer);
    //sua 
    Task UpdateCustomerAsync(Customer customer);
    //xoa
    Task DeleteCustomerAsync(int Id);
}