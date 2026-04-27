using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class OrderDetail
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public int Quantity { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
