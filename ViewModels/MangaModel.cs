using System.Text.Json.Serialization;

namespace yorokoanime.ViewModels;

public class MangaModel
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("images")]
    public ImageContainer Images { get; set; } = new();

    [JsonIgnore]
    public string ImageUrl => Images?.Jpg?.ImageUrl ?? "https://via.placeholder.com/150";

    [JsonPropertyName("approved")]
    public bool? Approved { get; set; }

    [JsonPropertyName("titles")]
    public List<TitleData> Titles { get; set; } = new();

    [JsonIgnore]
    public string Title => Titles?.FirstOrDefault(t => t.Type == "Default")?.Title ?? "Unknown Title";

    [JsonPropertyName("title")]
    public string MainTitle { get; set; }

    [JsonPropertyName("title_english")]
    public string TitleEnglish { get; set; }

    [JsonPropertyName("title_japanese")]
    public string TitleJapanese { get; set; }

    [JsonPropertyName("title_synonyms")]
    public List<string> TitleSynonyms { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("chapters")]
    public int? Chapters { get; set; }

    [JsonIgnore]
    public int ChaptersSafe => Chapters ?? 0;

    [JsonPropertyName("volumes")]
    public int? Volumes { get; set; }

    [JsonIgnore]
    public int VolumesSafe => Volumes ?? 0;

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("publishing")]
    public bool Publishing { get; set; }

    [JsonPropertyName("published")]
    public Published Published { get; set; }

    [JsonPropertyName("score")]
    public double? Score { get; set; }

    [JsonIgnore]
    public double ScoreSafe => Score ?? 0.0;

    [JsonPropertyName("scored")]
    public double? Scored { get; set; }

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
    public string Synopsis { get; set; } = "No synopsis available.";

    [JsonPropertyName("background")]
    public string Background { get; set; }

    [JsonPropertyName("authors")]
    public List<Person> Authors { get; set; }

    [JsonPropertyName("serializations")]
    public List<SerializationInfo> Serializations { get; set; }

    [JsonPropertyName("genres")]
    public List<GenreType> Genres { get; set; }

    [JsonPropertyName("explicit_genres")]
    public List<GenreType> ExplicitGenres { get; set; }

    [JsonPropertyName("themes")]
    public List<GenreType> Themes { get; set; }

    [JsonPropertyName("demographics")]
    public List<GenreType> Demographics { get; set; }
}

public class TitleData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

public class ImageContainer
{
    [JsonPropertyName("jpg")]
    public MangaImageData Jpg { get; set; } = new();

    [JsonPropertyName("webp")]
    public MangaImageData Webp { get; set; } = new();
}

public class MangaImageData
{
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("small_image_url")]
    public string SmallImageUrl { get; set; }

    [JsonPropertyName("large_image_url")]
    public string LargeImageUrl { get; set; }
}

public class Published
{
    [JsonPropertyName("from")]
    public DateTime? From { get; set; }

    [JsonPropertyName("to")]
    public DateTime? To { get; set; }

    [JsonPropertyName("prop")]
    public PublishedProp Prop { get; set; }

    [JsonPropertyName("string")]
    public string String { get; set; }
}

public class PublishedProp
{
    [JsonPropertyName("from")]
    public DateParts From { get; set; }

    [JsonPropertyName("to")]
    public DateParts To { get; set; }
}

public class DateParts
{
    [JsonPropertyName("day")]
    public int? Day { get; set; }

    [JsonPropertyName("month")]
    public int? Month { get; set; }

    [JsonPropertyName("year")]
    public int? Year { get; set; }
}

public class Person
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class SerializationInfo
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class GenreType
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

// Wrapper classes
public class JikanMangaApiResponse
{
    public List<MangaModel> Data { get; set; }
}

public class MangaApiResponse
{
    public MangaModel Data { get; set; }
}
