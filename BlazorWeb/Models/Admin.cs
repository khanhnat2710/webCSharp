using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Models;

public class Admin
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Name { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Email { get; set; }
    [Required (ErrorMessage = "Can not be empty")]
    public string Password { get; set; }
    public enum UserRole
    {
        Admin,
        Staff
    }
    [Required (ErrorMessage = "Can not be empty")]
    public UserRole Role { get; set; }
}