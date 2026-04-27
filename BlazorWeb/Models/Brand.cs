using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Brand
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Name { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}