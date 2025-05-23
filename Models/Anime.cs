using System.ComponentModel.DataAnnotations;

namespace yorokoanime.Models;

public class Anime
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int TotalNoEpisodes { get; set; }
    public string Desc { get; set; }
    public int userRating { get; set; }
    public bool isLiked { get; set; }
    public decimal episodes {get; set;}
}
