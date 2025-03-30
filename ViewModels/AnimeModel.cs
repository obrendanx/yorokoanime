using System.Text.Json.Serialization;

namespace yorokoanime.ViewModels;

public class AnimeModel
{
    [JsonPropertyName("mal_id")]
    public int? MalId { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonIgnore] // Extract the main image URL from the JSON structure
    public string? ImageUrl => Images?.Jpg?.ImageUrl ?? "https://via.placeholder.com/150";
    
    [JsonPropertyName("title_english")]
    public string? TitleEnglish { get; set; }
    
    [JsonPropertyName("title_japanese")]
    public string? TitleJapanese { get; set; }
    
    [JsonPropertyName("title_synonyms")]
    public List<string> TitleSynonyms { get; set; }
    
    [JsonPropertyName("images")]
    public ImageData Images { get; set; }
    
    [JsonPropertyName("trailer")]
    public TrailerData Trailer { get; set; }
    
    [JsonPropertyName("episodes")]
    public int? Episodes { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("aired")]
    public AiredData Aired { get; set; }
    
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }
    
    [JsonPropertyName("rating")]
    public string? Rating { get; set; }
    
    [JsonPropertyName("score")]
    public double? Score { get; set; }
    
    [JsonPropertyName("scored_by")]
    public int? ScoredBy { get; set; }
    
    [JsonPropertyName("rank")]
    public int? Rank { get; set; }
    
    [JsonPropertyName("popularity")]
    public int? Popularity { get; set; }
    
    [JsonPropertyName("members")]
    public int? Members { get; set; }
    
    [JsonPropertyName("favorites")]
    public int? Favorites { get; set; }
    
    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set; }
    
    [JsonPropertyName("background")]
    public string? Background { get; set; }
    
    [JsonPropertyName("season")]
    public string? Season { get; set; }
    
    [JsonPropertyName("year")]
    public int? Year { get; set; }
    
    [JsonPropertyName("producers")]
    public List<Producer> Producers { get; set; }
    
    [JsonPropertyName("studios")]
    public List<Studio> Studios { get; set; }
    
    [JsonPropertyName("genres")]
    public List<Genre> Genres { get; set; }
    
    [JsonPropertyName("demographics")]
    public List<Demographic> Demographics { get; set; }
}

public class ImageData
{
    [JsonPropertyName("jpg")]
    public JpgData Jpg { get; set; }
}

public class JpgData
{
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}

public class TrailerData
{
    [JsonPropertyName("youtube_id")]
    public string? YoutubeId { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("embed_url")]
    public string? EmbedUrl { get; set; }
}

public class AiredData
{
    [JsonPropertyName("string")]
    public string? DateString { get; set; }
}

public class Producer
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class Studio
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class Genre
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class Demographic
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class JikanApiResponse
{
    [JsonPropertyName("data")]
    public List<AnimeModel> Data { get; set; }
}

