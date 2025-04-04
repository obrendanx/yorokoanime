using System.Diagnostics;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using yorokoanime.DataAccessLayer;
using yorokoanime.Models;
using yorokoanime.Services;
using yorokoanime.ViewModels;

namespace yorokoanime.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AnimeService _animeService;
    private readonly DatabaseMethods _databaseMethods;

    public HomeController(ILogger<HomeController> logger, AnimeService animeService, DatabaseMethods databaseMethods)
    {
        _logger = logger;
        _animeService = animeService;
        _databaseMethods = databaseMethods;
    }

    public async Task<IActionResult> Index()
    {
        List<AnimeModel> topAnime = await _animeService.GetTopAnime();
        List<MangaModel> topManga = await _animeService.GetTopManga();
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
        
        var model = new HomeModel();
        model.TopAnime = topAnime;
        model.TopManga = topManga;
        model.TopAiringAnime = airingAnime;
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}