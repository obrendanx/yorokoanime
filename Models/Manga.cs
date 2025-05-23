using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using yorokoanime.ViewModels;

namespace yorokoanime.Models;

public class Manga
{
    [Key]
    public int Id { get; set; }

    public int? MalId { get; set; }

    public string? Titles { get; set; }

    public string? ImageUrl { get; set; }

    public int? Chapters { get; set; }

    public int? Volumes { get; set; }

    public string? Synopsis { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal? Score { get; set; }

    public byte[]? ImageBytes { get; set; }

    public string? Url { get; set; }

    public bool? Approved { get; set; }

    public string? MainTitle { get; set; }

    public string? TitleEnglish { get; set; }

    public string? TitleJapanese { get; set; }

    public string? TitleSynonyms { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(100)]
    public string? Status { get; set; }

    public bool? Publishing { get; set; }

    public DateTime? PublishedFrom { get; set; }

    public DateTime? PublishedTo { get; set; }

    public string? PublishedString { get; set; }

    public int? PublishedPropFromDay { get; set; }

    public int? PublishedPropFromMonth { get; set; }

    public int? PublishedPropFromYear { get; set; }

    public int? PublishedPropToDay { get; set; }

    public int? PublishedPropToMonth { get; set; }

    public int? PublishedPropToYear { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal? Scored { get; set; }

    public int? ScoredBy { get; set; }

    public int? Rank { get; set; }

    public int? Popularity { get; set; }

    public int? Members { get; set; }

    public int? Favorites { get; set; }

    public string? Background { get; set; }

    public string? Authors { get; set; }

    public string? Serializations { get; set; }

    public string? Genres { get; set; }

    public string? ExplicitGenres { get; set; }

    public string? Themes { get; set; }

    public string? Demographics { get; set; }
}
