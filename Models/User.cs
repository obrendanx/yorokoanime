using Microsoft.AspNetCore.Identity;

namespace yorokoanime.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; } = false;
}