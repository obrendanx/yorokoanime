namespace yorokoanime.Models;

public class UserFavorite
{
    public int Id { get; set; }
    public int malID { get; set; }
    public string username { get; set; }
    public string contentType { get; set; }
    public int userRating { get; set; }
    public bool isLiked { get; set; }
    public decimal episodes {get; set;}
    public decimal chapters {get; set;}
    public decimal volumes {get; set;}
    
}