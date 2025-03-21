using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using yorokoanime.Helpers;
using yorokoanime.ViewModels;

namespace yorokoanime.DataAccessLayer;

public class DatabaseMethods
{
    private readonly AppDbContext _dbContext;

    public DatabaseMethods(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public bool RegisterUser(UserRegister model)
    {
        try
        {
            string hashedPassword = PasswordHasher.HashPassword(model.Password);
            
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC RegisterUser @Username, @Email, @Password",
                new SqlParameter("@Username", model.Username),
                new SqlParameter("@Password", hashedPassword), 
                new SqlParameter("@Email", model.Email)
            );
            return true; // If execution succeeds, return true
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering user: {ex.Message}");
            return false; // If an exception occurs, return false
        }
    }
    
    public UserAccount? GetUser(string username)
    {
        try
        {
            var user = _dbContext.Users
                .FromSqlRaw("EXEC GetUser @Username", new SqlParameter("@Username", username))
                .AsEnumerable()
                .Select(u => new UserAccount
                {
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    Password = u.Password
                })
                .FirstOrDefault();
            
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user: {ex.Message}");
        }
        
        return null;
    }
}