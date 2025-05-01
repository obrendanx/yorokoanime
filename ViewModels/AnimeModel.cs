using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace yorokoanime.ViewModels
{
    public class AnimeModel
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonIgnore]
        public string? ImageUrl => Images?.Jpg?.ImageUrl ?? "https://via.placeholder.com/150";

        [JsonPropertyName("images")]
        public ImageData Images { get; set; }

        [JsonPropertyName("trailer")]
        public TrailerData Trailer { get; set; }

        [JsonPropertyName("approved")]
        public bool? Approved { get; set; }

        [JsonPropertyName("titles")]
        public List<TitleInfo> Titles { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("title_english")]
        public string? TitleEnglish { get; set; }

        [JsonPropertyName("title_japanese")]
        public string? TitleJapanese { get; set; }

        [JsonPropertyName("title_synonyms")]
        public List<string> TitleSynonyms { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("episodes")]
        public int? Episodes { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("airing")]
        public bool? Airing { get; set; }

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

        [JsonPropertyName("broadcast")]
        public BroadcastData Broadcast { get; set; }

        [JsonPropertyName("producers")]
        public List<Producer> Producers { get; set; }

        [JsonPropertyName("licensors")]
        public List<Licensor> Licensors { get; set; }

        [JsonPropertyName("studios")]
        public List<Studio> Studios { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("explicit_genres")]
        public List<Genre> ExplicitGenres { get; set; }

        [JsonPropertyName("themes")]
        public List<Genre> Themes { get; set; }

        [JsonPropertyName("demographics")]
        public List<Demographic> Demographics { get; set; }


        public int userRating { get; set; } = 0;
        public SelectList RatingOptions { get; set; }
        public bool isLiked { get; set; } = false;
        public double userEpisodes { get; set; } = 0;
        public SelectList EpisodeOptions { get; set; }
        public double userChapters { get; set; } = 0;
        public SelectList ChapterOptions { get; set; }
        public double userVolumes { get; set; } = 0;
        public SelectList VolumeOptions { get; set; }
    }

    public class TitleInfo
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }

    public class ImageData
    {
        [JsonPropertyName("jpg")]
        public JpgData Jpg { get; set; }

        [JsonPropertyName("webp")]
        public JpgData Webp { get; set; }
    }

    public class JpgData
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("small_image_url")]
        public string? SmallImageUrl { get; set; }

        [JsonPropertyName("large_image_url")]
        public string? LargeImageUrl { get; set; }
    }

    public class TrailerData
    {
        [JsonPropertyName("youtube_id")]
        public string? YoutubeId { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("embed_url")]
        public string? EmbedUrl { get; set; }

        [JsonPropertyName("images")]
        public TrailerImages? Images { get; set; }
    }

    public class TrailerImages
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("small_image_url")]
        public string? SmallImageUrl { get; set; }

        [JsonPropertyName("medium_image_url")]
        public string? MediumImageUrl { get; set; }

        [JsonPropertyName("large_image_url")]
        public string? LargeImageUrl { get; set; }

        [JsonPropertyName("maximum_image_url")]
        public string? MaximumImageUrl { get; set; }
    }

    public class AiredData
    {
        [JsonPropertyName("from")]
        public DateTime? From { get; set; }

        [JsonPropertyName("to")]
        public DateTime? To { get; set; }

        [JsonPropertyName("string")]
        public string? DateString { get; set; }
    }

    public class BroadcastData
    {
        [JsonPropertyName("day")]
        public string? Day { get; set; }

        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("string")]
        public string? String { get; set; }
    }

    public class Producer
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class Licensor
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class Studio
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class Genre
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class Demographic
    {
        [JsonPropertyName("mal_id")]
        public int? MalId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class JikanApiResponse
    {
        [JsonPropertyName("data")]
        public List<AnimeModel> Data { get; set; }
    }

    public class AnimeApiResponse
    {
        [JsonPropertyName("data")]
        public AnimeModel Data { get; set; }
    }
}
