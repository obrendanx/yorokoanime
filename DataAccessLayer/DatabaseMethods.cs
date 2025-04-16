using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using yorokoanime.Helpers;
using yorokoanime.Models;
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
            "EXEC AddAnime @malId, @title, @titles, @imageUrl, @episodes, @synopsis, @score, @rank, @popularity, " +
            "@members, @favorites, @airedFrom, @airedTo, @airedString, @year, @trailerUrl, @trailerEmbedUrl, " +
            "@background, @imageBytes, @maxImageBytes, @title_english, @title_japanese, @status, @duration, " +
            "@rating, @season, @producer, @studio, @source, @airing, @broadcastString, @type, @genres, " +
            "@demographics, @explicitGenres, @themes, @licensors, @scoredBy",

            new SqlParameter("@malId", model.MalId ?? (object)DBNull.Value),
            new SqlParameter("@title", model.Title ?? (object)DBNull.Value),
            new SqlParameter("@titles", string.Join(", ", model.Titles?.Select(t => t.Title) ?? [])),
            new SqlParameter("@imageUrl", model.ImageUrl ?? (object)DBNull.Value),
            new SqlParameter("@episodes", model.Episodes ?? (object)DBNull.Value),
            new SqlParameter("@synopsis", model.Synopsis ?? (object)DBNull.Value),
            new SqlParameter("@score", model.Score ?? (object)DBNull.Value),
            new SqlParameter("@rank", model.Rank ?? (object)DBNull.Value),
            new SqlParameter("@popularity", model.Popularity ?? (object)DBNull.Value),
            new SqlParameter("@members", model.Members ?? (object)DBNull.Value),
            new SqlParameter("@favorites", model.Favorites ?? (object)DBNull.Value),
            new SqlParameter("@airedFrom", DBNull.Value),
            new SqlParameter("@airedTo", DBNull.Value),
            new SqlParameter("@airedString", model.Aired?.DateString ?? (object)DBNull.Value),
            new SqlParameter("@year", model.Year ?? (object)DBNull.Value),
            new SqlParameter("@trailerUrl", model.Trailer?.Url ?? (object)DBNull.Value),
            new SqlParameter("@trailerEmbedUrl", model.Trailer?.EmbedUrl ?? (object)DBNull.Value),
            new SqlParameter("@background", model.Background ?? (object)DBNull.Value),
            new SqlParameter("@imageBytes", imageBytes ?? (object)DBNull.Value),
            new SqlParameter("@maxImageBytes", maxImageBytes ?? (object)DBNull.Value),
            new SqlParameter("@title_english", model.TitleEnglish ?? (object)DBNull.Value),
            new SqlParameter("@title_japanese", model.TitleJapanese ?? (object)DBNull.Value),
            new SqlParameter("@status", model.Status ?? (object)DBNull.Value),
            new SqlParameter("@duration", model.Duration ?? (object)DBNull.Value),
            new SqlParameter("@rating", model.Rating ?? (object)DBNull.Value),
            new SqlParameter("@season", model.Season ?? (object)DBNull.Value),
            new SqlParameter("@producer", string.Join(", ", model.Producers?.Select(p => p.Name) ?? [])),
            new SqlParameter("@studio", string.Join(", ", model.Studios?.Select(s => s.Name) ?? [])),
            new SqlParameter("@source", model.Source ?? (object)DBNull.Value),
            new SqlParameter("@airing", model.Airing ?? (object)DBNull.Value),
            new SqlParameter("@broadcastString", model.Broadcast?.String ?? (object)DBNull.Value),
            new SqlParameter("@type", model.Type ?? (object)DBNull.Value),
            new SqlParameter("@genres", string.Join(", ", model.Genres?.Select(g => g.Name) ?? [])),
            new SqlParameter("@demographics", string.Join(", ", model.Demographics?.Select(d => d.Name) ?? [])),
            new SqlParameter("@explicitGenres", string.Join(", ", model.ExplicitGenres?.Select(e => e.Name) ?? [])),
            new SqlParameter("@themes", string.Join(", ", model.Themes?.Select(t => t.Name) ?? [])),
            new SqlParameter("@licensors", string.Join(", ", model.Licensors?.Select(l => l.Name) ?? [])),
            new SqlParameter("@scoredBy", model.ScoredBy ?? (object)DBNull.Value)
        );

        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error adding anime: {ex.Message}");
        return false;
    }
}
    
    public bool AddManga(MangaModel model, byte[] imageBytes)
    {
        try
        {
            string titleJapanese = model.Titles?.FirstOrDefault(t => t.Type == "Japanese")?.Title ?? model.Title;
            string titleEnglish = model.Titles?.FirstOrDefault(t => t.Type == "English")?.Title ?? model.Title;
            string titleSynonyms = string.Join("; ", model.Titles?.Where(t => t.Type == "Synonym").Select(t => t.Title) ?? new List<string>());
            string authors = string.Join("; ", model.Authors?.Select(a => a.Name) ?? new List<string>());
            string serializations = string.Join("; ", model.Serializations?.Select(s => s.Name) ?? new List<string>());
            string genres = string.Join("; ", model.Genres?.Select(g => g.Name) ?? new List<string>());
            string explicitGenres = string.Join("; ", model.ExplicitGenres?.Select(g => g.Name) ?? new List<string>());
            string themes = string.Join("; ", model.Themes?.Select(g => g.Name) ?? new List<string>());
            string demographics = string.Join("; ", model.Demographics?.Select(g => g.Name) ?? new List<string>());

            _dbContext.Database.ExecuteSqlRaw(
                @"EXEC AddManga 
                    @malId, @titles, @imageUrl, @chapters, @volumes, @synopsis, @score, @imageBytes,
                    @url, @approved, @mainTitle, @titleEnglish, @titleJapanese, @titleSynonyms,
                    @type, @status, @publishing, @publishedFrom, @publishedTo, @publishedString,
                    @publishedPropFromDay, @publishedPropFromMonth, @publishedPropFromYear,
                    @publishedPropToDay, @publishedPropToMonth, @publishedPropToYear,
                    @scored, @scoredBy, @rank, @popularity, @members, @favorites, @background,
                    @authors, @serializations, @genres, @explicitGenres, @themes, @demographics",
                new SqlParameter("@malId", model.MalId),
                new SqlParameter("@titles", titleJapanese),
                new SqlParameter("@imageUrl", model.ImageUrl ?? (object)DBNull.Value),
                new SqlParameter("@chapters", (object?)model.Chapters ?? DBNull.Value),
                new SqlParameter("@volumes", (object?)model.Volumes ?? DBNull.Value),
                new SqlParameter("@synopsis", model.Synopsis ?? (object)DBNull.Value),
                new SqlParameter("@score", (object?)model.Score ?? DBNull.Value),
                new SqlParameter("@imageBytes", imageBytes ?? (object)DBNull.Value),

                new SqlParameter("@url", model.Url ?? (object)DBNull.Value),
                new SqlParameter("@approved", (object?)model.Approved ?? DBNull.Value),
                new SqlParameter("@mainTitle", model.MainTitle ?? (object)DBNull.Value),
                new SqlParameter("@titleEnglish", titleEnglish),
                new SqlParameter("@titleJapanese", titleJapanese),
                new SqlParameter("@titleSynonyms", titleSynonyms),

                new SqlParameter("@type", model.Type ?? (object)DBNull.Value),
                new SqlParameter("@status", model.Status ?? (object)DBNull.Value),
                new SqlParameter("@publishing", model.Publishing),

                new SqlParameter("@publishedFrom", model.Published?.From ?? (object)DBNull.Value),
                new SqlParameter("@publishedTo", model.Published?.To ?? (object)DBNull.Value),
                new SqlParameter("@publishedString", model.Published?.String ?? (object)DBNull.Value),

                new SqlParameter("@publishedPropFromDay", model.Published?.Prop?.From?.Day ?? (object)DBNull.Value),
                new SqlParameter("@publishedPropFromMonth", model.Published?.Prop?.From?.Month ?? (object)DBNull.Value),
                new SqlParameter("@publishedPropFromYear", model.Published?.Prop?.From?.Year ?? (object)DBNull.Value),
                new SqlParameter("@publishedPropToDay", model.Published?.Prop?.To?.Day ?? (object)DBNull.Value),
                new SqlParameter("@publishedPropToMonth", model.Published?.Prop?.To?.Month ?? (object)DBNull.Value),
                new SqlParameter("@publishedPropToYear", model.Published?.Prop?.To?.Year ?? (object)DBNull.Value),

                new SqlParameter("@scored", (object?)model.Scored ?? DBNull.Value),
                new SqlParameter("@scoredBy", (object?)model.ScoredBy ?? DBNull.Value),
                new SqlParameter("@rank", (object?)model.Rank ?? DBNull.Value),
                new SqlParameter("@popularity", (object?)model.Popularity ?? DBNull.Value),
                new SqlParameter("@members", (object?)model.Members ?? DBNull.Value),
                new SqlParameter("@favorites", (object?)model.Favorites ?? DBNull.Value),
                new SqlParameter("@background", model.Background ?? (object)DBNull.Value),

                new SqlParameter("@authors", authors),
                new SqlParameter("@serializations", serializations),
                new SqlParameter("@genres", genres),
                new SqlParameter("@explicitGenres", explicitGenres),
                new SqlParameter("@themes", themes),
                new SqlParameter("@demographics", demographics)
            );

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding manga: {ex.Message}");
            return false;
        }
    }

    
    public bool AddUserFavorite(UserFavorite model)
    {
        try
        {
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC AddUserFavourite @malId, @username, @contentType, @userRating, @hasLiked, @episodes, @chapters, @volumes",
                new SqlParameter("@malId", model.malID),
                new SqlParameter("@username", model.username ?? (object)DBNull.Value),
                new SqlParameter("@contentType", model.contentType ?? (object)DBNull.Value),
                new SqlParameter("@userRating", model.userRating),
                new SqlParameter("@hasLiked", model.isLiked),
                new SqlParameter("@episodes", model.episodes),
                new SqlParameter("@chapters", model.chapters),
                new SqlParameter("@volumes", model.volumes)
            );

            return true; // Return true if successful
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding favorite: {ex.Message}");
            return false; // Return false if an error occurs
        }
    }
    
    public UserFavorite GetUserFavorite(int malId, string username, string contentType)
    {
        try
        {
            var favorites = _dbContext.UserFavorites
                .FromSqlRaw(
                    "EXEC GetUserFavourite @malId, @username, @contentType",
                    new SqlParameter("@malId", malId),
                    new SqlParameter("@username", username ?? (object)DBNull.Value),
                    new SqlParameter("@contentType", contentType ?? (object)DBNull.Value)
                )
                .AsEnumerable() // forces evaluation before FirstOrDefault
                .FirstOrDefault();

            return favorites;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving favorite: {ex.Message}");
            return null; // return null if an error occurs
        }
    }
}

