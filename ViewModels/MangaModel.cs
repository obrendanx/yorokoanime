using System.Text.Json.Serialization;

namespace yorokoanime.ViewModels;

public class MangaModel
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("titles")]
    public List<TitleData> Titles { get; set; } = new(); // Ensure it initializes properly

    [JsonIgnore] // We will extract a default title from Titles list
    public string Title => Titles?.FirstOrDefault(t => t.Type == "Default")?.Title ?? "Unknown Title";

    [JsonPropertyName("images")]
    public ImageContainer Images { get; set; } = new();

    [JsonIgnore] // Extract the main image URL from the JSON structure
    public string ImageUrl => Images?.Jpg?.ImageUrl ?? "https://via.placeholder.com/150";

    [JsonPropertyName("chapters")]
    public int? Chapters { get; set; } // Nullable to handle missing values

    [JsonIgnore] // Provide a default value if Chapters is null
    public int ChaptersSafe => Chapters ?? 0;

    [JsonPropertyName("volumes")]
    public int? Volumes { get; set; } // Nullable to handle missing values

    [JsonIgnore] // Provide a default value if Volumes is null
    public int VolumesSafe => Volumes ?? 0;

    [JsonPropertyName("synopsis")]
    public string Synopsis { get; set; } = "No synopsis available.";

    [JsonPropertyName("score")]
    public double? Score { get; set; } // Nullable to handle missing values

    [JsonIgnore] // Provide a default value if Score is null
    public double ScoreSafe => Score ?? 0.0;
    
}

// Class for handling Titles list
public class TitleData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

// Class for handling Image structure
public class ImageContainer
{
    [JsonPropertyName("jpg")]
    public MangaImageData Jpg { get; set; } = new();

    [JsonPropertyName("webp")]
    public MangaImageData Webp { get; set; } = new();
}

// Class for handling ImageData
public class MangaImageData
{
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    
    [JsonPropertyName("small_image_url")]
    public string SmallImageUrl { get; set; }
    
    [JsonPropertyName("large_image_url")]
    public string LargeImageUrl { get; set; }
}

public class JikanMangaApiResponse
{
    public List<MangaModel> Data { get; set; }
}