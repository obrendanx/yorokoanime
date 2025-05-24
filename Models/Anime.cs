using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yorokoanime.Models;

public class Anime
{
    [Key]
    public int Id { get; set; }

    public int? MalId { get; set; }

    public string? Title { get; set; }

    public string? ImageUrl { get; set; }

    public int? Episodes { get; set; }

    public string? Synopsis { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal? Score { get; set; }

    public int? Rank { get; set; }

    public int? Popularity { get; set; }

    public int? Members { get; set; }

    public int? Favorites { get; set; }

    public DateTime? AiredFrom { get; set; }

    public DateTime? AiredTo { get; set; }

    public int? Year { get; set; }

    public string? TrailerUrl { get; set; }

    public string? Background { get; set; }

    public byte[]? ImageBytes { get; set; }

    [Column("title_english")]
    public string? TitleEnglish { get; set; }

    [Column("title_japanese")]
    public string? TitleJapanese { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [StringLength(50)]
    public string? Duration { get; set; }

    [StringLength(50)]
    public string? Rating { get; set; }

    [StringLength(50)]
    public string? Season { get; set; }

    [StringLength(255)]
    public string? Producer { get; set; }

    [StringLength(255)]
    public string? Studio { get; set; }

    public byte[]? MaxImageBytes { get; set; }

    public string? Titles { get; set; }

    public string? TrailerEmbedUrl { get; set; }

    public int? ScoredBy { get; set; }

    [StringLength(100)]
    public string? Source { get; set; }

    public bool? Airing { get; set; }

    public string? AiredString { get; set; }

    public string? BroadcastString { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    public string? Genres { get; set; }

    public string? Demographics { get; set; }

    public string? ExplicitGenres { get; set; }

    public string? Themes { get; set; }

    public string? Licensors { get; set; }
}
