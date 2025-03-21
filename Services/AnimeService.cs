using System.Text.Json;
using yorokoanime.ViewModels;

namespace yorokoanime.Services;

public class AnimeService
{
    private readonly HttpClient _httpClient;

    public AnimeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AnimeModel>> GetTopAnime()
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/top/anime");

        if (!response.IsSuccessStatusCode)
        {
            return new List<AnimeModel>(); // Return empty list if failed
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JikanApiResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        if (result?.Data != null)
        {
            foreach (var anime in result.Data)
            {
                // Manually extract image_url from the nested JSON object
                anime.ImageUrl = anime.Images?.Jpg?.ImageUrl;
            }
        }

        return result?.Data ?? new List<AnimeModel>();
    }
}