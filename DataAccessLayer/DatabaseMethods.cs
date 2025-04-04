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
    
    public bool AddAnime(AnimeModel model, byte[] imageBytes, byte[] maxImageBytes)
    {
        try
        {
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC AddAnime @malId, @title, @imageUrl, @episodes, @synopsis, @score, " +
                "@rank, @popularity, @members, @favorites, @year, @trailerUrl, @background, @imageBytes, @title_english, @title_japanese, @status, @duration, @rating, @season, @producer, @studio",
                new SqlParameter("@malId", model.MalId),
                new SqlParameter("@title", (object?)model.Title ?? DBNull.Value), // Convert list to comma-separated string
                new SqlParameter("@imageUrl", model.ImageUrl),
                new SqlParameter("@episodes", (object?)model.Episodes ?? DBNull.Value),
                new SqlParameter("@synopsis", model.Synopsis ?? (object)DBNull.Value),
                new SqlParameter("@score", (object?)model.Score ?? DBNull.Value),
                new SqlParameter("@rank", (object?)model.Rank ?? DBNull.Value),
                new SqlParameter("@popularity", (object?)model.Popularity ?? DBNull.Value),
                new SqlParameter("@members", (object?)model.Members ?? DBNull.Value),
                new SqlParameter("@favorites", (object?)model.Favorites ?? DBNull.Value),
                new SqlParameter("@year", (object?)model.Year ?? DBNull.Value),
                new SqlParameter("@trailerUrl", model.Trailer.Url ?? (object)DBNull.Value),
                new SqlParameter("@background", model.Background ?? (object)DBNull.Value),
                new SqlParameter("@imageBytes", imageBytes ?? (object)DBNull.Value),
                new SqlParameter("@maxImageBytes", maxImageBytes ?? (object)DBNull.Value),
                new SqlParameter("@title_english", model.TitleEnglish ?? (object)DBNull.Value),
                new SqlParameter("@title_japanese", model.TitleJapanese ?? (object)DBNull.Value),
                new SqlParameter("@status", model.Status ?? (object)DBNull.Value),
                new SqlParameter("@duration", model.Duration ?? (object)DBNull.Value),
                new SqlParameter("@rating", model.Rating ?? (object)DBNull.Value),
                new SqlParameter("@season", model.Season ?? (object)DBNull.Value),
                new SqlParameter("@producer", model.Producers.FirstOrDefault().Name ?? (object)DBNull.Value),
                new SqlParameter("@studio", model.Studios.FirstOrDefault().Name ?? (object)DBNull.Value)

            );

            return true; // Return true if successful
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding anime: {ex.Message}");
            return false; // Return false if an error occurs
        }
    }
    
    public bool AddManga(MangaModel model, byte[] imageBytes)
    {
        try
        {
            string titleJapanese = model.Titles?.FirstOrDefault(t => t.Type == "Japanese")?.Title ?? model.Title;
            
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC AddManga @malid, @titles, @imageUrl, @chapters, @volumes, @synopsis, @score, @imageBytes",
                new SqlParameter("@malId", model.MalId),
                new SqlParameter("@titles", titleJapanese), // Convert list to comma-separated string
                new SqlParameter("@imageUrl", model.ImageUrl),
                new SqlParameter("@chapters", (object?)model.Chapters ?? DBNull.Value),
                new SqlParameter("@volumes", (object?)model.Volumes ?? DBNull.Value),
                new SqlParameter("@synopsis", model.Synopsis ?? (object)DBNull.Value),
                new SqlParameter("@score", model.Score ?? (object)DBNull.Value),
                new SqlParameter("@imageBytes", imageBytes ?? (object)DBNull.Value)
            );

            return true; // Return true if successful
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding manga: {ex.Message}");
            return false; // Return false if an error occurs
        }
    }
}