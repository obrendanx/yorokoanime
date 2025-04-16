using Microsoft.AspNetCore.Mvc;
using yorokoanime.DataAccessLayer;
using yorokoanime.Services;
using yorokoanime.ViewModels;

namespace yorokoanime.Controllers;

public class AdminController : Controller
{
    private readonly AnimeService _animeService;
    private readonly DatabaseMethods _databaseMethods;

    public AdminController(AnimeService animeService, DatabaseMethods databaseMethods)
    {
        _animeService = animeService;
        _databaseMethods = databaseMethods;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UpdateAnime()
    {
        List<AnimeModel> topAnime = await _animeService.GetTopAnime();
        List<AnimeModel> airingAnime = await _animeService.GetTopAiringAnime();

        foreach (var anime in topAnime)
        {
            // Download image as byte array
            byte[]? imageBytes = await _animeService.DownloadImageAsByteArray(anime.ImageUrl);
            byte[]? maxImageBytes = await _animeService.DownloadImageAsByteArray(anime.Trailer?.Images.MaximumImageUrl);

            // Call AddAnime method
            bool success = _databaseMethods.AddAnime(anime, imageBytes, maxImageBytes);

            Console.WriteLine(success
                ? $"Successfully added anime: {anime.Title}"
                : $"Failed to add anime: {anime.Title}");
        }
        
        foreach (var anime in airingAnime)
        {
            // Download image as byte array
            byte[]? imageBytes = await _animeService.DownloadImageAsByteArray(anime.ImageUrl);
            byte[]? maxImageBytes = await _animeService.DownloadImageAsByteArray(anime.Trailer?.Images.MaximumImageUrl);

            // Call AddAnime method
            bool success = _databaseMethods.AddAnime(anime, imageBytes, maxImageBytes);

            Console.WriteLine(success
                ? $"Successfully added anime: {anime.Title}"
                : $"Failed to add anime: {anime.Title}");
        }

        return RedirectToAction("Success");
    }
    
    public async Task<IActionResult> UpdateManga()
    {
        List<MangaModel> topManga = await _animeService.GetTopManga();
        
        foreach (var manga in topManga)
        {
            // Download image as byte array
            byte[]? imageBytes = await _animeService.DownloadImageAsByteArray(manga.ImageUrl);

            // Call AddAnime method
            bool success = _databaseMethods.AddManga(manga, imageBytes);

            Console.WriteLine(success
                ? $"Successfully added manga: {manga.Title}"
                : $"Failed to add manga: {manga.Title}");
        }

        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}