namespace yorokoanime.Models;

public class Manga
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal TotalNoChapters { get; set; }
    public string Desc { get; set; }
    public int userRating { get; set; }
    public bool isLiked { get; set; }
    public decimal chapters {get; set;}
    public decimal volumes {get; set;}
}
