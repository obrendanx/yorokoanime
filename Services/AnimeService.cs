using System.Text.Json;
using System.Text.Json.Serialization;
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
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignores null values
        });

        return result?.Data ?? new List<AnimeModel>();
    }
    
    public async Task<List<MangaModel>> GetTopManga()
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/top/manga");

        if (!response.IsSuccessStatusCode)
        {
            return new List<MangaModel>(); // Return empty list if failed
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JikanMangaApiResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignores null values
        });
        

        return result?.Data ?? new List<MangaModel>();
    }
    
    public async Task<List<AnimeModel>> GetTopAiringAnime()
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/top/anime?type=tv&filter=airing");

        if (!response.IsSuccessStatusCode)
        {
            return new List<AnimeModel>(); // Return empty list if failed
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JikanApiResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        return result?.Data ?? new List<AnimeModel>();
    }

    public async Task<byte[]?> DownloadImageAsByteArray(string imageUrl)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(imageUrl);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download image: {imageUrl}. Error: {ex.Message}");
            return null; // Return null if the image download fails
        }
    }
    
    public async Task<AnimeModel> GetAnime(int malID)
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/anime/" + malID);

        if (!response.IsSuccessStatusCode)
        {
            return new AnimeModel(); // Return empty model if failed
        }

        var jsonString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AnimeApiResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Data ?? new AnimeModel(); 
    }
    
    public async Task<MangaModel> GetManga(int malID)
    {
        var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/manga/" + malID);

        if (!response.IsSuccessStatusCode)
        {
            return new MangaModel(); // Return empty model if failed
        }

        var jsonString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<MangaApiResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Data ?? new MangaModel(); 
    }
}