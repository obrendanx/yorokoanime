using System.Text.Json.Serialization;

namespace yorokoanime.ViewModels;

public class AnimeModel
{
    public int MalId { get; set; }
    public string Title { get; set; }
    
    [JsonPropertyName("images")]
    public ImageData Images { get; set; }
    public string ImageUrl { get; set; }
    public int Episodes { get; set; }
    public string Synopsis { get; set; }
    public double Score { get; set; }
}

public class ImageData
{
    [JsonPropertyName("jpg")]
    public JpgData Jpg { get; set; }
}

public class JpgData
{
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
}

public class JikanApiResponse
{
    public List<AnimeModel> Data { get; set; }
}