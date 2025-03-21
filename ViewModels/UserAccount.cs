using Microsoft.AspNetCore.Identity;

namespace yorokoanime.ViewModels;

public class UserAccount
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; } = false;
}