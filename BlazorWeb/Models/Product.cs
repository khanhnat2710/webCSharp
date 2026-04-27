using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWeb.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Ram { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Rom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Chip { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Screen_size { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Camera { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Color { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public int Quantity { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please choose a brand")]
    public int BrandId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please choose a category")]
    public int CategoryId { get; set; }

    [ForeignKey("BrandId")]
    public Brand? brand { get; set; }

    [ForeignKey("CategoryId")]
    public Category? category { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
