namespace yorokoanime.Models;

public class UserFavorite
{
    public int Id { get; set; }
    public int malID { get; set; }
    public string username { get; set; }
    public string contentType { get; set; }
    public int userRating { get; set; }
    public bool hasLiked { get; set; }
    public double episodes {get; set;}
    public double chapters {get; set;}
    public double volumes {get; set;}
    
}