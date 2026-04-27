using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Address { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
