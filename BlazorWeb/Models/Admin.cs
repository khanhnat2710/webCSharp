using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Admin
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Can not be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Can not be empty")]
    public UserRole Role { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public enum UserRole
{
    Admin = 0,
    Staff = 1
}
